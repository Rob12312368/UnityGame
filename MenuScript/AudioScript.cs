﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour {

    private static AudioScript instance = null;
    public static AudioScript Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start () {
		if(instance == null)
        {
            instance = this;
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(this.GetComponentInChildren<AudioSource>().time >= 188)
        {
            this.GetComponentInChildren<AudioSource>().time = 0.8f;
        }
	}
}
