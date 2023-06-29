using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// this script is used for the UI in GAME

public class MenuScript : MonoBehaviour{

    public Transform HelpMenu;
    public Transform startMenu;
    public Transform pauseMenu;
    public Transform gameOverMenu;
    public RawImage ADPlayer;
    
    private Transform returnState; //temp
    

    public void Start()
    {
        HelpMenu.gameObject.SetActive(false);
        ADPlayer.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HelpMenu.gameObject.SetActive(false);
        }
    }

    public void BtnNextScene()
    {
        SceneManager.LoadScene("scene1");
    }

    public void BtnExit()
    {
        Debug.Log("exit game");
        Application.Quit();
    }

    public void BtnReturnToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void BtnHelp()
    {
        HelpMenu.gameObject.SetActive(true);
    }

    public void BtnHelpOK()
    {
        HelpMenu.gameObject.SetActive(false);
    }

    public void ADClick()
    {
        ADPlayer.gameObject.SetActive(true);
    }

}
