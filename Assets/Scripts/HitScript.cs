using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScript : MonoBehaviour
{
    private Material material;
    
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].otherCollider.tag.CompareTo("Bullet") == 0 &&
            collision.contacts[0].otherCollider.GetComponent<Renderer>().material.name == material.name)
        {
            collision.contacts[0].thisCollider.GetComponent<Renderer>().material.color = Color.white;
        }   
    }
}
