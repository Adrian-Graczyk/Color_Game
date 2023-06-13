using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour
{
    [Header("Events")]
    public GameEvent onPlayerDeath;

    [SerializeField] private Collider playerCollider;
    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.contacts[0].otherCollider.gameObject;

        if (other.CompareTag("EnemyBullet") || other.CompareTag("EnemyBlade"))
        {
            onPlayerDeath.Raise(this, null);
        }
    }   

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("EnemyBlade")) {
            onPlayerDeath.Raise(this, null);
        }
    }
}
