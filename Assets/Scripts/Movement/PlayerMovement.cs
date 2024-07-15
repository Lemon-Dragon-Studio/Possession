using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement parameters")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float drag;

    [Header("Intermediate object to handle camera")]
    [SerializeField] private Transform orientation;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;

    private Rigidbody rb;
    private Transform trnsfrm;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        trnsfrm = GetComponent<Transform>();
    }

    private void Update()
    {
        MyInput();
        SpeedControl();

        // handle drag
        rb.drag = drag;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        // rotate character (if visible)
        // trnsfrm.rotation = orientation.rotation;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}