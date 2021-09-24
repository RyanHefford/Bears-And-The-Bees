using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private const float CROUCHING_CHANGE = 2.0f;

    private PlayerNoise noise;

    private CharacterController controller;
    private Animator animator;
    private bool moving = false;
    private bool crouching = false;
    public float playerSpeed = 8.0f;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    public float jumpSpeed = 8.0f;
    public float visibility = 0.0f;
    private float gravity = 20.0f;
    private Vector3 jumpVelocity = Vector3.zero;
    private GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        noise = GetComponent<PlayerNoise>();
        PlayerPrefs.SetInt("Paused", 0);
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        // Check if player pressed other keys
        KeyControls();
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
            noise.CalculateNoise(playerSpeed);
        } else if (moving)
        {
            noise.CalculateNoise(0.0f);
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

    private void KeyControls()
    {
        if (Input.GetKeyDown("left shift"))
        {
            crouching = !crouching;
            animator.SetBool("crouching", crouching);
            if (crouching)
                playerSpeed /= CROUCHING_CHANGE;
            else
                playerSpeed *= CROUCHING_CHANGE;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PlayerPrefs.GetInt("Paused") == 1)
            {
                Time.timeScale = 1f;
                pauseMenu.SetActive(false);
                PlayerPrefs.SetInt("Paused", 0);
            } 
            else if (PlayerPrefs.GetInt("Paused") == 0)
            {
                Time.timeScale = 0f;
                pauseMenu.SetActive(true);
                PlayerPrefs.SetInt("Paused", 1);
            }
        }
    }
}
