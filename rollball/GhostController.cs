using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GhostController : MonoBehaviour
{
    public int A;
    public Vector3 velocity;

    void Start()
    {
        velocity = new Vector3(Random.Range(-9, 9), 0, Random.Range(-9, 9));
    }
    // Update is called once per frame
    void Update()
    {
        if (PlayerController.gamestate == PlayerController.playingState)
        {
            // transform.position = new Vector3(5 * Mathf.PingPong(Time.time, 3), transform.position.y, transform.position.z);
            this.GetComponent<Rigidbody>().velocity = velocity;
        }
        else
        {
            this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name.Contains("Wall"))
        {
            velocity *= -1;
        }
    }
}
