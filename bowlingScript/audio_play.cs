using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio_play : MonoBehaviour {
    AudioSource jazz;
    
	// Use this for initialization
	void Start () {
        jazz = GetComponent<AudioSource>();
        jazz.Play();
	}
	
	// Update is called once per frame
	void Update () {

	}
}
