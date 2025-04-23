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
    public InventoryUI inventoryUI;

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
    [SerializeField] private bool grounded = false;
    [SerializeField] private float crouchSpeed = 2f;
    [SerializeField] private float crouchYScale;
    [SerializeField] private float crouchStartYScale;
    public bool crouched = false;

    private Vector3 velocity;
    private Vector3 move;

    [SerializeField] private LayerMask floor;

    [SerializeField] private float verticalVelocity;
    private float speed;

    [Header("Input")]
    private float moveInputX;
    private float moveInputZ;
    private float mouseX;
    private float mouseY;
    private float verticalRotation = 0f;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;

    private void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        crouchStartYScale = transform.localScale.y;
        crouchYScale = crouchStartYScale / 2;
    }

    private void Update()
    {
        if (!inventoryUI.inventoryVisible)
        {
            InputManagement();
            Movement();
            Test();
            GroundCheck();
        }
        if (!inventoryUI.inventoryVisible)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
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

        if (Input.GetKey(KeyCode.LeftShift) && isMoving)
        {
            speed = Mathf.Lerp(speed, sprintSpeed, sprintTransitSpeed * Time.deltaTime);
            animator.SetTrigger("IsRunning");
        }
        else if (isMoving && !crouched)
        {
            speed = Mathf.Lerp(speed, walkSpeed, sprintTransitSpeed * Time.deltaTime);
            animator.SetTrigger("IsWalking");
        }
        else if (!Input.GetButton("Jump"))
        {
            animator.SetTrigger("Idle");
        }

        if (Input.GetKey(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            speed = Mathf.Lerp(speed, crouchSpeed, sprintTransitSpeed * Time.deltaTime);
            crouched = true;
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchStartYScale, transform.localScale.z);
            crouched = false;
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
        transform.Rotate(Vector3.up * mouseX * turningSpeed);
        verticalRotation -= mouseY * turningSpeed;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 60f);

        camera.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
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

        if (grounded == true)
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
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        return verticalVelocity;
    }

    public Vector3 GetCurrentFlatVelocity()
    {
        Vector3 flatVelocity = velocity;
        flatVelocity.y = 0f;
        return flatVelocity;
    }

    private bool GroundCheck()
    {
        Vector3 playerPosition = new Vector3 (transform.position.x, transform.position.y - 0.09999847f, transform.position.z);

        Collider[] colliders = Physics.OverlapSphere(playerPosition, 0.1f, floor);
        foreach(Collider c in colliders)
        {
            if (colliders.Length > 0)
            {
                grounded = true;
            }
        }
        if(colliders.Length == 0)
        {
            grounded = false;
        }
        return grounded;
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

    
