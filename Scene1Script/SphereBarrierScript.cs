using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereBarrierScript : MonoBehaviour {

    private const float laneLeftBound = -4;
    private const float laneRightBound = 4;
    private float oscillationAngle;
    private const float oscillationAngleDelta = 0.05f;

    // Use this for initialization
    void Start () {
        oscillationAngle = 0;
        GetComponent<MeshRenderer>().material.color = Color.black;
	}
	
	// Update is called once per frame
	void Update () {
        oscillationAngle += oscillationAngleDelta;
        transform.localPosition = new Vector3(4.5f*Mathf.Sin(oscillationAngle), transform.localPosition.y, transform.localPosition.z);
	}
}
