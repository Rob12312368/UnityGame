using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.IO;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class PlayerController : MonoBehaviour
{ 
    public float speed;
    public Text countText;

    private Rigidbody rb;
    private int count;
    public int p;

    public static int gamestate;
    public const int initialState = 0;
    public const int playingState = 1;
    public const int gameoverState = 2;
    public const int pauseState = 3;

    private bool writeRecord = false;

    public Canvas GameStartMenu;
    public Canvas GamePauseMenu;
    public Canvas GameOverMenu;
    public Canvas HelpMenu;

    public Vector3 pausePosition;
    public Vector3 gameOverPosition;

    //public Vector3 jump = new Vector3(0, 1f, 0);
    //public float forceAmount = 500f;
    public Vector3 jumpForce = new Vector3(0, 500f, 0);

    public bool OnGround;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        countText.text = "Count: " + count.ToString();

        OnGround = true;

        gamestate = initialState;

        GameStartMenu.gameObject.SetActive(true);
        GamePauseMenu.gameObject.SetActive(false);
        GameOverMenu.gameObject.SetActive(false);
        HelpMenu.gameObject.SetActive(false);
        
    }

    void Update()
    {
        // start the gamee
        if(gamestate == initialState)
        {
            if (Input.GetKey(KeyCode.W))
            {
                gamestate = playingState;
                GameStartMenu.gameObject.SetActive(false);
            }
            else
            {
                return;
            }
        }

        if(gamestate == gameoverState)
        {
            transform.localPosition = gameOverPosition;
            GameOverMenu.gameObject.SetActive(true);

            if (!writeRecord)
            {
                writeRecord = true;
                string filepath = @"./roll.txt";
                StreamWriter sw;
                if (File.Exists(filepath))
                {
                    sw = File.AppendText(filepath);
                }
                else
                {
                    sw = File.CreateText(filepath);
                }
                string playerName;
                if(UsernameScript.playerName == "")
                {
                    playerName  = "Visitor";
                }
                else
                {
                    playerName = UsernameScript.playerName;
                }
                sw.WriteLine(playerName + "\t" + count);
                sw.Close();
            }

            return;
        }

        // pause the game
        if (Input.GetKeyDown(KeyCode.Escape) && gamestate == playingState)
        {
            pausePosition = transform.localPosition;
            gamestate = pauseState;
            GamePauseMenu.gameObject.SetActive(true);
            return;
        }
        // resume the game
        else if(Input.GetKeyDown(KeyCode.Escape) && gamestate == pauseState)
        {
            GamePauseMenu.gameObject.SetActive(false);
            HelpMenu.gameObject.SetActive(false);
            gamestate = playingState;
        }

        if(gamestate == pauseState)
        {
            transform.localPosition = pausePosition;
            return;
        }

        p++;
        if (Input.GetKeyDown(KeyCode.Space) && OnGround)
        {
            Debug.Log("a");
            rb.AddForce(jumpForce);
            OnGround = false;
        }

        if (p % 1000 == 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (transform.localPosition.y < -10f)
        {
            gamestate = gameoverState;
            gameOverPosition = transform.localPosition;
        }
    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            countText.text = "Count: " + count.ToString();
        }

        if (other.gameObject.CompareTag("larger"))
        {
            other.gameObject.SetActive(false);
            transform.localScale += new Vector3(1, 1, 1);
            
        }

        if (other.gameObject.CompareTag("area"))
        {
            gamestate = gameoverState;
            gameOverPosition = transform.localPosition;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name.Contains("Ghost"))
        {
            gamestate = gameoverState;
            gameOverPosition = transform.localPosition;
        }

        if(other.gameObject.name.Contains("Ground"))
        {
            OnGround = true;
        }
    }
}


   