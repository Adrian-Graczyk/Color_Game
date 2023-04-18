using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPickup : MonoBehaviour
{

    [Header("Events")]
    public GameEvent onBulletPickUp;

    public float pickupRange = 1.5f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
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
