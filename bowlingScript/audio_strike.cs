using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class audio_strike : MonoBehaviour {
    AudioSource audiosource;
    public AudioClip strike_sound;
    bool play = true;
    score score;
	// Use this for initialization
	void Start () {
        audiosource = GetComponent<AudioSource>();
        score = GameObject.FindGameObjectWithTag("score").GetComponent<score>();
    }
	void playsound()
    {
        if (play)
            audiosource.PlayOneShot(strike_sound);
        play = false;
    }
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            play = true;
        }
		if(score.display_score == 10)
        {
            playsound();
           
        }
	}
}
