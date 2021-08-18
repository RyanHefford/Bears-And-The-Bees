using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    private bool moving = false;
    private float playerSpeed = 8f;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private float jumpSpeed = 8.0f;
    private float gravity = 20.0f;
    private Vector3 jumpVelocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Check for player movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            animator.SetBool("moving", true);
            moving = true;

            //adjust for camera's ofset
            direction = Camera.main.transform.TransformDirection(direction);
            direction.y = 0.0f;

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            direction *= playerSpeed;
        } else if (moving)
        {
            animator.SetBool("moving", false);
            moving = false;
        }

        //check for player jump
        if (controller.isGrounded && Input.GetButton("Jump"))
        {
            jumpVelocity.y = jumpSpeed;
        }
        if (!controller.isGrounded)
        {
            jumpVelocity.y -= gravity * Time.deltaTime;
        }

        direction += jumpVelocity;
        
        //Move Player
        controller.Move(direction * Time.deltaTime);
    }
}
