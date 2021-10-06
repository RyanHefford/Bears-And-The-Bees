using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public struct PlayerStats
    {
        public float moveSpeed, jumpSpeed, visibility, crouchChange;

        public PlayerStats(float _moveSpeed, float _jumpSpeed, float _visibility, float _crouchChange)
        {
            moveSpeed = _moveSpeed;
            jumpSpeed = _jumpSpeed;
            visibility = _visibility;
            crouchChange = _crouchChange;
        }
    }

    //private const float CROUCHING_CHANGE = 2.0f;

    private PlayerNoise noise;

    private CharacterController controller;
    private Animator animator;
    private bool moving = false;
    private bool crouching = false;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private float gravity = 20.0f;

    //status effects
    StatusEffectHandler statusHandler;

    public PlayerStats baseStats = new PlayerStats(8.0f, 8.0f, 0.0f, 0.4f);
    public PlayerStats currentStats;

    private Vector3 jumpVelocity = Vector3.zero;
    private GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        noise = GetComponent<PlayerNoise>();
        statusHandler = GetComponent<StatusEffectHandler>();
        PlayerPrefs.SetInt("Paused", 0);
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        //reset stats then apply any stat effects
        currentStats = baseStats;
        currentStats = statusHandler.ApplyStatusEffects(currentStats);

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
            direction *= currentStats.moveSpeed;
            noise.CalculateNoise(currentStats.moveSpeed);
        } else if (moving)
        {
            noise.CalculateNoise(0.0f);
            animator.SetBool("moving", false);
            moving = false;
        }

        //check for player jump
        if (controller.isGrounded && Input.GetButton("Jump"))
        {
            jumpVelocity.y = currentStats.jumpSpeed;
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
                baseStats.moveSpeed *= baseStats.crouchChange;
            else
                baseStats.moveSpeed /= baseStats.crouchChange;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PlayerPrefs.GetInt("Paused") == 1)
            {
                Time.timeScale = 1f;
                pauseMenu.SetActive(false);
                PlayerPrefs.SetInt("Paused", 0);
                Cursor.lockState = CursorLockMode.Locked;
            } 
            else if (PlayerPrefs.GetInt("Paused") == 0)
            {
                Time.timeScale = 0f;
                pauseMenu.SetActive(true);
                PlayerPrefs.SetInt("Paused", 1);
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    
}
