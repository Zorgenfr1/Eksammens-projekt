using NUnit.Framework.Internal;
using Unity.Properties;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    private CharacterController controller;
    [SerializeField] private new Transform camera;

    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private float sprintTransitSpeed = 5f;
    [SerializeField] private float turningSpeed = 2f;
    [SerializeField] private float gravity = 6f;
    [SerializeField] private float jumpForce = 1f;

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
        move = new Vector3(moveInputX, 0, moveInputZ);
        move = transform.TransformDirection(move);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = Mathf.Lerp(speed, sprintSpeed, sprintTransitSpeed * Time.deltaTime);
        }
        else
        {
            speed = Mathf.Lerp(speed, walkSpeed, sprintTransitSpeed * Time.deltaTime);
        }

        move.y = VerticalForceCalculation();

        velocity = move * speed;

        velocity.y = VerticalForceCalculation();

        controller.Move(velocity * Time.deltaTime);
    }

    private void AirMovement()
    {
        velocity.x = Mathf.Lerp(velocity.x, move.x * speed, 0.1f);
        velocity.z = Mathf.Lerp(velocity.z, move.z * speed, 0.1f);

        velocity.y = VerticalForceCalculation();

        controller.Move(velocity * Time.deltaTime);
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
        moveInputZ = Input.GetAxis("Vertical");
        moveInputX = Input.GetAxis("Horizontal");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
    }
}

    
