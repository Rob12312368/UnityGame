using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ADPlayerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if ( this.GetComponent<VideoPlayer>().time >= 28.5)
        {
            this.GetComponent<VideoPlayer>().Pause();
        }

        if (this.isActiveAndEnabled)
        {
            if (Input.anyKey)
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    void Awake()
    {
    }
}
