using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slider : MonoBehaviour {
    public static Slider strength;
    public Transform BackGround;
    public Transform Fill;

	// Use this for initialization
	void Start () {
        strength = GetComponent<Slider>();
    }
	
	// Update is called once per frame
	void Update () {
       
        if(strength.value==10)
        {
            strength.value = 0;
        }

        if(roll_ball.gamestate==roll_ball.DoForce)
        {
            strength.value += Time.deltaTime * 5;
            BackGround.GetComponent<Image>().color = Color.Lerp(Color.red, Color.green, strength.value / 10);
            Fill.GetComponent<Image>().color = Color.Lerp(Color.red, Color.green, strength.value/10);
        }
	}
}
