using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_camera : MonoBehaviour {
    GameObject my_cam;
    GameObject my_ball;
	// Use this for initialization
	void Start () {
        my_cam = GameObject.Find("Main Camera");
        
	}
	
	// Update is called once per frame
	void Update () {
        my_ball = GameObject.Find("pumpkin(Clone)");
        if (my_ball.transform.localPosition.z <=-15)
        {
            Vector3 temp = my_ball.transform.localPosition;
            my_cam.transform.localPosition = new Vector3(-5, temp.y + 5, temp.z - 10);
        }

	}
}
