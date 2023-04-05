using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScript : MonoBehaviour
{
    [SerializeField] private Collider playerCollider;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].otherCollider.gameObject.CompareTag("EnemyBullet"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
