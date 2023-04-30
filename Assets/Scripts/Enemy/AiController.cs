using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController : MonoBehaviour
{
    public Transform target;
    
    private float rotationTime = 0.2f;
    private EnemyReferences enemyReferences;
    private float shootingDistance;
    private float pathUpdateDeadline;

    void Awake()
    {
        enemyReferences = GetComponent<EnemyReferences>();
    }

    void Start()
    {
        shootingDistance = enemyReferences.navMesh.stoppingDistance;
    }

    
    void Update()
    {
        if (target != null)
        {
            bool inRange = Vector3.Distance(transform.position, target.position) <= shootingDistance;
            Debug.Log("Target distance: " + Vector3.Distance(transform.position, target.position));
            
            if (inRange) {
                LookAtTarget();
            } else {
                UpdatePath();
            }

            enemyReferences.animator.SetBool("Shoot", inRange);
            enemyReferences.animator.SetBool("Walk", !inRange);
        }
    }

    private void LookAtTarget()
    {
        Vector3 lookPos = target.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationTime);
    }

    private void UpdatePath()
    {
        if (Time.time >= pathUpdateDeadline)
        {
            Debug.Log("Updating path");
            pathUpdateDeadline = Time.time + enemyReferences.pathUpdateDelay;
            enemyReferences.navMesh.SetDestination(target.position);
        }
    }
}
