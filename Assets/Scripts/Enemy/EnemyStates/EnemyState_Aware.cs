using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Aware : IState
{
    private EnemyReferences enemyReferences;
    private Transform target;


    public EnemyState_Aware(EnemyReferences enemyReferences, Transform target) {
        this.enemyReferences = enemyReferences;
        this.target = target;
    }

    public void OnEnter() {
        enemyReferences.animator.SetBool("Aware", true);
        enemyReferences.navMesh.SetDestination(enemyReferences.transform.position);
        alarmEnemies();
    }

    public void OnExit() {
        enemyReferences.animator.SetBool("Aware", false);
    }

    public void Tick() {
        Vector3 lookPos = target.position - enemyReferences.transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        enemyReferences.transform.rotation = Quaternion.Slerp(enemyReferences.transform.rotation, rotation, enemyReferences.rotationTime);
    }

    public Color GizmoColor() {
        return Color.magenta;
    }

    private void alarmEnemies() {
        if (enemyReferences.enemyAlarmArea != null) {
           enemyReferences.enemyAlarmArea.alarmEnemies();
        }
    }
}
