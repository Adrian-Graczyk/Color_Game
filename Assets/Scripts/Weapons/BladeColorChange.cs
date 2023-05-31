using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeColorChange : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Collider otherCollider = collision.contacts[0].otherCollider;
        Material otherMaterial = otherCollider.GetComponent<Renderer>().sharedMaterial;

        if (otherCollider.CompareTag("ColorObject") || otherCollider.CompareTag("Throwable") && otherCollider.gameObject.layer == LayerMask.NameToLayer("ColorObject"))
        {
            GetComponent<Renderer>().sharedMaterial = otherMaterial;
        }

    }
}
