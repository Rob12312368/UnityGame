using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class spawn : MonoBehaviour {

    public Canvas GameStartMenu;
    public Canvas GameOverMenu;
    public Canvas GamePauseMenu;
    public Canvas HelpMenu;

    // hmm somehow it works
    public static Canvas gpm;
    public static Canvas hm;
    public static Canvas gsm;

    public Transform[] pins;
    public Transform ball;
    public score score;
    public Text game_result;

    public bool writeRecord;

    public static int gameCount;

    void Start()
    {
        gpm = GamePauseMenu;
        gsm = GameStartMenu;
        hm = HelpMenu;

        gameCount = 0;
        writeRecord = false;

        GameOverMenu.gameObject.SetActive(false);
        GamePauseMenu.gameObject.SetActive(false);
        HelpMenu.gameObject.SetActive(false);
        
    }

    void spawn_all()
    {
        Transform b = Instantiate(ball);
        b.localPosition =new Vector3(-5f, 2.84f, -41f);
        b.Rotate(0, 90, 0);
        Transform pin1 = Instantiate(pins[0]);
        pin1.localPosition = new Vector3(-5, 2, -22);
        Transform pin2 = Instantiate(pins[1]);
        pin2.localPosition = new Vector3(-6, 2, -20);
        Transform pin3 = Instantiate(pins[2]);
        pin3.localPosition = new Vector3(-4, 2, -20);
        Transform pin4 = Instantiate(pins[3]);
        pin4.localPosition = new Vector3(-7, 2, -18);
        Transform pin5 = Instantiate(pins[4]);
        pin5.localPosition = new Vector3(-5,2,-18);
        Transform pin6 = Instantiate(pins[5]);
        pin6.localPosition = new Vector3(-3, 2, -18);
        Transform pin7 = Instantiate(pins[6]);
        pin7.localPosition = new Vector3(-8, 2, -16);
        Transform pin8 = Instantiate(pins[7]);
        pin8.localPosition = new Vector3(-6, 2, -16);
        Transform pin9 = Instantiate(pins[8]);
        pin9.localPosition = new Vector3(-4, 2, -16);
        Transform pin10 = Instantiate(pins[9]);
        pin10.localPosition = new Vector3(-2, 2, -16);
    }
    void Awake()
    {
        spawn_all();
    }
    private void Update()
    {


        if(Input.GetKeyDown(KeyCode.W) && roll_ball.gamestate == roll_ball.Rolling)
        {
            gameCount++;
            score.total_update();
            var clones = GameObject.FindGameObjectsWithTag("clone");
            foreach (var clone in clones)
            {
                Destroy(clone);
            }
            spawn_all();
            score.set(0);
            score.change_game_num();
            score.gamenumber_update();
            score.strike.text = "";
            roll_ball.gamestate = roll_ball.DoPosition;
            slider.strength.value = 0f;
            roll_ball.PositionFix = true;

            if (gameCount == 10)
            {
                GameObject canv = GameObject.Find("game_ui");
                GameObject sco = GameObject.Find("score");
                GameObject str = GameObject.Find("strike");
                GameObject gam = GameObject.Find("gamenumber");
                GameObject tot = GameObject.Find("total_score");
                GameObject Sli = GameObject.Find("Slider");
                GameObject ang = GameObject.Find("Rotation");
                GameObject ang1 = GameObject.Find("angleCover");
                GameObject ang2 = GameObject.Find("angleBackground");
                Destroy(sco);
                Destroy(str);
                Destroy(gam);
                Destroy(tot);
                Destroy(Sli);
                Destroy(ang);
                Destroy(ang1);
                Destroy(ang2);
                RawImage img = canv.GetComponent<RawImage>();
                img.enabled = true;
                GameOverMenu.gameObject.SetActive(true);
                game_result.text = "TOTAL SCORE " + score.total_score;

                if (!writeRecord)
                {
                    writeRecord = false;
                    string filepath = @"./bowling.txt";

                    StreamWriter sw;
                    if (!File.Exists(filepath))
                        sw = File.CreateText(filepath);
                    else
                        sw = File.AppendText(filepath);

                    string playerName;
                    if (UsernameScript.playerName == "")
                        playerName = "Visitor";
                    else
                        playerName = UsernameScript.playerName;
                    sw.WriteLine(playerName + "\t" + score.total_score);

                    sw.Close();
                }
            }

        }


    }
}
