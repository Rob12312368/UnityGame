using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;

[RequireComponent(typeof(Collider))]

public class cuber : MonoBehaviour {

    public static int playerScore; 
    public static float playerSpeed; // only for display info
    public static float diveSinceCoolDown; // for both
    public static bool diveReset;
    protected int OnLanes;   // index of lane the object running on
    protected bool InLanes;  // object is switching lane of not
    private bool OnGround;
    private bool JumpForce;
    private bool GameOver;
    private bool GamePause;
    private bool GameStart; // Press anything to start the game at begining
    private bool isUnstoppable;
    private bool reJumpEnable;
    private bool DiveForce;
    private bool isDiving;
    private bool isCrashing;
    private bool hitTextTimerEnable;
    private bool writeRecord;
    
    private float ForwardSpeed = 6f;
    private float switchLandSpeed = 3.7f;
    private float hitTextTimer = 0f;

    private const float jumpForce = 6.8f;
    private const float speedUpperLimit = 19f;
    private const float speedLowerLimit = 6f;
    private const float speedUpRate = 1f;
    private const float switchLandSpeedUpRate = 0.1f;
    private const float crashingHeight = 2.5f;
    private const float diveCoolDown = 2f; // CD of dive
    private const float DiveSpeedUP = 30f;

    private string playerName;

    private Vector3 tempVelocity; // if the game is paused
    private Vector3 tempPosition;

    public Transform MainCamera;
    public Transform LightSource;
    public Transform GameOverMenu;
    public Transform GamePauseMenu;
    public Transform GameStartMenu;
    public Transform GameHelpMenu;
    public Transform GameUI;
    public Text HitText;
    

	// Use this for initialization
	void Start () {
        playerScore = 0;
        OnLanes = 0;
        InLanes = true;
        OnGround = true;
        JumpForce = false;
        GameOver = false;
        GamePause = false;
        GameStart = false;
        isUnstoppable = false;
        reJumpEnable = false;
        DiveForce = false;
        isDiving = false;
        isCrashing = false;
        hitTextTimerEnable = false;
        diveSinceCoolDown = 2f;
        writeRecord = false;
       

        GameOverMenu.localPosition = transform.localPosition;
        GameOverMenu.gameObject.SetActive(false);
        GamePauseMenu.localPosition = transform.localPosition;
        GamePauseMenu.gameObject.SetActive(false);
        GameStartMenu.localPosition = transform.localPosition;
        GameStartMenu.gameObject.SetActive(true);
        HitText.gameObject.SetActive(false);
    }
   
