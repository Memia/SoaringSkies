using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovment : MonoBehaviour
{
    [Header(" Base Variables")]
    public LayerMask mask;
    public float moveSpeed;
    public float startMovementSpeed;
    public float maxMoveSpeed;
    public float speedMultiplier;
    public float speedIncreaseRate;
    private Vector3 moveDirection;
    Rigidbody rigid;

    [Header("Gliding")]
    public bool soaring;
    public float gravityScale;

    [Header("Jump")]
    public float jumpForce;
    public bool IsGrounded;
    public float hitDistance;
    public bool jump;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    [Header("Draft Boost")]
    public bool boosting;
    public float boostForce;

    [Header("Energy Blast")]
    //Activate Blast
    public float energyBlastForce;
    public bool energyBlast;
    public float blastTimer;
    //Blast cooldown
    public float blastCooldownTimer;
    public bool readyToBlast = true;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        readyToBlast = true;
    }
    private void Update()
    {
        Jump();
        Soaring();
        if (rigid.velocity.y < 2 && !soaring && !IsGrounded) // do not run this whilst soaring
        {
            rigid.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier) * Time.deltaTime;
        }
        UpdateStats();
    }
    private void FixedUpdate()
    {
        //EnergyBlast();
        BoostingUp();
        Movement();
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Boost")
        {
            boosting = true;
        }
        if (other.gameObject.tag == "Void")
        {
            SceneManager.LoadScene(0);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Boost")
        {
            boosting = false;
        }
    }
    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            rigid.AddForce(new Vector3(0, jumpForce * Time.deltaTime, 0), ForceMode.Impulse);
        }
        //else if (rigid.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        //{
        //    rigid.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1f) * Time.deltaTime;
        //}
    }
    public void Movement()
    {
        float inputH = Input.GetAxis("Horizontal") * Time.deltaTime;
        float inputV = Input.GetAxis("Vertical") * Time.deltaTime;

        rigid.AddRelativeForce(new Vector3(inputH * moveSpeed, 0, inputV * moveSpeed), ForceMode.Impulse);

		if (Input.GetKey(KeyCode.W))
        {
            moveSpeed += speedIncreaseRate;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            moveSpeed = startMovementSpeed;
        }

        if (moveSpeed > maxMoveSpeed)
        {
            moveSpeed = maxMoveSpeed;
        }
    }
    public void Soaring()
    {
        if(Input.GetKey(KeyCode.Space) && jump) //start soaring
        {
            soaring = true;
            Physics.gravity = new Vector3(0, -40f, 0);
        }
        else
        {
            soaring = false;
            Physics.gravity = new Vector3(0, -20f, 0);
        }
    }
    public void UpdateStats()
    {
        if(IsGrounded)
        {
            hitDistance = 0.55f;
        }
        else
        {
            hitDistance = 0.5f;
        }
        if(Physics.Raycast(transform.position, -transform.up, hitDistance, mask))
        {
            IsGrounded = true;
            readyToBlast = true;
            jump = false;
        }
        else
        {
            jump = true;
            IsGrounded = false;
        }
    }
    public void BoostingUp()
    {
        if(boosting)
        {
            rigid.AddForce(new Vector3(0, boostForce * Time.deltaTime, 0), ForceMode.Impulse);
        }
    }
    public void Sliding()
    {

    }
    public void EnergyBlast()
    {
        if (readyToBlast)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && !IsGrounded)
            {
                energyBlast = true;
            }
            if (energyBlast)
            {
                blastTimer += Time.deltaTime;
                rigid.AddForce(new Vector3(0, 0, energyBlastForce * Time.deltaTime), ForceMode.Impulse);
                if (blastTimer > 0.2)
                {
                    moveSpeed += 10; //increases movment speed when you use this skill
                    energyBlast = false;
                    blastTimer = 0;
                    readyToBlast = false;
                }
            }
        }
    }
}
