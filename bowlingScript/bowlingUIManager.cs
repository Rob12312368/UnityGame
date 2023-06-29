using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class bowlingUIManager : MonoBehaviour {

    public Canvas GameStartMenu;
    public Canvas GameOverMenu;
    public Canvas GamePauseMenu;
    public Canvas HelpMenu;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // start the game
    public void StartGame()
    {
        roll_ball.gamestate = roll_ball.DoPosition;
        GameStartMenu.gameObject.SetActive(false);
    }

    // return to menu
    public void returnMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ReStartGame()
    {
        SceneManager.LoadScene("bowling");
    }

    public void showHelp()
    {
            HelpMenu.gameObject.SetActive(true);
    }

    public void HelpOKButton()
    {
        HelpMenu.gameObject.SetActive(false);
    }

}
