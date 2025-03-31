using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    private CharacterController controller;
    [SerializeField] private new Transform camera;

    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float turningSpeed = 2f;
    [SerializeField] private float gravity = 6f;
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private float acceleration = 2f;
    [SerializeField] private float decceleration = 0.5f;
    [SerializeField] private float currentSpeed;
    private Vector3 move;
    private Vector3 jump;

    [SerializeField] private float verticalVelocity;

    [Header("Input")]
    private float moveInput;
    private float turnInput;
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
    }

    private void Movement()
    {
        GroundMovement();
        Turn();
    }
    
    private void GroundMovement()
    {
        Vector3 moveDirection = new Vector3(turnInput, 0, moveInput).normalized;
        Vector3 move = new Vector3(0, 0, 0);

        if (controller.isGrounded)
        {
            move = new Vector3(moveDirection.x * currentSpeed, 0, moveDirection.z * currentSpeed);
        }

        move = transform.TransformDirection(move);

        Vector3 jump = new(0, VerticalForceCalculation(), 0);
       
        //move *= walkSpeed;

        if(moveInput != 0 || turnInput != 0)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, walkSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, decceleration * Time.deltaTime);
        }

        controller.Move(move * Time.deltaTime);
        controller.Move(jump * Time.deltaTime);
    }

    private void Turn()
    {
        transform.Rotate(Vector3.up * mouseX * turningSpeed);
        verticalRotation -= mouseY * turningSpeed;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f); // Prevent flipping

        camera.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }

    private float VerticalForceCalculation()
    {
        if (controller.isGrounded)
        {
            verticalVelocity = -1f;

            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = Mathf.Sqrt(jumpForce * gravity * 2);
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        return verticalVelocity;
    }
    private void InputManagement()
    {
        moveInput = Input.GetAxisRaw("Vertical");
        turnInput = Input.GetAxisRaw("Horizontal");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

    }

    private void Test()
    {
        if (Input.GetKeyDown("1"))
        {
            acceleration = 2f;
            walkSpeed = 5f;
            jumpForce = 1f;
            gravity = 6f;
            turningSpeed = 2f;
        }
        if (Input.GetKeyDown("2"))
        {
            acceleration = 1f;
            walkSpeed = 5f;
            jumpForce = 1f;
            gravity = 6f;
            turningSpeed = 2f;
        }
        if (Input.GetKeyDown("3"))
        {
            acceleration = 2f;
            walkSpeed = 10f;
            jumpForce = 1f;
            gravity = 6f;
            turningSpeed = 2f;
        }
        if (Input.GetKeyDown("4"))
        {
            acceleration = 2f;
            walkSpeed = 5f;
            jumpForce = 1.5f;
            gravity = 6f;
            turningSpeed = 2f;
        }
        if (Input.GetKeyDown("5"))
        {
            acceleration = 2f;
            walkSpeed = 5f;
            jumpForce = 1f;
            gravity = 12f;
            turningSpeed = 2f;
        }
        if (Input.GetKeyDown("6"))
        {
            acceleration = 2f;
            walkSpeed = 5f;
            jumpForce = 1f;
            gravity = 6f;
            turningSpeed = 4f;
        }
    }
}
