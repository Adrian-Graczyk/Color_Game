using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Chase : IState
{
    private EnemyReferences enemyReferences;
    private Transform target;

    public EnemyState_Chase(EnemyReferences enemyReferences, Transform target) {
        this.enemyReferences = enemyReferences;
        this.target = target;
    }

    public void OnEnter() {
        enemyReferences.animator.SetBool("Chase", true);
    }

    public void OnExit() {
        enemyReferences.animator.SetBool("Chase", false);
    }

    public void Tick() {
        enemyReferences.navMesh.SetDestination(target.position);
    }

    public Color GizmoColor() {
        return Color.yellow;
    }
}
