using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SubMovement : MonoBehaviour
{
    public float subThrust;
    public InputAction moveAction;
    private Rigidbody2D subBody;

    void Start()
    {
        moveAction.Enable();
        moveAction.Enable();
        subBody = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        var moveDirection = moveAction.ReadValue<Vector2>();
        //position += moveDirection * moveSpeed * Time.deltaTime;

        subBody.AddForce(moveDirection * subThrust);
    }
}
