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
        Material otherMaterial = otherCollider.GetComponent<Renderer>().sharedMaterial;

        Debug.Log("tag: " + otherCollider.tag + " name: " + otherMaterial.name + " expected: " + material.name);


        if ((otherCollider.CompareTag("Bullet") || otherCollider.CompareTag("Blade")) && otherMaterial.name == material.name && thisCollider.CompareTag("Enemy"))
        {
            collision.contacts[0].thisCollider.GetComponent<Renderer>().material.color = Color.white;
        }
        
        if(thisCollider.CompareTag("ColorObject") && otherCollider.CompareTag("Blade"))
        { 
            collision.contacts[0].otherCollider.GetComponent<Renderer>().material = material;
        }

    }
}
