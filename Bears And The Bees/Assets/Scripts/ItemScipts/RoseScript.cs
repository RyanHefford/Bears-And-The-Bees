using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoseScript : MonoBehaviour
{
    public float startDelay = 2.0f;
    public float destroyDelay = 6.0f;
    private float startTime;
    private GameObject[] enemies;
    private float maxAlertDistance = 40f;
    private GameObject player;

    private bool startLure = false;
    private ParticleSystem[] particleSystems;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");

        particleSystems = GetComponentsInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!startLure && Time.time > startTime + startDelay)
        {
            startLure = true;
            foreach (ParticleSystem currParticle in particleSystems)
            {
                currParticle.Play();
            }
        }else if (Time.time > startTime + destroyDelay)
        {
            Destroy(this.gameObject);
        }
        else if(startLure)
        {
            LureEnemies();
        }
    }

    private void LureEnemies()
    {
        foreach (GameObject enemy in enemies)
        {
            // check if enemy is within alert distance
            if (Vector3.Distance(transform.position, enemy.transform.position) <= maxAlertDistance)
            {
                if (enemy.GetComponentInChildren<EnemyVision>() != null)
                {
                    enemy.GetComponentInChildren<EnemyVision>().PlayerFound(this.transform.position);
                }
            }
        }
    }
}
