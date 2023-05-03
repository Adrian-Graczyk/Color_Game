using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AiController : MonoBehaviour
{
    public PatrolPath? patrolPath;
    public bool enableDetection = false;

    private EnemyReferences enemyReferences;
    private StateMachine fsm;
    private bool hasPatrolPath;
    private Transform playerTarget;


    void Start()
    {
        enemyReferences = GetComponent<EnemyReferences>();
        fsm = new StateMachine();

        hasPatrolPath = patrolPath != null;
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;

        // STATES
        var idle = new EnemyState_Idle(enemyReferences);
        var patrol = new EnemyState_Patrol(enemyReferences, patrolPath);
        var detect = new EnemyState_Detect(enemyReferences, playerTarget);
        var chase = new EnemyState_Chase(enemyReferences, playerTarget);
        var attack = new EnemyState_Attack(enemyReferences, playerTarget);

        // TRANSITIONS
        At(idle, patrol, () => hasPatrolPath && !isTargetInSight(playerTarget));
        At(idle, detect, () => enableDetection && isTargetInSight(playerTarget));
        At(idle, chase, () => !enableDetection && isTargetInSight(playerTarget));
        At(idle, attack, () => !enableDetection && isTargetInShootingRange(playerTarget) && isTargetInSight(playerTarget));
        At(patrol, chase, () => !enableDetection && isTargetInSight(playerTarget));
        At(patrol, detect, () => enableDetection && isTargetInSight(playerTarget));
        At(detect, chase, () => detect.TargetDetected());
        At(detect, idle, () => !isTargetInSight(playerTarget));
        At(chase, attack, () => isTargetInShootingRange(playerTarget) && isTargetInSight(playerTarget));
        At(attack, chase, () => !isTargetInShootingRange(playerTarget) && !isTargetInSight(playerTarget));
        At(attack, idle, () => !isTargetInShootingRange(playerTarget) && !isTargetInSight(playerTarget));

        // START STATE
        fsm.SetState(idle);

        // FUNCTIONS & CONDITIONS
        void At(IState from, IState to, Func<bool> condititon) => fsm.AddTransition(from, to, condititon);
        void Any(IState to, Func<bool> condititon) => fsm.AddAnyTransition(to, condititon);
    }

    void Update()
    {
        fsm.Tick();
    }

    private void OnDrawGizmos() {
        if (fsm != null) {
            Gizmos.color = fsm.GetGizmoColor();
            Gizmos.DrawSphere(transform.position + Vector3.up * 3, 0.4f);
        }
    }

    private bool isTargetInSight(Transform target) {
       if (Vector3.Distance(enemyReferences.transform.position, target.position) < enemyReferences.viewRange)
        {
            Vector3 targetDirection = (target.position - enemyReferences.transform.position).normalized;
            float angleTotarget = Vector3.Angle(enemyReferences.transform.forward, targetDirection);

            if (angleTotarget < enemyReferences.viewAngle / 2)
            {
                int layerMask = ~(1 << LayerMask.NameToLayer("Enemy")); // ignore Enemy layer
                if (Physics.Linecast(enemyReferences.transform.position, target.position, out RaycastHit hit, layerMask))
                {
                    Debug.Log("Can see: " + hit.collider.tag + " layer: " + hit.collider.gameObject.layer);
                    return hit.collider.CompareTag("Player");
                }
            }
        }

        return false;
    }

    private bool isTargetInShootingRange(Transform target) {
        return Vector3.Distance(target.position, enemyReferences.transform.position) < enemyReferences.shootingRange;
    }
}
