using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    [SerializeField] private GameObject Sword;
    [SerializeField] private GameObject SwordBlade;
    [SerializeField] private bool canAttack =  true;
    [SerializeField] private float attackCooldown = 0.6f;
    [SerializeField] public LayerMask enemyLayer;

    [Header("Events")]
    public GameEvent onEnemyHitBySword;

    private Animator animator;
    private BoxCollider swordBladeCollider;

    [SerializeField] private AudioSource swordSwingSound;

    private void Start()
    {
        animator = Sword.GetComponent<Animator>();
        swordBladeCollider = SwordBlade.GetComponent<BoxCollider>();
        Physics.IgnoreCollision(swordBladeCollider, PlayerFinder.playerCollider(), true);

        swordBladeCollider.enabled = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            SwordAttack();
            swordSwingSound.Play();
        }
    }


    public void SwordAttack()
    {
        Debug.Log("Sword Attack");
        animator.SetTrigger("Attack");
        swordBladeCollider.enabled = true;
        canAttack = false;
        Collider[] colliders = Physics.OverlapBox(swordBladeCollider.transform.position, swordBladeCollider.bounds.size, swordBladeCollider.transform.rotation, enemyLayer);
        
        foreach (Collider collider in colliders)
        {
            Debug.Log("Overlapping with " + collider.gameObject.name);
            onEnemyHitBySword.Raise(this, collider.gameObject);
        }
    
        StartCoroutine(ResetAttackCooldown());
    }

    private IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        swordBladeCollider.enabled = false;
    }

}
