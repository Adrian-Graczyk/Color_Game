using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField] private Transform orientation;
    [SerializeField] private PlayerMovement playerMovement;

    public float dashForce = 5f; // force of dash
    public float dashTime = 0.2f; // duration of dash
    public float dashCooldownTime = 3f; // time between dashes

    public bool canDash = true; // flag to check if player can dash
    private Vector3 dashDirection; // direction to dash
    private float dashCooldown; // time when player can dash again

    Rigidbody rb;

    [SerializeField] private AudioSource dashSound;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (canDash && playerMovement.isGrounded)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                if (Input.GetKeyDown(KeyCode.LeftAlt))
                {
                    dashSound.Play();

                    // set direction and start coroutine for dash
                    dashDirection = GetDashDirection();
                    StartCoroutine(DashCoroutine());

                    // disable dashing and set cooldown time
                    canDash = false;
                    dashCooldown = Time.time + dashCooldownTime;

                }
            }
        }
        else
        {
            // check if cooldown has ended
            if (Time.time >= dashCooldown)
            {
                canDash = true;
            }
        }
    }

    IEnumerator DashCoroutine()
    {
        float startTime = Time.time;
        Vector3 startPos = transform.position;

        // move player in dash direction over duration of dashTime
        while (Time.time < startTime + dashTime)
        {
            float t = (Time.time - startTime) / dashTime;
            //transform.position = startPos + dashDirection * Mathf.Lerp(0f, dashDistance, t);
            rb.AddForce(dashDirection * dashForce, ForceMode.Force);
            yield return null;
        }
    }

    private Vector3 GetDashDirection()
    {
        Vector3 direction = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            direction += orientation.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction -= orientation.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction -= orientation.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += orientation.right;
        }
        return direction.normalized;
    }
}
