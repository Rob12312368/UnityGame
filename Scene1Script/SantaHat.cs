using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SantaHat : MonoBehaviour {


    public cuber master;

    // Use this for initialization
    void Start () {
        transform.localPosition = master.transform.localPosition + new Vector3(0, 0.5f, 0);
	}
	
	// Update is called once per frame
	void Update () {

        transform.localPosition = master.transform.localPosition + new Vector3(0,0.5f,0);
    }
}
