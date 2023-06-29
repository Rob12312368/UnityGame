using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCoin : MonoBehaviour {
    private bool exist;
    // Use this for initialization
    void Start () {
        exist = true;
    }
	
	// Update is called once per frame
	void Update () {
    }
    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name.Contains("Player") && exist)
        {
            //this.gameObject.SetActive(false);
            //GetComponent<AudioSource>().Play();
            GetComponent<MeshRenderer>().enabled = false;
            exist = false;
        }
    }
}
