using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    [SerializeField] private GameObject Sword;
    [SerializeField] private GameObject SwordBlade;
    [SerializeField] private bool canAttack =  true;
    [SerializeField] private float attackCooldown = 0.6f;

    private Animator animator;
    private BoxCollider swordBladeCollider; 

    private void Start()
    {
        animator = Sword.GetComponent<Animator>();
        swordBladeCollider = SwordBlade.GetComponent<BoxCollider>();
        Physics.IgnoreCollision(swordBladeCollider, GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>(), true);

        swordBladeCollider.enabled = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            SwordAttack();      
        }
    }


    public void SwordAttack()
    {
        animator.SetTrigger("Attack");
        swordBladeCollider.enabled = true;
        canAttack = false;
        StartCoroutine(ResetAttackCooldown());
    }

    private IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        swordBladeCollider.enabled = false;
    }

}
