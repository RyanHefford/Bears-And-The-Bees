using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private GameObject player;
    private float attackCooldown = 2f;
    public float attackRange = 3f;
    private float lastAttack = 0;
    public int attackDamage = 2;
    //bee animation
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }


    private void OnTriggerStay(Collider other)
    {
        if (lastAttack + attackCooldown < Time.time)
        {
            if (other.CompareTag("Player") && animator.GetBool("IsAlert"))
            {
                animator.SetBool("Attack", true);
                lastAttack = Time.time;
            }
        }
    }

    public void AttackAnimationEvent()
    {
        if (Mathf.Abs(Vector3.Distance(transform.position, player.transform.position)) <= attackRange)
        {
            player.GetComponent<PlayerHealth>().TakeHit(attackDamage);
        }
        animator.SetBool("Attack", false);
    }
}
