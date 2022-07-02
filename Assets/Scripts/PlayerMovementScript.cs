using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovementScript : MonoBehaviour
{
    public CharacterController controller;

    public float Speed = 12f;
    public float jumpHeight = 3f;
    public float gravity = -9.81f;
    public float coyoteTime = 0.5f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    float coyoteTimeCooldown = 0;
    float nextJump;
    float disableJumpAt;
    bool isGrounded;
    bool pressedJumpButton;

    void Update()
    {
        MoveAndJump();
        CheckGrounded();
        ApplyGravity();
    }

    private void CheckGrounded()
    {
        if (Physics.CheckSphere(groundCheck.position, groundDistance, groundMask))
        {
            isGrounded = true;
            coyoteTimeCooldown = Time.time + coyoteTime; 
        }
        else if (Time.time < coyoteTimeCooldown)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }
    }

    private void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    private void MoveAndJump()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = transform.right * h + transform.forward * v;
        
        if(Input.GetButtonDown("Jump"))
            disableJumpAt = Time.time + 0.2f;

        pressedJumpButton = Time.time <= disableJumpAt;

        if(pressedJumpButton && isGrounded && nextJump <= Time.time)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            nextJump = Time.time + 0.7f;
        }


        controller.Move(move * Speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }
}