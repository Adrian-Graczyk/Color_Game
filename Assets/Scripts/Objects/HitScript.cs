using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScript : MonoBehaviour
{
    private Material material;
    
    void Start()
    {
        material = GetComponent<Renderer>().sharedMaterial;
    }


    private void OnCollisionEnter(Collision collision)
    {
        Collider otherCollider = collision.contacts[0].otherCollider;
        Collider thisCollider = collision.contacts[0].thisCollider;

        Debug.Log("other collider tag: " + otherCollider.tag);

        if (otherCollider.CompareTag("Bullet") || otherCollider.CompareTag("Blade") || otherCollider.CompareTag("Throwable"))
        {
            Material otherMaterial = otherCollider.GetComponent<Renderer>().sharedMaterial;

            Debug.Log(" name: " + otherMaterial.name + " expected: " + material.name);

            if (otherMaterial.name == material.name) {
                collision.contacts[0].thisCollider.GetComponent<Renderer>().material.color = Color.white;
            }
        }
        
        if((thisCollider.CompareTag("ColorObject") || thisCollider.CompareTag("Throwable")) && otherCollider.CompareTag("Blade"))
        { 
            collision.contacts[0].otherCollider.GetComponent<Renderer>().material = material;
        }

    }
}
