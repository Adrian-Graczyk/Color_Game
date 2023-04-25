using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollScript : MonoBehaviour
{
    private bool isRagdollActive = false;
    private Rigidbody[] rigidBodies;
    private Animator animator;
    private MonoBehaviour[] otherScripts;
    public bool isDead;

    private void Start()
    {
        isDead = false;
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

    public void onHitBySword(Component sender, object data)
    {
        if (data is GameObject && (GameObject) data == gameObject)
        {
            Material material = GetComponentInChildren<Renderer>().sharedMaterial;
            Material otherMaterial = sender.GetComponentInChildren<Renderer>().sharedMaterial;
            if (material.name == otherMaterial.name || material.color == Color.white)
            {
                Debug.Log("Hit by sword (RagdollScript)");
                EnableRagdoll();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Material material = GetComponentInChildren<Renderer>().sharedMaterial;
        Material otherMaterial = collision.contacts[0].otherCollider.GetComponent<Renderer>().sharedMaterial;

        // Check if the collision was with a game object with tag "bullet"
        if ((collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Blade") || collision.gameObject.CompareTag("Throwable")) && !isRagdollActive)
        {
            Debug.Log("Hit by bullet / throwable (RagdollScript)" + "Enemy color = " + material.color + ", Object color = " + otherMaterial.color);
            if (otherMaterial.color == material.color || material.color == Color.white)
            {
                Debug.Log("same material (RagdollScript)");
                EnableRagdoll();
            }
        }
    }

    private void EnableRagdoll()
    {
        GetComponent<BoxCollider>().enabled = false;
        SetScriptsState(true);
        SetRigidbodyState(true);
        animator.enabled = false;
        isRagdollActive = true;
        isDead = true;
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
