using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPickup : MonoBehaviour
{
    public float pickupRange = 1.5f;
    public LayerMask bulletLayer;
    public BoxCollider pickupCollider;

    public KeyCode bulletPickupKey = KeyCode.Mouse1;

    [Header("Events")]
    public GameEvent onBulletPickUp;


    void Update()
    {
        if (Input.GetKeyDown(bulletPickupKey))
        {
            // Collider[] colliders = Physics.OverlapBox(pickupCollider.transform.position,
            //                                           pickupCollider.bounds.size,
            //                                           pickupCollider.transform.rotation,
            //                                           bulletLayer);
            // Debug.Log("Colliders: " + colliders.Length);

            // foreach (Collider collider in colliders)
            // {
            //     Debug.Log("Bullet in pickup range");
            //     onBulletPickUp.Raise(this, collider.GetComponent<Renderer>().sharedMaterial);
            //     Destroy(collider.gameObject);
            // }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (Vector3.Distance(ray.origin, hitInfo.point) > pickupRange) {
                    return;
                }

                Collider hitCollider = hitInfo.collider;

                if (hitCollider.CompareTag("Bullet")) {
                    onBulletPickUp.Raise(this, hitCollider.GetComponent<Renderer>().sharedMaterial);
                    Destroy(hitCollider.gameObject);
                }
            }
        }
    }
}
