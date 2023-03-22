using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float slideSpeed = 10f;
    public float crouchHeight = 1f;
    public float slideDuration = 2f;

    private Rigidbody rb;
    private bool isCrouched = false;
    private bool isSliding = false;
    private float slideTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftControl))
        {
            if (!isSliding)
            {
                // if the player is pressing both left shift and left control keys and not currently sliding, crouch the player's capsule and start sliding
                isSliding = true;
                isCrouched = true;
                slideTimer = 0f;
                transform.localScale = new Vector3(transform.localScale.x, crouchHeight, transform.localScale.z);
            }
        }
        else
        {
            if (isSliding)
            {
                // if the player is not pressing both left shift and left control keys and currently sliding, stop sliding and uncrouch the player's capsule
                isSliding = false;
                isCrouched = false;
                transform.localScale = new Vector3(transform.localScale.x, 2f, transform.localScale.z);
            }
        }

        if (isSliding)
        {
            // if the player is currently sliding, increase the slide timer and add a forward force to the player's rigidbody
            slideTimer += Time.deltaTime;
            if (slideTimer <= slideDuration)
            {
                rb.AddForce(transform.forward * slideSpeed);
            }
            else
            {
                // if the slide duration is over, stop sliding and uncrouch the player's capsule
                isSliding = false;
                isCrouched = false;
                transform.localScale = new Vector3(transform.localScale.x, 2f, transform.localScale.z);
            }
        }
    }

    void FixedUpdate()
    {
        // move the player based on the input
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);

        if (isCrouched)
        {
            // if the player is crouched, move slower
            movement *= moveSpeed / 2f;
        }
        else if (isSliding)
        {
            // if the player is sliding, move faster
            movement *= moveSpeed * 2f;
        }
        else
        {
            // if the player is not crouched or sliding, move at the normal speed
            movement *= moveSpeed;
        }

        rb.AddForce(movement, ForceMode.Impulse);
    }
}
