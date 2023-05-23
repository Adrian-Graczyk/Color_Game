using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
public class EnemyReferences : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent navMesh;
    [HideInInspector] public Animator animator;

    [Header("Stats")]
    public float pathUpdateDelay = 0.2f;
    public float viewRange = 20f;
    public float shootingRange = 15f;
    public float viewAngle = 90;
    public float rotationTime = 0.2f;
    public float timeBetweenAttacks = 1.0f;
    public float detectionTime = 5.0f;

    [Header("EnemyAlarmArea")]
    public EnemyAlarmArea? enemyAlarmArea;

    [Header("Gun projectiles")]
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public float projectileSpeed = 10.0f;
    public float projectileLifetime = 2.0f;

    [Header("Sword")]
    public BoxCollider swordCollider;

    private void Awake()
    {
        navMesh = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (swordCollider != null) {
            swordCollider.enabled = false;
        }
    }
}
