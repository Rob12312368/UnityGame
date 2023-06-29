using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bowling_pin : MonoBehaviour {

    public Transform pin;
    private float threshold=2f;
    public score score;
    public int point=1;
    public bool fall = false;

    //debug
    public static int temp;

    private void Awake()
    {
        temp = 0;
        score = GameObject.FindGameObjectWithTag("score").GetComponent<score>();


    }

    void check_if_fell()
    {
        try
        {
            Debug.Log(pin);
            //Debug.Log(pin.transform.localPosition.y);
            //Debug.Log(fall);
            Debug.Log(pin.transform.localPosition.y < threshold);
            Debug.Log(pin.transform.localPosition.y);
            Debug.Log(threshold);
            Debug.Log(fall == false);
            if ( pin.transform.localPosition.y < threshold && fall == false)
            {
                Debug.Log(pin);
                Debug.Log(pin.transform.localPosition.y);
                temp++;
                score.add(point);
                gameObject.GetComponent<Collider>().enabled = false;
                fall = true;
            }
        }
        catch
        {
            Debug.Log("enter the dead zone");
        }
    }
    //private void OnTriggerEnter(Collider other)
    public void Update()
    {
        check_if_fell();
    }
}
