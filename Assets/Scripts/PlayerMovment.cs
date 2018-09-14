using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    [Header("Variables")]
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public float moveSpeed;
    public float jumpForce;
    public float gravityScale;
    public bool soaring;
    public bool jump;
    public bool IsGrounded;
    public float hitDistance;
    public LayerMask mask;

    private Vector3 moveDirection;
    Rigidbody rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        UpdateStats();
        Jump();
        Soaring();
        if (rigid.velocity.y < 0 && !soaring) // do not run this whilst soaring
        {
            rigid.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }
    private void FixedUpdate()
    {
        Movement();
    }
    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            rigid.AddForce(new Vector3(0, jumpForce * Time.deltaTime, 0), ForceMode.Impulse);
        }
        //else if (rigid.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        //{
        //    rigid.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        //}
    }
    public void Movement()
    {
        float inputH = Input.GetAxis("Horizontal") * Time.deltaTime;
        float inputV = Input.GetAxis("Vertical") * Time.deltaTime;

        rigid.AddForce(new Vector3(inputH * moveSpeed, 0, inputV * moveSpeed), ForceMode.Impulse);
    }
    public void Soaring()
    {
        if(Input.GetKey(KeyCode.Space) && !IsGrounded) //start soaring
        {
            soaring = true;
            Physics.gravity = new Vector3(0, -4f, 0);
        }
        else
        {
            soaring = false;
            Physics.gravity = new Vector3(0, -9.81f, 0);
        }
    }
    public void UpdateStats()
    {
        if(IsGrounded)
        {
            hitDistance = 0.35f;
        }
        else
        {
            hitDistance = 0.30f;
        }
        if(Physics.Raycast(transform.position - new Vector3(0,0.85f, 0), -transform.up, hitDistance, mask))
        {
            IsGrounded = true;
        }
        else
        {
            IsGrounded = false;
        }
    }
}
