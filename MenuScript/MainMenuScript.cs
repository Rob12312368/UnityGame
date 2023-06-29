using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

// this script is used for UI in MAIN MENU

public class MainMenuScript : MonoBehaviour {

    public Transform MainMenu;
    public Transform OptionMenu;
    public Transform GameSelectMenu;
    public Transform LeaderBoardMenu;

    public Transform AudioObj;
    public static Transform AudioObject;


    // Use this for initialization
    void Start ()
    {
        OptionMenu.gameObject.SetActive(false);
        GameSelectMenu.gameObject.SetActive(false);
        LeaderBoardMenu.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {

        if (AudioObj)
        {
            AudioObject = AudioObj;
        }
            
        if (AudioObject.GetComponent<AudioSource>().isPlaying)
        {
            GameObject temp = OptionMenu.transform.Find("AudioBtn").gameObject;
            temp.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = "Music: ON";
        }
        else
        {
            GameObject temp = OptionMenu.transform.Find("AudioBtn").gameObject;
            temp.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = "Music: OFF";
        }
    }

    void Awake()
    {
    }

    public void SelectGame()
    {
        // Enter Game select
        GameSelectMenu.gameObject.SetActive(true);
        MainMenu.gameObject.SetActive(false);
    }


    public void LeaderBoard()
    {
        LeaderBoardMenu.gameObject.SetActive(true);
        MainMenu.gameObject.SetActive(false);
    }

    public void ExitMenu()
    {
        Application.Quit();
    }

    public void OptionBtn()
    {
        // Enter Option Menu
        OptionMenu.gameObject.SetActive(true);
        MainMenu.gameObject.SetActive(false);

    }

    public void returnFromOption()
    {
        OptionMenu.gameObject.SetActive(false);
        GameSelectMenu.gameObject.SetActive(false);
        LeaderBoardMenu.gameObject.SetActive(false);
        MainMenu.gameObject.SetActive(true);
    }

    public void ClearLeaderBoard()
    {
        string[] filepath = new string[3];
        filepath[0] = @"./surf.txt";
        filepath[1] = @"./bowling.txt";
        filepath[2] = @"./roll.txt";
        
        foreach(string fp in filepath)
        {
            if (File.Exists(fp))
            {
                File.Delete(fp);
            }
        }

        LeaderBoardMenu.GetComponent<InputField>().text = "Empty";
    }

    public void BtnAudioControl()
    {
        if (AudioObject.GetComponent<AudioSource>().isPlaying)
        {
            GameObject temp = OptionMenu.transform.Find("AudioBtn").gameObject;
            temp.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = "Music: OFF";


            //if (AudioObj)
            //{
            //    AudioObject = AudioObj;
            //}
            if (AudioObject.GetComponent<AudioSource>())
                AudioObject.GetComponent<AudioSource>().Pause();
        }
        else
        {
            GameObject temp = OptionMenu.transform.Find("AudioBtn").gameObject;
            temp.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = "Music: ON";

            //if (AudioObj)
            //{
            //    AudioObject = AudioObj;
            //}
            if (AudioObject.GetComponent<AudioSource>())
                AudioObject.GetComponent<AudioSource>().Play();
        }
    }

    public void StartBallSurfer()
    {
        SceneManager.LoadScene("scene1");
    }

    public void StartBowling()
    {
        SceneManager.LoadScene("bowling");
    }

    public void StartRollball()
    {
        SceneManager.LoadScene("roll_a_ball");
    }

}
