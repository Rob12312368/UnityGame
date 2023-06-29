using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoatationScript : MonoBehaviour {

    // form 0 to 0.5
    // init 0.25
    public Image angleCover;

	// Use this for initialization
	void Start () {
        angleCover.fillAmount = 0.25f;
	}
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<Text>().text = "Angle: " + roll_ball.RotationAngle;
        float fillAmount = 0.25f + 0.25f * roll_ball.RotationAngle/ 90;
        if(fillAmount > 0.5)
        {
            angleCover.fillAmount = 0.5f;
        }
        else if(fillAmount < 0)
        {
            angleCover.fillAmount = 0;
        }
        else
        {
            angleCover.fillAmount = fillAmount;
        }
	}
}
