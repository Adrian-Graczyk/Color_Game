using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsEnemyPickUp : MonoBehaviour
{
    private bool pickedUpAlready;
    private GameObject enemy;
    private Material enemyMaterial;


    [Header("Events")]
    public GameEvent onBulletPickUp;

    void Start()
    {
        enemy = transform.parent.gameObject;

        pickedUpAlready = false;
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && enemy.GetComponent<RagdollScript>().isDead && !pickedUpAlready)
        {
            onBulletPickUp.Raise(this, true);
            pickedUpAlready = true;
        }
    }
}
