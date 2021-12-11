using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { grounded, airborne }
public class Player : MonoBehaviour
{
    public Umbrella umbrella;
    public Transform cameraReference;
    Rigidbody rb;
    Collider capsuleCollider;

    public Animator animator;
    public PlayerState state;

    float distToGround;
    public float moveSpeed = 6f;
    public float rotationSmoothing = 0.1f;
    public float jumpSpeed = 8f;
    float turnSmoothVelocity;
    public float umbrellaFloat = 2f;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        rb = GetComponent<Rigidbody>();
        umbrella = GetComponentInChildren<Umbrella>();
        animator = GetComponent<Animator>();
        state = PlayerState.grounded;
        capsuleCollider = GetComponentInChildren<CapsuleCollider>();
        distToGround = capsuleCollider.bounds.extents.y;
    }
    void FixedUpdate()
    {
        float mH = Input.GetAxis("Horizontal");
        float mV = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(mH, 0f, mV);

        bool isRunning = direction.magnitude >= 0.1f;
        animator.SetBool("isRunning", isRunning);

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, rotationSmoothing);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        rb.velocity = new Vector3(mH * moveSpeed, rb.velocity.y, mV * moveSpeed);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(new Vector3(0f, jumpSpeed, 0f), ForceMode.VelocityChange);
            Debug.Log(rb.velocity.y);
        }
        state = IsGrounded() ? PlayerState.grounded : PlayerState.airborne;

        // Umbrella logic
        if (Input.GetKey(KeyCode.Mouse1))
        {
            if(umbrella.State == UmbrellaState.closed) { umbrella.Open(); }
        }
        else
        {
            if(umbrella.State == UmbrellaState.open) { umbrella.Close(); }
        }

        if(state == PlayerState.airborne && umbrella.State == UmbrellaState.open)
        {
            rb.drag = umbrellaFloat;
        }
        else
        {
            rb.drag = 0;
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(rb.position, Vector3.down, distToGround + 0.1f);
    }
}
