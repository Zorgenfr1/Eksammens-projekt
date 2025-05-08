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
    public Animator arms;

    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 3.5f;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float airControl = 0.8f;
    [SerializeField] private float airDrag = 0.998f;
    [SerializeField] private float sprintTransitSpeed = 5f;
    [SerializeField] private float turningSpeed = 2f * Options.sensitivity;
    [SerializeField] private float gravity = 9f;
    [SerializeField] private float baseGravity = 9f;
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] public bool Grounded = false;
    [SerializeField] public int stamina = 50;
    [SerializeField] private Pause _pause;
    [SerializeField] private float height;
    [SerializeField] private bool crouched = false;
    [SerializeField] private bool isMoving;
    [SerializeField] Vector3 cameraCrouchedOffset = new Vector3(0, -5f, 5f);
    [SerializeField] bool changeCameraCrouch = false;
    [SerializeField] Vector3 startCameraPosition;
    [SerializeField] float cameraOffsetTime = 1.5f;
    [SerializeField] private LayerMask climbable;
    private LayerMask layerMask;
    private RaycastHit frontWallHit;
    private bool wallFront;
    private float wallLookAngle;
    Vector3 cameraTargetPosition;
    public float transitionState = 0f;
    private float staminaTimer = 0.5f;
    private float staminaTimer2 = 0f;
    bool isClimbing = false;

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
        layerMask = LayerMask.GetMask("Climbable") | LayerMask.GetMask("Default");
        cameraTargetPosition = camera.localPosition;
    }

    private void Update()
    {
        InputManagement();
        Movement();
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
        WallCheck();
    }

    private void GroundMovement()
    {
        sprintSpeed = walkSpeed * 2;
        move = new Vector3(moveInputX, 0, moveInputZ).normalized;
        move = transform.TransformDirection(move);

        isMoving = moveInputX != 0 || moveInputZ != 0;

        if (Input.GetKey(KeyCode.LeftShift) && isMoving && stamina > 0 && !crouched)
        {
            speed = Mathf.Lerp(speed, sprintSpeed, sprintTransitSpeed * Time.deltaTime);
            animator.SetTrigger("IsRunning");
            staminaTimer += Time.deltaTime;
            
            if(staminaTimer >= 0.5f)
            {
                stamina -= 2;
                staminaTimer = 0;
            }
        }
        else if (isMoving && !crouched)
        {
            speed = Mathf.Lerp(speed, walkSpeed, sprintTransitSpeed * Time.deltaTime);
            animator.SetTrigger("IsWalking");
            animator.SetBool("Crouched", false);
            animator.SetBool("Moving", true);
            GainStamina();
        }
        else if(isMoving && crouched)
        {
            speed = Mathf.Lerp(speed, walkSpeed / 2, sprintTransitSpeed * Time.deltaTime);
            animator.SetBool("Crouched", true);
            animator.SetBool("Moving", true);
            GainStamina();
        }
        else if (crouched)
        {
            animator.SetBool("Crouched", true);
            animator.SetBool("Moving", false);
            GainStamina();
        }
        else
        {
            animator.SetTrigger("Idle");
            animator.SetBool("Crouched", false);
            animator.SetBool("Moving", false);
            GainStamina();
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            staminaTimer = 0.5f;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            crouched = !crouched;
            transitionState = 0f;
            if (crouched)
            {
                cameraTargetPosition = camera.localPosition + cameraCrouchedOffset;
            }
            else
            {
                cameraTargetPosition = camera.localPosition - cameraCrouchedOffset;
            }
        }
        if (crouched)
        {
            float goonTime = Time.deltaTime * cameraOffsetTime + transitionState;
            if (goonTime > 1f)
            {
                goonTime = 1f;
            }
            camera.localPosition = Vector3.Lerp(camera.localPosition, cameraTargetPosition, goonTime);
        }
        if (!crouched)
        {
            float goonTime = Time.deltaTime * cameraOffsetTime + transitionState;
            if (goonTime > 1f)
            {
                goonTime = 1f;
            }
            camera.localPosition = Vector3.Lerp(camera.localPosition, cameraTargetPosition, goonTime);
        }

        //robin goon ahh spil

        move.y = VerticalForceCalculation();

        velocity = move * speed;

        velocity.y = VerticalForceCalculation();

        controller.Move(velocity * Time.deltaTime);
    }

    public void GainStamina()
    {
        staminaTimer += Time.deltaTime;
        if (staminaTimer >= 1f)
        {
            staminaTimer2 += Time.deltaTime;
            if (staminaTimer2 >= 0.1 && stamina < 50)
            {
                stamina += 1;
                staminaTimer2 = 0;
            }
        }
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
        if (wallFront == true && wallLookAngle < 45 && Input.GetKey(KeyCode.C))
        {
            verticalVelocity = 2f;
            if (!isClimbing)
            {
                arms.SetBool("climbing", true);
                isClimbing = true;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
            if (isClimbing)
            {
                arms.SetBool("climbing", false);
                arms.SetTrigger("Scroll 1");
                isClimbing = false;
            }
            
        }
        return verticalVelocity;
    }

    private bool GroundCheck()
    {
        Vector3 playerPosition = new Vector3 (transform.position.x, transform.position.y - height, transform.position.z);

        Collider[] colliders = Physics.OverlapSphere(playerPosition, 0.1f, layerMask);
        foreach(Collider c in colliders)
        {
            if (colliders.Length > 0 || controller.isGrounded)
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

    private void WallCheck()
    {
        wallFront = Physics.SphereCast(transform.position, 0.25f, camera.forward, out frontWallHit, 0.75f, climbable);
        wallLookAngle = Vector3.Angle(camera.forward, -frontWallHit.normal);
    }

    private void InputManagement()
    {
        moveInputZ = Input.GetAxis("Vertical");
        moveInputX = Input.GetAxis("Horizontal");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
    }


    /*private void Test()
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
    }*/
}

    
