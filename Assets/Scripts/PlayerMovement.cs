using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float playerHeight = 1f;

    [SerializeField] Transform orientation;

    [Header("Camera")]
    [SerializeField] private Camera cam;
    [SerializeField] float fov = 40f;
    [SerializeField] float slidefov = 70f;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float airMultiplier = 0.4f;
    float movementMultiplier = 10f;

    [Header("Sprinting")]
    [SerializeField] float walkSpeed = 6f;
    [SerializeField] float sprintSpeed = 10f;
    [SerializeField] float acceleration = 10f;
    private bool isSprinting = false;

    [Header("Jumping")]
    public float jumpForce = 5f;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] KeyCode crouchKey = KeyCode.C;

    [Header("Drag")]
    [SerializeField] float groundDrag = 6f;
    [SerializeField] float airDrag = 2f;

    float horizontalMovement;
    float verticalMovement;

    [Header("Ground Detection")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundDistance = 0.2f;

    [Header("Crouching")]
    [SerializeField] float crouchHeightMultiplier = 0.5f;
    [SerializeField] float crouchSpeed = 3f;
    private bool isCrouched = false;

    [Header("Sliding")]
    [SerializeField] float slideDuration = 2f;
    [SerializeField] float slideSpeed = 10f;
    private bool isSliding = false;
    private float slideTimer = 0f;
    public bool isGrounded { get; private set; }
    

    Vector3 moveDirection;
    Vector3 slopeMoveDirection;

    Rigidbody rb;

    RaycastHit slopeHit;

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        MyInput();
        ControlDrag();
        ControlSpeed();

        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }

        if (Input.GetKey(sprintKey) && isGrounded)
        {
            Sprint();
            if (Input.GetKeyDown(crouchKey))
            {
                Crouch();
                isSliding = true;
            }
        }
        else isSprinting = false;
  
        if (Input.GetKeyDown(crouchKey) && isGrounded && !isSprinting && !isSliding)
        {
            if (!isCrouched)
            {
                Crouch();
            }
            else
            {
                StandUp();
            }
        }

        if (isSliding)
        {
           Slide();
        }

            slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }

    void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
    }

    void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    void ControlSpeed()
    {
        if (isSprinting && isGrounded && !isCrouched)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
        }
        else if (isCrouched && isGrounded)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, crouchSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
        }
    }

    void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }

    void Sprint()
    {
        isSprinting = true;
    }

    void Crouch()
    {
        isCrouched = true;
        transform.localScale = new Vector3(transform.localScale.x, crouchHeightMultiplier * playerHeight, transform.localScale.z);
        transform.position = new Vector3(transform.position.x, playerHeight * (1-crouchHeightMultiplier), transform.position.z);
    }

    void StandUp()
    {
        isCrouched = false;
        transform.localScale = new Vector3(transform.localScale.x, 1f, transform.localScale.z);
    }

    void Slide()
    {
        
        // if the player is currently sliding, increase the slide timer and add a forward force to the player's rigidbody
        slideTimer += Time.deltaTime;
        if (slideTimer <= slideDuration)
        {
            rb.AddForce(moveDirection * slideSpeed);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, slidefov, slideDuration * Time.deltaTime);
        }
        else
        {
            // if the slide duration is over, stop sliding and uncrouch the player's capsule
            isSliding = false;
            slideTimer = 0f;
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, slideDuration * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (isGrounded && OnSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }
    }
}