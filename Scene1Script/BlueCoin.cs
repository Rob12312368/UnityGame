using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueCoin : MonoBehaviour {
    private float rotateAngle;
    private bool exist;
    
    // Use this for initialization
    void Start () {
        exist = true;
        rotateAngle = 0;
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }
	
	// Update is called once per frame
	void Update () {
        if (rotateAngle < 360)
        {
            rotateAngle = rotateAngle + 10;
        }
        else
        {
            rotateAngle = rotateAngle - 350;
        }
        transform.localRotation = Quaternion.Euler(0, rotateAngle, 0f);
    }
    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name.Contains("Player") && exist)
        {
            this.gameObject.SetActive(false);
            //GetComponent<AudioSource>().Play();
            GetComponent<MeshRenderer>().enabled = false;
            exist = false;
        }
    }
}
