using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Patrol : IState
{
    private EnemyReferences enemyReferences;
    private PatrolPath patrolPath;
    private NavPoint targetNavPoint;

    public EnemyState_Patrol(EnemyReferences enemyReferences, PatrolPath patrolPath) {
        this.enemyReferences = enemyReferences;
        this.patrolPath = patrolPath;
    }

    public void OnEnter() {
        enemyReferences.animator.SetBool("Patrol", true);
        targetNavPoint = patrolPath.StartNavPoint();
        enemyReferences.navMesh.SetDestination(targetNavPoint.transform.position);
    }

    public void OnExit() {
        enemyReferences.animator.SetBool("Patrol", false);
    }

    public void Tick() {
        if (Vector3.Distance(targetNavPoint.transform.position, enemyReferences.transform.position) < 1.0f)
        {
            targetNavPoint = patrolPath.NextNavPoint(targetNavPoint);
            enemyReferences.navMesh.SetDestination(targetNavPoint.transform.position);
        }

        if (!enemyReferences.walkSound.isPlaying)
        {
            enemyReferences.walkSound.Play();
        }
    }

    public Color GizmoColor() {
        return Color.green;
    }
}
