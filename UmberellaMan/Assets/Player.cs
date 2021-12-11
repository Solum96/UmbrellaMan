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
    Animator animator;

    PlayerState state;

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
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;


        if(direction.magnitude >= 0.1f)
        {
            animator.SetBool("isRunning", true);
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraReference.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, rotationSmoothing);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            //_controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
            rb.AddForce(moveDir.normalized * moveSpeed * Time.deltaTime, ForceMode.VelocityChange);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        if (Input.GetButtonDown("Jump") && state == PlayerState.grounded)
        {
            state = PlayerState.airborne;
            rb.AddForce(transform.up * jumpSpeed, ForceMode.VelocityChange);
            animator.SetTrigger("Jump");
        }

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
            //rb.drag = umbrellaFloat;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground")) state = PlayerState.grounded;
    }
}
