using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        target = PlayerFinder.playerGameObject().transform;
        enemyReferences.animator.SetBool("Attack", true);
        enemyReferences.navMesh.SetDestination(enemyReferences.transform.position);
        alarmEnemies();
    }

    public void OnExit() {
        enemyReferences.animator.SetBool("Attack", false);
    
        if (enemyReferences.swordCollider != null) {
            enemyReferences.swordCollider.enabled = false;
        }
    }

    public void Tick() {
        //  workaround because in OnEnter it is not always called
        if (enemyReferences.swordCollider != null) {
            enemyReferences.swordCollider.enabled = true;
        }

        Vector3 lookPos = target.position - enemyReferences.transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        enemyReferences.transform.rotation = Quaternion.Slerp(enemyReferences.transform.rotation, rotation, enemyReferences.rotationTime);

        if (Time.time > nextFireTime && enemyReferences.projectileSpawnPoint != null)
        {
            nextFireTime = Time.time + enemyReferences.timeBetweenAttacks;

            GameObject projectile = GameObject.Instantiate(enemyReferences.projectilePrefab, enemyReferences.projectileSpawnPoint.position, enemyReferences.projectileSpawnPoint.rotation);
            Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
            projectileRigidbody.velocity = (target.position - enemyReferences.projectileSpawnPoint.position) * enemyReferences.projectileSpeed;
            GameObject.Destroy(projectile, enemyReferences.projectileLifetime);

            enemyReferences.gunSound.Play();
        }
    }

    public Color GizmoColor() {
        return Color.red;
    }

    private void alarmEnemies() {
        if (enemyReferences.enemyAlarmArea != null) {
            enemyReferences.enemyAlarmArea.alarmEnemies();
        }
    }
}
