using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollScript : MonoBehaviour
{
    private bool isRagdollActive = false;
    private Rigidbody[] rigidBodies;
    private Animator animator;
    private MonoBehaviour[] otherScripts;

    private void Start()
    {
        // Get all the rigidbodies of the character
        rigidBodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb in rigidBodies)
        {
            rb.interpolation = RigidbodyInterpolation.Interpolate;
        }

        // Get the animator component of the character
        animator = GetComponent<Animator>();

        // Get all other scripts on the same game object
        otherScripts = GetComponents<MonoBehaviour>();

        // Disable all the rigidbodies at start
        SetRigidbodyState(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision was with a game object with tag "bullet"
        if (collision.gameObject.CompareTag("Bullet") && !isRagdollActive)
        {
            GetComponent<BoxCollider>().enabled = false;
            // Enable ragdoll
            SetScriptsState(true);
            SetRigidbodyState(true);
            animator.enabled = false;
            isRagdollActive = true;

        }



    }

    private void SetRigidbodyState(bool state)
    {
        foreach (Rigidbody rb in rigidBodies)
        {
            rb.isKinematic = !state;
        }
    }

    private void SetScriptsState(bool state)
    {
        foreach (MonoBehaviour script in otherScripts)
        {
            // Don't disable the current script or the ragdoll script
            if (script.GetType() == typeof(EnemyShoot))
            {
                script.enabled = !state;
            }
        }
    }
}
