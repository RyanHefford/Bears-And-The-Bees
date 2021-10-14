using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JazzEnemyAttack : MonoBehaviour
{
    private GameObject player;
    private float attackCooldown = 2f;
    private float attackRange = 2.5f;
    private float lastAttack = 0;
    private int attackDamage = 1;
    public AudioClip attackSound;
    private AudioSource audioSource;
    //bee animation
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        audioSource = GetComponentInParent<AudioSource>();
        attackDamage = PlayerPrefs.GetInt("EnemyAttack") / 2;
        attackCooldown = PlayerPrefs.GetFloat("AttackCD");
        attackRange = PlayerPrefs.GetFloat("AttackRange") + 8f;
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
            audioSource.PlayOneShot(attackSound);
            player.GetComponent<PlayerHealth>().TakeHit(attackDamage);
            SlowStatus slowStatus = ScriptableObject.CreateInstance<SlowStatus>();
            slowStatus.Init(2f, 0.15f);
            player.GetComponent<StatusEffectHandler>().AddStatus(slowStatus);
            JumpDownStatus jumpDownStatus = ScriptableObject.CreateInstance<JumpDownStatus>();
            jumpDownStatus.Init(2f, 0.15f);
            player.GetComponent<StatusEffectHandler>().AddStatus(jumpDownStatus);
        }
        animator.SetBool("Attack", false);
    }
}
