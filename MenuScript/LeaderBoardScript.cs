using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.EventSystems;

public class LeaderBoardScript : MonoBehaviour {

    public Dropdown DropDownMenu;
    public InputField InputFieldMenu;

    private const int Ballsurfer = 0;
    private const int Bowling = 1;
    private const int Rollball = 2;

    private int curr;
    private const int max_data = 100;

    private string filepath;

	// Use this for initialization
	void Start () {
        LoadFile();
        ShowBoard();
	}
	
	// Update is called once per frame
	void Update () {
        if(DropDownMenu.value != curr)
        {
            ShowBoard();
        }
	}

    private void LoadFile()
    {
        if (DropDownMenu.value == Ballsurfer)
        {
            filepath = @"./surf.txt";
            curr = Ballsurfer;
        }
        else if (DropDownMenu.value == Bowling)
        {
            filepath = @"./bowling.txt";
            curr = Bowling;
        }
        else if (DropDownMenu.value == Rollball)
        {
            curr = Rollball;
            filepath = @"./roll.txt";
        }
        else
        {
            curr = -1;
            filepath = "";
        }

        if (!File.Exists(filepath))
        {
            filepath = "";
        }
    }

    private void ShowBoard()
    {

        int count = 0;
        int[] scoreArray = new int[max_data];
        for (int i = 0; i < max_data; i++)
            scoreArray[i] = -1;
        string[] textArray = new string[max_data];

        InputFieldMenu.text = "";
        LoadFile();

        if(filepath == "")
        {
            InputFieldMenu.text = "Empty";
            return;
        }

        StreamReader sr = new StreamReader(filepath);
        while (true)
        {
            string str = sr.ReadLine();
            if (str == null)
            {
                break;
            }
            else
            {
                textArray[count] = str.Split('\t')[0];
                scoreArray[count++] = int.Parse(str.Split('\t')[1]);
            }
        }
        Array.Sort(scoreArray, textArray);
        Debug.Log("sorted");
        for (int i = 99; i >= 90; i--)
        {
            if (textArray[i] != null)
                InputFieldMenu.text += (textArray[i] + "\t\t\t" + scoreArray[i] + "\n");
        }
        sr.Close();

    }

    public void do_update()
    {
        LoadFile();
        ShowBoard();
    }
}
