using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public bool isDestroyedOnContact = false;

    [SerializeField]
    Rigidbody rb;

    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    SphereCollider pickupCollider;


    private bool isStick = false;
    private GameObject target;
    private Vector3 refPos;
    private SphereCollider sphereCollider;
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(!gameObject.CompareTag("EnemyBullet")) {
            Physics.IgnoreCollision(GetComponent<Collider>(), GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Collider>(), true);
        }
        
        if (pickupCollider != null) {
            foreach (Collider otherCollider in FindObjectsOfType<Collider>())
            {
                Physics.IgnoreCollision(pickupCollider, otherCollider, true);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.contacts[0].otherCollider.name);
        if(!isStick && collision.contacts[0].otherCollider.tag != "Enemy" && collision.contacts[0].otherCollider.tag != "EnemyBody")
        {
            FixedJoint joint = gameObject.AddComponent<FixedJoint>();
            // sets joint position to point of contact
            joint.anchor = collision.contacts[0].point;
            // conects the joint to the other object
            joint.connectedBody = collision.contacts[0].otherCollider.transform.GetComponentInParent<Rigidbody>();
            // Stops objects from continuing to collide and creating more joints
            joint.enableCollision = false;
            
            isStick = true;

            ChangeMaterial(collision.contacts[0].otherCollider);
        }

        if (isDestroyedOnContact) {
            Destroy(gameObject);
        }
    }

    private void ChangeMaterial(Collider other)
    {
        // Get the Renderer component of the current object
        Renderer renderer = GetComponent<Renderer>();

        // Check if the collided object has a Renderer component
        Renderer otherRenderer = other.GetComponent<Renderer>();
        if (otherRenderer && (other.CompareTag("ColorObject") || other.CompareTag("Throwable")) && other.gameObject.layer == LayerMask.NameToLayer("ColorObject"))
        {
            renderer.material = otherRenderer.sharedMaterial;
        }
    }
}
