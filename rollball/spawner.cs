using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour {

    // Use this for initialization
    private Vector3 v;
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            v.x = Random.Range(-15, 15);
            v.z = Random.Range(-15, 15);
            v.y = -2f;
            Transform c = Instantiate(PickUp);
            c.parent = transform;
            c.localPosition = v;
        }
        for(int i = 0; i < 1; i++)
        {
            v.x = Random.Range(-10, 10);
            v.z = Random.Range(-10, 10);
            v.y = -2f;
            Transform c = Instantiate(bar);
            c.parent = transform;
            c.localPosition = v;
        }

        
        v.x = Random.Range(-13, 13);
        v.z = Random.Range(-13, 13);
        v.y = -2.5f;

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.gamestate == PlayerController.playingState)
        {
            count++;
            if (count % 500 == 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    v.x = Random.Range(-15, 15);
                    v.z = Random.Range(-15, 15);
                    v.y = -2f;
                    Transform c = Instantiate(PickUp);
                    c.parent = transform;
                    c.localPosition = v;
                }

                var clones = GameObject.FindGameObjectsWithTag("area");
                foreach (var clone in clones)
                {
                    Destroy(clone);
                }

                for (int i = 0; i < 3; i++)
                {
                    v.x = Random.Range(-15, 15);
                    v.z = Random.Range(-15, 15);
                    v.y = -2.5f;
                    Transform c = Instantiate(area);
                    c.parent = transform;
                    c.localPosition = v;
                }

            }
            if (count % 1200 == 0)
            {
                v.x = Random.Range(-15, 15);
                v.z = Random.Range(-15, 15);
                v.y = -2f;
                Transform k = Instantiate(larger);
                k.parent = transform;
                k.localPosition = v;

            }
        }
    }

    int count;
    public Transform PickUp;
    public Transform bar;
    public Transform area;
    public Transform ghost;
    public Transform larger;
}
