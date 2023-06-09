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

        Debug.Log("other collider tag: " + otherCollider.tag);

        if ((otherCollider.CompareTag("Bullet") || otherCollider.CompareTag("Blade") || otherCollider.CompareTag("Throwable")))
        {
            Material otherMaterial = otherCollider.GetComponent<Renderer>().sharedMaterial;

            Debug.Log(" name: " + otherMaterial.name + " expected: " + material.name);

            if (otherMaterial.name == material.name) {
                GetComponentInChildren<Renderer>().material.color = Color.white;
            }
            
        }
    }

    public void onHitBySword(Component sender, object data)
    {
        if (data is GameObject && (GameObject) data == gameObject)
        {
            Material material = GetComponentInChildren<Renderer>().sharedMaterial;
            Material otherMaterial = sender.GetComponentInChildren<Renderer>().sharedMaterial;
            Debug.Log("thisMaterial: " + material + "  otherMaterial: " + otherMaterial);
            if (material.name == otherMaterial.name || material.color == Color.white)
            {
                Debug.Log("Hit by sword (HitEnemyScript)");
                GetComponentInChildren<Renderer>().material.color = Color.white;
            }
        }
    }
}
