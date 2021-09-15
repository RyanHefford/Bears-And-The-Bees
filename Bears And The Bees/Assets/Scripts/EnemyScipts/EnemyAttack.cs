using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackCooldown = 1f;
    private float lastAttack = 0;
    public int attackDamage = 2;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (lastAttack + attackCooldown < Time.time)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<PlayerHealth>().TakeHit(attackDamage);
                lastAttack = Time.time;
            }
        }
    }
}
