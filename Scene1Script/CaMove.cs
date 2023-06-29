using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaMove : MonoBehaviour {

    public Transform characterObj;

	// Use this for initialization
	void Start () {
        //GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 6);
	}
	
	// Update is called once per frame
	void Update () {
        transform.localPosition = characterObj.localPosition - new Vector3(characterObj.localPosition.x, characterObj.localPosition.y-5, 5);
    
    }
}
