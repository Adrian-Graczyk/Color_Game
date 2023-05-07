using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderScript : MonoBehaviour
{
    public float climbSpeed = 3f;   // Speed at which the player will climb the ladder

    private GameObject player;      // Reference to the player object
    private bool isClimbing = false;  // Indicates whether the player is currently climbing
    private float inputVertical = 0f; // Vertical input from player

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform.parent.gameObject;
            isClimbing = true;
            // Prevent the player from falling down while on the ladder
            player.GetComponent<Rigidbody>().useGravity = false;
            player.GetComponent<WallRun>().enabled=false;
            player.GetComponent<PlayerMovement>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isClimbing = false;
            // Enable gravity again once the player is off the ladder
            player.GetComponent<Rigidbody>().useGravity = true;
            player.GetComponent<WallRun>().enabled=true;
            player.GetComponent<PlayerMovement>().enabled = true;
        }
    }

    private void Update()
    {
        if (isClimbing)
        {
            // Get vertical input from the player
            inputVertical = Input.GetAxisRaw("Vertical");
            // Move the player up or down based on input
            player.transform.Translate(Vector3.up * inputVertical * climbSpeed * Time.deltaTime);
        }
    }

}
