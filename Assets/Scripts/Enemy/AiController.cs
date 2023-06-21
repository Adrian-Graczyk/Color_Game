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
    private bool alarmed = false;


    void Start()
    {
        enemyReferences = GetComponent<EnemyReferences>();
        fsm = new StateMachine();

        hasPatrolPath = patrolPath != null;
        playerTarget = PlayerFinder.playerGameObject().transform;

        // STATES
        var idle = new EnemyState_Idle(enemyReferences);
        var patrol = new EnemyState_Patrol(enemyReferences, patrolPath);
        var detect = new EnemyState_Detect(enemyReferences, playerTarget);
        var aware = new EnemyState_Aware(enemyReferences, playerTarget);
        var chase = new EnemyState_Chase(enemyReferences, playerTarget);
        var attack = new EnemyState_Attack(enemyReferences, playerTarget);

        // TRANSITIONS
        At(idle, patrol, () => hasPatrolPath && !isTargetInSight(playerTarget));
        At(idle, detect, () => enableDetection && isTargetInSight(playerTarget));
        At(idle, aware, () => !enableDetection && isTargetInSight(playerTarget) || alarmed);

        At(patrol, detect, () => enableDetection && isTargetInSight(playerTarget));
        At(patrol, aware, () => !enableDetection && isTargetInSight(playerTarget) || alarmed);

        At(detect, aware, () => detect.TargetDetected());
        At(detect, idle, () => !isTargetInSight(playerTarget));

        At(aware, chase, () => !canAttackTarget(playerTarget) && isTargetReachable(playerTarget));
        At(aware, attack, () => canAttackTarget(playerTarget));

        At(chase, attack, () => canAttackTarget(playerTarget));
        At(chase, aware, () => !canAttackTarget(playerTarget) && !isTargetReachable(playerTarget));
        
        At(attack, aware, () => !canAttackTarget(playerTarget));

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

    public void alarm() {
        alarmed = true;
    }

    private void OnDrawGizmos() {
        if (fsm != null) {
            Gizmos.color = fsm.GetGizmoColor();
            Gizmos.DrawSphere(transform.position + Vector3.up * 3, 0.4f);
            // Gizmos.DrawSphere(transform.position + Vector3.up, 0.1f);  // Linecast source

            if (enemyReferences.enemyAlarmArea != null) {
                Vector3 position = enemyReferences.enemyAlarmArea.transform.position;
                Vector3 dimensions = enemyReferences.enemyAlarmArea.boxSize;
                Gizmos.DrawWireCube(position, dimensions);
            }
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
                if (Physics.Linecast(enemyReferences.transform.position + Vector3.up, target.position, out RaycastHit hit, layerMask))
                {
                    Debug.Log("Can see: " + hit.collider.tag + " layer: " + hit.collider.gameObject.layer);
                    return hit.collider.CompareTag("Player");
                }
            }
        }

        return false;
    }

    private bool isTargetInAttackRange(Transform target) {
        return Vector3.Distance(target.position, enemyReferences.transform.position) < enemyReferences.shootingRange;
    }

    private bool isTargetReachable(Transform target) {
        UnityEngine.AI.NavMeshPath path = new UnityEngine.AI.NavMeshPath();

        return enemyReferences.navMesh.CalculatePath(target.position, path) && path.status == UnityEngine.AI.NavMeshPathStatus.PathComplete;
    }

    private bool canAttackTarget(Transform target) {
        return isTargetInAttackRange(target) && isTargetInSight(target);
    }
}
