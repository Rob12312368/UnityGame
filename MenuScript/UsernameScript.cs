using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsernameScript : MonoBehaviour {

    public InputField NameInputField;

    public static string playerName;

	// Use this for initialization
	void Start () {
        if (playerName == null)
            NameInputField.placeholder.GetComponent<Text>().text = "Enter your Name...";
        else
            NameInputField.text = playerName;
    }
	
	// Update is called once per frame
	void Update () {
        if(NameInputField.text != null)
            playerName = NameInputField.text;
        else
            NameInputField.placeholder.GetComponent<Text>().text = "Enter your Name...";

    }
}
