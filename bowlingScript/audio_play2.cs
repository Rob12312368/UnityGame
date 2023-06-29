using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio_play2 : MonoBehaviour {
    AudioSource pin_sound;
    score score;
    // Use this for initialization
    void Start () {
        
        pin_sound = GetComponent<AudioSource>();
        pin_sound.Stop();
        score = GameObject.FindGameObjectWithTag("score").GetComponent<score>();
    }
	
	// Update is called once per frame
	void Update () {
        if(score.text.text=="Score1")
        {
            pin_sound.Play();
        }
    }
}
