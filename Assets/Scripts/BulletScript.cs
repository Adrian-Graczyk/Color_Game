using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;

    [SerializeField]
    GameObject bulletPrefab;


    private bool isStick = false;
    private GameObject target;
    private Vector3 refPos;
    private SphereCollider sphereCollider;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    
       if (isStick)
       {
            //transform.position = target.transform.position + refPos;
       }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!isStick)
        {
         
            FixedJoint joint = gameObject.AddComponent<FixedJoint>();
            // sets joint position to point of contact
            joint.anchor = collision.contacts[0].point;
            // conects the joint to the other object
            joint.connectedBody = collision.contacts[0].otherCollider.transform.GetComponentInParent<Rigidbody>();
            // Stops objects from continuing to collide and creating more joints
            joint.enableCollision = false;
            
            isStick = true;   
        }
    }


}
