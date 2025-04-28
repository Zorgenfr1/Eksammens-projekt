using System.Runtime.CompilerServices;
using NUnit.Framework.Internal;
using Unity.Properties;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    private CharacterController controller;
    [SerializeField] private new Transform camera;
    public Animator animator;

    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 3.5f;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float airControl = 0.8f;
    [SerializeField] private float airDrag = 0.98f;
    [SerializeField] private float sprintTransitSpeed = 5f;
    [SerializeField] private float turningSpeed = 2f * Options.sensitivity;
    [SerializeField] private float gravity = 9f;
    [SerializeField] private float baseGravity = 9f;
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] public bool Grounded = false;
    [SerializeField] public int stamina = 25;
    [SerializeField] private Pause _pause;
    [SerializeField] private float height;
    private float staminaTimer = 0.5f;
    private float staminaTimer2 = 0f;

    private Vector3 velocity;
    private Vector3 move;

    [SerializeField] private float verticalVelocity;
    private float speed;

    [Header("Input")]
    private float moveInputX;
    private float moveInputZ;
    private float mouseX;
    private float mouseY;
    private float verticalRotation = 0f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        InputManagement();
        Movement();
        Test();
        GroundCheck();
    }

    private void Movement()
    {
        if (controller.isGrounded)
        {
            GroundMovement();
        }
        else
        {
            AirMovement();
        }
        Turn();
    }

    private void GroundMovement()
    {
        sprintSpeed = walkSpeed * 2;
        move = new Vector3(moveInputX, 0, moveInputZ).normalized;
        move = transform.TransformDirection(move);

        bool isMoving = moveInputX != 0 || moveInputZ != 0;

        if (Input.GetKey(KeyCode.LeftShift) && isMoving && stamina > 0)
        {
            speed = Mathf.Lerp(speed, sprintSpeed, sprintTransitSpeed * Time.deltaTime);
            animator.SetTrigger("IsRunning");
            staminaTimer += Time.deltaTime;
            
            if(staminaTimer >= 0.5f)
            {
                stamina -= 1;
                staminaTimer = 0;
            }
        }
        else if (isMoving)
        {
            speed = Mathf.Lerp(speed, walkSpeed, sprintTransitSpeed * Time.deltaTime);
            animator.SetTrigger("IsWalking");
        }
        else if (!Input.GetButton("Jump"))
        {
            animator.SetTrigger("Idle");
            staminaTimer += Time.deltaTime;
            if (staminaTimer >= 2f)
            {
                staminaTimer2 += Time.deltaTime;
                if (staminaTimer2 >= 0.2 && stamina < 25)
                {
                    stamina += 1;
                    staminaTimer2 = 0;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            staminaTimer = 0.5f;
        }

        move.y = VerticalForceCalculation();

        velocity = move * speed;

        velocity.y = VerticalForceCalculation();

        controller.Move(velocity * Time.deltaTime);
    }

    private void AirMovement()
    {
        move = new Vector3(moveInputX, 0, moveInputZ);
        move = transform.TransformDirection(move);

        velocity.x *= airDrag;
        velocity.z *= airDrag;

        velocity += move * speed * airControl * Time.deltaTime;

        velocity.y = VerticalForceCalculation();

        controller.Move(velocity * Time.deltaTime);
    }

    private void Turn()
    {
        if (_pause.isPaused == false)
        {
            transform.Rotate(Vector3.up * mouseX * turningSpeed);
            verticalRotation -= mouseY * turningSpeed;
            verticalRotation = Mathf.Clamp(verticalRotation, -90f, 60f);

            camera.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }

    private float VerticalForceCalculation()
    {
        if (Input.GetButton("Jump"))
        {
            gravity = baseGravity/2;
        }
        else
        {
            gravity = baseGravity;
        }

        if (Grounded == true && stamina > 0)
        {
            if (Input.GetButton("Jump"))
            {
                verticalVelocity = Mathf.Sqrt(jumpForce * gravity * 2);
                animator.SetTrigger("Jump");
            }
            else
            {
                verticalVelocity = -1f;
            }
            if (Input.GetButtonDown("Jump"))
            {
                stamina -= 5;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        return verticalVelocity;
    }

    private bool GroundCheck()
    {
        Vector3 playerPosition = new Vector3 (transform.position.x, transform.position.y - height, transform.position.z);

        Collider[] colliders = Physics.OverlapSphere(playerPosition, 0.1f);
        foreach(Collider c in colliders)
        {
            if (colliders.Length > 0)
            {
                Grounded = true;
            }
        }
        if(colliders.Length == 0)
        {
            Grounded = false;
        }
        return Grounded;
    }

    private void InputManagement()
    {
        moveInputZ = Input.GetAxis("Vertical");
        moveInputX = Input.GetAxis("Horizontal");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
    }


    private void Test()
    {
        if (Input.GetKeyDown("1"))
        {
            sprintTransitSpeed = 5f;
            walkSpeed = 3.5f;
            jumpForce = 1f;
            baseGravity = 9f;
            turningSpeed = 2f;
        }
        if (Input.GetKeyDown("2"))
        {
            sprintTransitSpeed = 3f;
            walkSpeed = 3.5f;
            jumpForce = 1f;
            baseGravity = 9f;
            turningSpeed = 2f;
        }
        if (Input.GetKeyDown("3"))
        {
            sprintTransitSpeed = 5f;
            walkSpeed = 5f;
            jumpForce = 1f;
            baseGravity = 9f;
            turningSpeed = 2f;
        }
        if (Input.GetKeyDown("4"))
        {
            sprintTransitSpeed = 5f;
            walkSpeed = 3.5f;
            jumpForce = 1.5f;
            baseGravity = 9f;
            turningSpeed = 2f;
        }
        if (Input.GetKeyDown("5"))
        {
            sprintTransitSpeed = 5f;
            walkSpeed = 3.5f;
            jumpForce = 1f;
            baseGravity = 15f;
            turningSpeed = 2f;
        }
        if (Input.GetKeyDown("6"))
        {
            sprintTransitSpeed = 5f;
            walkSpeed = 3.5f;
            jumpForce = 1f;
            baseGravity = 9f;
            turningSpeed = 4f;
        }
    }
}

    
