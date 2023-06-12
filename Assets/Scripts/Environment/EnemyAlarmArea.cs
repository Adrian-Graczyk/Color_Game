using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyAlarmArea : MonoBehaviour
{
    public Vector3 boxSize;
    public LayerMask enemyLayer;

    private bool isAlarmed = false;

    public void alarmEnemies() {
        Physics.OverlapBox(transform.position, boxSize / 2.0f, transform.rotation, enemyLayer)
            .Select(collider => collider.gameObject.GetComponent<AiController>())
            .Where(aiController => aiController != null)
            .ToList()
            .ForEach(aiController =>
            {
                Debug.Log("Alarming: " + aiController.gameObject.name);
                aiController.alarm();
            });
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
}
