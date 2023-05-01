using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Idle : IState
{
    private EnemyReferences enemyReferences;

    public EnemyState_Idle(EnemyReferences enemyReferences) {
        this.enemyReferences = enemyReferences;
    }

    public void OnEnter() {
    }

    public void OnExit() {

    }

    public void Tick() {

    }

    public Color GizmoColor() {
        return Color.grey;
    }
}
