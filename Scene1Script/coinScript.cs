using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinScript : MonoBehaviour
{
    AudioSource coin_sound;
    [SerializeField] AudioClip catch_sound;
    private float rotateAngle;
    private bool exist;

    // Use this for initialization
    void Start()
    {
        exist = true;
        rotateAngle = 0;
        GetComponent<MeshRenderer>().material.color = Color.yellow;
        coin_sound = GetComponent<AudioSource>();
        coin_sound.Stop();


    }

    // Update is called once per frame
    void Update()
    {
        if (rotateAngle < 360)
        {
            rotateAngle = rotateAngle + 10;
        }
        else
        {
            rotateAngle = rotateAngle - 350;
        }
        transform.localRotation = Quaternion.Euler(0, rotateAngle, 90f);
    }

    // collide but no physical reaction
    public void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.name.Contains("Player") && exist)
        {
            //this.gameObject.SetActive(false);
            GetComponent<MeshRenderer>().enabled = false;
            coin_sound.time = 0.25f;
            coin_sound.Play();
            exist = false;
        }
    }
}
