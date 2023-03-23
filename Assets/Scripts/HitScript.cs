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
        Material otherMaterial = otherCollider.GetComponent<Renderer>().sharedMaterial;

        Debug.Log("tag: " + otherCollider.tag + " name: " + otherMaterial.name + " expected: " + material.name);

         
        if (otherCollider.CompareTag("Bullet") && otherMaterial.name == material.name)
        {
            collision.contacts[0].thisCollider.GetComponent<Renderer>().material.color = Color.white;
        }   
    }
}
