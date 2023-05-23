using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScript : MonoBehaviour
{
    [SerializeField] private Collider playerCollider;
    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.contacts[0].otherCollider.gameObject;

        if (other.CompareTag("EnemyBullet") || other.CompareTag("EnemyBlade"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }   

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("EnemyBlade")) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
