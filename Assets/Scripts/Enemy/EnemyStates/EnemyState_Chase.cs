using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Chase : IState
{
    private EnemyReferences enemyReferences;
    private Transform target;
    private float pathUpdateDeadline;

    public EnemyState_Chase(EnemyReferences enemyReferences, Transform target) {
        this.enemyReferences = enemyReferences;
        this.target = target;
    }

    public void OnEnter() {
        enemyReferences.animator.SetBool("Chase", true);
        alarmEnemies();
    }

    public void OnExit() {
        enemyReferences.animator.SetBool("Chase", false);
    }

    public void Tick() {
        if (enemyReferences.pathUpdateDelay <= Time.time) {
            pathUpdateDeadline = Time.time + enemyReferences.pathUpdateDelay;
            enemyReferences.navMesh.SetDestination(target.position);

            if (!enemyReferences.walkSound.isPlaying)
            {
                enemyReferences.walkSound.Play();
            }
        }
    }

    public Color GizmoColor() {
        return Color.yellow;
    }

    private void alarmEnemies() {
        if (enemyReferences.enemyAlarmArea != null) {
           enemyReferences.enemyAlarmArea.alarmEnemies();
        }
    }
}
