using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAlarmArea : MonoBehaviour
{
    public Vector3 boxSize;
    public LayerMask enemyLayer;


    public void alarmEnemies() {
        Collider[] hitColliders = Physics.OverlapBox(transform.position, boxSize, transform.rotation, enemyLayer);

        Debug.Log("Alarmed enemies: " + hitColliders);

        foreach (var collider in hitColliders) {
            collider.gameObject.GetComponent<AiController>().alarm();
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
}
