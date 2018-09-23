using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public GameObject fade;
    //Timer is animation time for fading.
    public float timer = 1f;
    public bool countDown = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {   //if count down is true, start counting down.
        if (countDown)
        {
            timer -= Time.deltaTime;
        }
        if(Input.anyKey)
        {   //Activate the fade panel
            fade.SetActive(true);
            countDown = true;
        }
        //Once the timer reach zero, load the game scene
        if (timer <= 0)
        {
            SceneManager.LoadScene(1);
        }
    }

}
