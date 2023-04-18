using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject projectilePrefab; // prefab of the projectile that the enemy will shoot
    public Transform projectileSpawnPoint; // point from where the projectile will be spawned
    public float fireRate = 1.0f; // how often the enemy will shoot (in seconds)
    public float projectileSpeed = 10.0f; // speed of the projectile
    public float projectileLifetime = 2.0f; // how long the projectile will exist before being destroyed
    public float aimDuration = 0.5f; // how long it takes to aim at the player
    public float predictionTimeAdjust = 0.7f; // value that is added to predictedTime
    public float shootingRange = 30f;

    private Rigidbody player; // reference to the player
    private Animator animator; // reference to the animator component
    private float nextFireTime; // time of the next shot

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        ChangeTags(transform);
    }

    void Update()
    {
        // check if the player is within shooting range and if it's time to shoot again
        if (Vector3.Distance(transform.position, player.position) < shootingRange && Time.time > nextFireTime)
        {
            // set the next fire time
            nextFireTime = Time.time + fireRate;

            // set the shooting animation trigger
            animator.SetTrigger("Shoot");

            // aim at the player
            StartCoroutine(AimAtPlayerSmoothly());

            // spawn the projectile
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

            // set the velocity of the projectile
            Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
            projectileRigidbody.velocity = projectileSpawnPoint.forward * projectileSpeed;

            // destroy the projectile after its lifetime expires
            Destroy(projectile, projectileLifetime);
        }
    }

    IEnumerator AimAtPlayerSmoothly()
    {
        Quaternion originalRotation = transform.rotation;
        float distance = Vector3.Distance(player.position, transform.position);
        float predictionTime = aimDuration + distance / projectileSpeed + predictionTimeAdjust;
        Vector3 predictedPlayerPosition = player.position + player.velocity * predictionTime;
        Vector3 targetDirection = predictedPlayerPosition - transform.position;
        targetDirection.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        Debug.Log("player distance: " + distance + " predicted player position: " + predictedPlayerPosition + " prediction time: " + predictionTime);

        float elapsedTime = 0.0f;
        while (elapsedTime < aimDuration)
        {
            transform.rotation = Quaternion.Slerp(originalRotation, targetRotation, elapsedTime / aimDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    void ChangeTags(Transform currentTransform)
    {   
        if (!currentTransform.gameObject.CompareTag("Enemy"))
        currentTransform.gameObject.tag = "EnemyBody";

        foreach (Transform child in currentTransform)
        {
            ChangeTags(child);
        }
    }
}
