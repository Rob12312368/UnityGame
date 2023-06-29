using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (PlayerController.gamestate == PlayerController.playingState)
        {
            //transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
        }
	}
}
