using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemyScript : MonoBehaviour
{
    private Material material;
    
    void Start()
    {
        material = GetComponentInChildren<Renderer>().sharedMaterial;
        Debug.Log(material);
    }


    private void OnCollisionEnter(Collision collision)
    {
        Collider otherCollider = collision.contacts[0].otherCollider;
        Collider thisCollider = collision.contacts[0].thisCollider;
        Material otherMaterial = otherCollider.GetComponent<Renderer>().sharedMaterial;

        Debug.Log("tag: " + otherCollider.tag + " name: " + otherMaterial.name + " expected: " + material.name);


        if ((otherCollider.CompareTag("Bullet") || otherCollider.CompareTag("Blade")))
        {
            Debug.Log("SFAGFASFASFAS");
            collision.contacts[0].thisCollider.GetComponentInChildren<Renderer>().material.color = Color.white;
        }

    }
}
