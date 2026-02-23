using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SubMovement : MonoBehaviour
{
    public float subThrust;
    public InputAction moveAction;
    private Rigidbody2D subBody;

    private Animator animator;

    void Start()
    {
        moveAction.Enable();
        moveAction.Enable();
        subBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float currentSpeed = subBody.linearVelocity.magnitude; 

        // Pass the speed value to the Animator's float parameter
        animator.SetFloat("Speed", currentSpeed);
    }

    void FixedUpdate()
    {
        var moveDirection = moveAction.ReadValue<Vector2>();

        // Add force in that direction
        subBody.AddForce(moveDirection * subThrust);
    }
}
