using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingUp : MonoBehaviour
{
   // public GameObject player;
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
			other.GetComponent<Rigidbody>().AddForce(0, 2000, force);
        }
    }
}
