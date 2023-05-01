using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Attack : IState
{
    private EnemyReferences enemyReferences;
    private Transform target;
    private float nextFireTime;
    
    public EnemyState_Attack(EnemyReferences enemyReferences, Transform target) {
        this.enemyReferences = enemyReferences;
        this.target = target;
    }

    public void OnEnter() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        enemyReferences.animator.SetBool("Attack", true);
        enemyReferences.navMesh.SetDestination(enemyReferences.transform.position);
    }

    public void OnExit() {
        enemyReferences.animator.SetBool("Attack", false);
    }

    public void Tick() {
        Vector3 lookPos = target.position - enemyReferences.transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        enemyReferences.transform.rotation = Quaternion.Slerp(enemyReferences.transform.rotation, rotation, enemyReferences.rotationTime);

        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + enemyReferences.timeBetweenAttacks;

            GameObject projectile = GameObject.Instantiate(enemyReferences.projectilePrefab, enemyReferences.projectileSpawnPoint.position, enemyReferences.projectileSpawnPoint.rotation);
            Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
            projectileRigidbody.velocity = enemyReferences.projectileSpawnPoint.forward * enemyReferences.projectileSpeed;
            GameObject.Destroy(projectile, enemyReferences.projectileLifetime);
        }
    }

    public Color GizmoColor() {
        return Color.red;
    }
}