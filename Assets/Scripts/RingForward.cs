using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingForward : MonoBehaviour
{
    public GameObject player;
	public float upForce = 300;
    public float force = 5000f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player.GetComponent<Rigidbody>().AddRelativeForce(0, upForce, force);
        }
    }
}
