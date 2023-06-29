using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class rollballMenu : MonoBehaviour {

    public Canvas HelpMenu;
    public Canvas GameStartMenu;
    public Canvas GamePauseMenu;
    public Canvas GameOverMenu;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Restart()
    {
        SceneManager.LoadScene("roll_a_ball");
    }

    public void GOStart()
    {
        PlayerController.gamestate = PlayerController.playingState;
        GameStartMenu.gameObject.SetActive(false);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Help()
    {
        HelpMenu.gameObject.SetActive(true);
    }

    public void returnHelp()
    {
        HelpMenu.gameObject.SetActive(false);
    }
}
