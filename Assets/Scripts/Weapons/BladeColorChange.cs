using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeColorChange : MonoBehaviour
{
    public void setBladeMaterial(Material material)
    {
        GetComponent<Renderer>().sharedMaterial = material;
    }

    public Material getBladeMaterial()
    {
        return GetComponent<Renderer>().sharedMaterial;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider otherCollider = collision.contacts[0].otherCollider;

        if (otherCollider.CompareTag("ColorObject") || otherCollider.CompareTag("Throwable") && otherCollider.gameObject.layer == LayerMask.NameToLayer("ColorObject"))
        {
            Material otherMaterial = otherCollider.GetComponent<Renderer>().sharedMaterial;

            GetComponent<Renderer>().sharedMaterial = otherMaterial;
        }
    }
}
