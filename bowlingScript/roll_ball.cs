using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class roll_ball : MonoBehaviour {

    Canvas GamePauseMenu;
    Canvas GameStartMenu;


    public static int gamestate;
    public const int initialState = 0;
    public const int gamePauseState = 1;
    public const int DoPosition = 2;
    public const int DoForce = 3;
    public const int DoAngle = 4;
    public const int Rolling = 5;
    public const int fifthsatate = 5;
    public const int sixstate = 6;
    public float ball_speed=1000f;
    public float move_speed=5f;

    public static bool PositionFix;
    public float fixX;

    public int gameCount;

    public static float RotationAngle;

    public int pauseRetrunState;

    private void OnMouseDown()
    {
       // GetComponent<Rigidbody>().AddForce(ball_speed * transform.forward);
    }
    // Use this for initialization
    void Start () {
        RotationAngle = 0f;
        RotationAngle = 0;
        fixX = 0;
        PositionFix = false;

        if (spawn.gameCount == 0)
            gamestate = initialState;
        else
            gamestate = DoPosition;

        pauseRetrunState = initialState;

        GameStartMenu = spawn.gsm;
        GamePauseMenu = spawn.gpm;
    }

    // Update is called once per frame
    void Update()
    {

        // press w to start the game
        if (gamestate == initialState)
        {
            if (Input.GetKey(KeyCode.W))
            {
                gamestate = DoPosition;
                GameStartMenu.gameObject.SetActive(false);
                spawn.hm.gameObject.SetActive(false);
            }
            else
            {
                return;
            }
        }
        // pause the game
        else if (gamestate != gamePauseState)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && gamestate != Rolling)
            {
                pauseRetrunState = gamestate;
                gamestate = gamePauseState;
                GamePauseMenu.gameObject.SetActive(true);
            }
        }
        // continuse the game
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gamestate = pauseRetrunState;
                GamePauseMenu.gameObject.SetActive(false);
                spawn.hm.gameObject.SetActive(false);
            }
            else
            {
                return;
            }
        }

        // try to fix the position
        if (PositionFix == true)
        {
            transform.localPosition = new Vector3(fixX, transform.localPosition.y, transform.localPosition.z);
        }

        // at initial state(position)
        if (gamestate == DoPosition)
        {
            // goto force state
            if (Input.GetKeyDown("space"))
            {
                fixX = transform.localPosition.x;
                gamestate = DoForce;
            }
            if (Input.GetKey(KeyCode.A))
            {
                if (transform.localPosition.x >= -9.4f)
                    transform.localPosition += new Vector3(-0.1f, 0, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                if (transform.localPosition.x <= -2f)
                    transform.localPosition += new Vector3(+0.1f, 0, 0);
            }
            //Vector3 move = Vector3.zero;
            //move.x = Input.GetAxis("Horizontal");
            //transform.position += move * move_speed * Time.deltaTime;
        }
        // force state
        else if (Input.GetKeyDown("space") && gamestate == DoForce)
        {
            // goto angle state
            gamestate = DoAngle;
            ball_speed = slider.strength.value * 250;
        }
        // angle state
        else if (gamestate == DoAngle)
        {
            if (Input.GetKeyDown("space"))
            {
                PositionFix = false;
                // degree to radian
                float RadAngle = RotationAngle * Mathf.PI / 180;
                Vector3 shotDirection = new Vector3(Mathf.Sin(RadAngle), 0, Mathf.Cos(RadAngle));
                GetComponent<Rigidbody>().AddForce(ball_speed * shotDirection);
                gamestate = Rolling;
            }
            if (Input.GetKey(KeyCode.A))
            {
                if (RotationAngle > -90f)
                    RotationAngle -= 1f;
            }
            if (Input.GetKey(KeyCode.D))
            {
                if (RotationAngle < 90f)
                    RotationAngle += 1f;
            }


        }
        else if (gamestate == Rolling)
        {
            // do something
        }
    }
}
