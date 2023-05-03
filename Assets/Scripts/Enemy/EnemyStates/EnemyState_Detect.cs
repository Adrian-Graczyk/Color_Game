using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Detect : IState
{
    private EnemyReferences enemyReferences;
    private Transform target;
    private float detectionDeadline;

    public EnemyState_Detect(EnemyReferences enemyReferences, Transform target) {
        this.enemyReferences = enemyReferences;
        this.target = target;
    }

    public void OnEnter() {
        enemyReferences.animator.SetBool("Detect", true);
        detectionDeadline = Time.time + enemyReferences.detectionTime;
        enemyReferences.navMesh.SetDestination(enemyReferences.transform.position);
    }

    public void OnExit() {
        enemyReferences.animator.SetBool("Detect", false);
    }

    public void Tick() {
        Vector3 lookPos = target.position - enemyReferences.transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        enemyReferences.transform.rotation = Quaternion.Slerp(enemyReferences.transform.rotation, rotation, enemyReferences.rotationTime);
    }

    public Color GizmoColor() {
        return Color.blue;
    }

    public bool TargetDetected() {
        return Time.time >= detectionDeadline;
    }
}