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

    [SerializeField] private AudioSource bulletPickUpSound;

    void Update()
    {
        if (Input.GetKeyDown(bulletPickupKey))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            RaycastHit[] hits = Physics.RaycastAll(ray, pickupRange);

            foreach (var hit in hits)
            {
                Collider hitCollider = hit.collider;

                if (hitCollider.CompareTag("Bullet")) {
                    onBulletPickUp.Raise(this, hitCollider.GetComponent<Renderer>().sharedMaterial);
                    Destroy(hitCollider.gameObject);
                    bulletPickUpSound.Play();

                    return;
                }
            }
        }
    }
}