    // Update is called once per frame
    void Update () {
        // light source following
        LightSource.transform.localPosition = transform.localPosition - new Vector3(0, -10f, 10f);

        // perss to start game
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)) && !GameStart)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, ForwardSpeed);
            GameStart = true;
            GameStartMenu.localPosition = transform.localPosition;
            GameStartMenu.gameObject.SetActive(false);
            GameHelpMenu.gameObject.SetActive(false);
        }
        if (!GameStart)
        {
            return;
        }

        // game over
        if(GameOver)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                SceneManager.LoadScene("scene1");
            }
            // if haven't write record yet
            if (!writeRecord)
            {
                StreamWriter recordWriter;
                writeRecord = true;
                if (UsernameScript.playerName == "")
                    playerName = "Visitor";
                else
                    playerName = UsernameScript.playerName;
                string filepath = (@"./surf.txt");
                if (!File.Exists(filepath))
                {
                    recordWriter = File.CreateText(filepath);
                }
                else
                {
                    recordWriter = File.AppendText(filepath);
                }

                recordWriter.WriteLine(playerName + "\t" + playerScore);
                recordWriter.Close();
            }
            return;
        }

        if(transform.localPosition.y <= -5)
        {
            SetGameOver();
            return;
        }
        

        // Check escape(pause)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Pause the game
            if (!GamePause)
            {
                // Keep track of player speed (for UI) when pause
                tempVelocity = GetComponent<Rigidbody>().velocity;
                playerSpeed = tempVelocity.z;
                // freeze the player's movement
                tempPosition = transform.localPosition;
                GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                GamePause = true;
                // show pause menu
                GamePauseMenu.gameObject.SetActive(true);
                return;
            }
            // Continue the game
            else
            {
                // player retrieve velocity
                GetComponent<Rigidbody>().velocity = tempVelocity;
                GamePause = false;
                // close pause menu
                GamePauseMenu.gameObject.SetActive(false);
                GameHelpMenu.gameObject.SetActive(false);
            }
        }

        // game pause
        if (GamePause)
        {
            // freeze player's movement
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            transform.localPosition = tempPosition;
            return;
        }

        // update velocity (include punishment of diving)
        if (isDiving)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, GetComponent<Rigidbody>().velocity.y, ForwardSpeed+DiveSpeedUP);
        }
        else
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, GetComponent<Rigidbody>().velocity.y, ForwardSpeed);
        }
        playerSpeed = ForwardSpeed;


        // press w to speed up
        if (Input.GetKeyDown(KeyCode.W) && OnGround)
        {
            if (ForwardSpeed <= speedUpperLimit)
            {
                ForwardSpeed += speedUpRate;
                switchLandSpeed += switchLandSpeedUpRate;
            }
        }
        // press s to speed down
        if (Input.GetKeyDown(KeyCode.S) && OnGround)
        {
            if (ForwardSpeed >= speedLowerLimit)
            {
                ForwardSpeed -= speedUpRate;
                switchLandSpeed -= switchLandSpeedUpRate;
            }
        }
        // press d to move right
        if (Input.GetKeyDown(KeyCode.D) && OnLanes < 2 && OnGround)
        {
            OnLanes += 1;
            InLanes = false;
        }
        // press a to move left
        if (Input.GetKeyDown(KeyCode.A) && OnLanes > -2 && OnGround)
        {
            OnLanes -= 1;
            InLanes = false;
        }
        // you can only jump when:
        // 1. Object is on the ground
        // 2. Object is not switching lanes
        if (Input.GetKeyDown(KeyCode.Space) && OnGround && InLanes && transform.localPosition.y >= 0.4)
        {
            OnGround = false;
            JumpForce = true;
        }
        // press space when not on ground
        else if (Input.GetKeyDown(KeyCode.Space) && !OnGround && InLanes && transform.localPosition.y >= 0.4)
        {
            // Go double jump
            if (reJumpEnable)
            {
                reJumpEnable = false;
                JumpForce = true;
                GetComponent<MeshRenderer>().material.color = Color.white;
            }
            // Go dive
            else if (diveSinceCoolDown >= diveCoolDown)
            {
                diveReset = true;
                DiveForce = true;
                isDiving = true;
                diveSinceCoolDown = 0;
                // height enough to crash and cooldown finish
                if (transform.localPosition.y >= crashingHeight)
                {
                    isCrashing = true;
                }
                else
                {
                    isCrashing = false;
                }
            }


        }

        diveSinceCoolDown += Time.deltaTime;
        if (hitTextTimerEnable)
        {
            hitTextTimer += Time.deltaTime;
            // when hit show nice for 1sec
            if (hitTextTimer >= 1f)
            {
                HitText.gameObject.SetActive(false);
                hitTextTimerEnable = false;
                hitTextTimer = 0f;
            }
            else
            {
                Color temp = HitText.color;
                temp.a -= 0.05f;
                HitText.color = temp;
                HitText.transform.localPosition += new Vector3(0,Time.deltaTime * 40f,0);
            }
        }
         
        // show diving heigh
        /*if (transform.localPosition.y >= crashingHeight)
        {

            GetComponent<MeshRenderer>().material.color = Color.blue;
        }
        else
        {

            GetComponent<MeshRenderer>().material.color = Color.white;
        }*/


        //  totally five lanes
        //  the middle of each lane is 2 * index of the lane
        //  |     |     |     |     |     |
        //  | -2  | -1  |  0  |  1  |  2  |
        //  |     |     |     |     |     |
        if (!InLanes)
        {
            // try to avoid ball from oscillationg at x axis
            if (transform.localPosition.x > 2 * OnLanes + 0.2f)
            {
                transform.localPosition -= Time.deltaTime * new Vector3(switchLandSpeed, 0, 0);
            }
            else if (transform.localPosition.x < 2 * OnLanes-0.2f)
            {
                transform.localPosition += Time.deltaTime * new Vector3(switchLandSpeed, 0, 0);
            }
            // settle down to the lane when players is +-0.2f from the lane
            else
            {
                transform.localPosition += new Vector3(-transform.localPosition.x + 2 * OnLanes, 0, 0);
                InLanes = true;
            }
        }
        else
        {
            transform.localPosition += new Vector3(-transform.localPosition.x + 2 * OnLanes, 0, 0);
        }
		
	}

    private void FixedUpdate()
    {
        if (JumpForce)
        {
            // add vertical upward force to the ball
            JumpForce = false;
            GetComponent<Rigidbody>().AddForce(0, jumpForce, 0, ForceMode.Impulse);
        }
        if (DiveForce)
        {
            // add vertical downwrad force to the ball
            DiveForce = false;
            GetComponent<Rigidbody>().AddForce(0, -3*jumpForce, 0, ForceMode.Impulse);
        }
    }

    // Detect the Collision object
    // Detect barrier?
    // Detect Ground?
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("disabled"))
        {
            isDiving = false;
        }
        
       // update the jump state
       if (collision.gameObject.name.Contains("Ground"))
        {
            isDiving = false;
            OnGround = true;
            isDiving = false;
            isCrashing = false;
        }
        else
        {
            OnGround = false;
        }

        // update game state
        if (collision.gameObject.name.Contains("Barrier"))
        {
            isDiving = false;
            if (isUnstoppable == false && isCrashing == false)
            {
                SetGameOver();
                return;
            }
            // Dive and Crash
            else if(isCrashing)
            {
                Debug.Log("Crash barrier");
                diveSinceCoolDown = 2.1f;
                isCrashing = false;
                collision.gameObject.name = "disabled";
                HitText.gameObject.SetActive(true);
                HitText.transform.localPosition = new Vector3(collision.transform.localPosition.x+OnLanes, transform.localPosition.y, 0);
                hitTextTimerEnable = true;

                // reset tranparancy
                Color temp = HitText.color;
                temp.a = 1;
                HitText.color = temp;

                hitTextTimer = 0f;
            }
            // cancel unstoppable
            else
            {
                Debug.Log("Unstoppable crash");
                isUnstoppable = false;
                collision.gameObject.name = "disabled";
                GetComponent<MeshRenderer>().material.color = Color.white;
            }

            isCrashing = false;
        }
    }

    // Collide but no physical reaction
    // (remember to turn IsTrigger of the collided Object On)
    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name.Contains("coin"))
        {
            playerScore++;
        }
        if (collider.gameObject.name.Contains("Shield") && !GameOver)
        {
            isUnstoppable = true;
            reJumpEnable = false;
            GetComponent<MeshRenderer>().material.color = Color.blue;
            
        }
        if (collider.gameObject.name.Contains("redCoin") && !GameOver)
        {

            reJumpEnable = true;
            isUnstoppable = false;
            GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    public void SetGameOver()
    {
        // turn black is touch any barrier
        GameOver = true;
        GetComponent<MeshRenderer>().material.color = Color.black;

        // show GameOver Menu
        GameOverMenu.gameObject.SetActive(true);
    }

    public void StartButtonClick()
    {
        GameStart = true;
        GameStartMenu.gameObject.SetActive(false);
    }
    public void ContinueButtonClick()
    {
        GameStartMenu.gameObject.SetActive(false);
    }
}
