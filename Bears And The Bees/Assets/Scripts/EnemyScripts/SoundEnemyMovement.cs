using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoundEnemyMovement : MonoBehaviour
{
    public Vector3[] patrolPoints;

    private NavMeshAgent agent;
    private PlayerMovement playerMvmt;
    private GameObject player;
    private bool lookingForPlayer = false;

    private int curPatrolPoint;
    private Vector3 parentPosition;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerMvmt = player.GetComponent<PlayerMovement>();
        parentPosition = transform.parent.position;
        Wander();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(this.transform.position, player.transform.position);

        if (distanceToPlayer < 10.0f)
        {
            LookForPlayer(0.2f);
        }
        else if (distanceToPlayer < 15.0f)
        {
            LookForPlayer(0.5f);
        }
        else
        {
            Wander();
        }
        
    }

    private void LookForPlayer(float noiseThreshold)
    {
        float noise = playerMvmt.GetCurrPlayerNoise();

        if (noise > noiseThreshold)
        {
            lookingForPlayer = true;

            agent.SetDestination(player.transform.position);
        }
       
    }

    private void Wander()
    {
        agent.SetDestination(parentPosition + patrolPoints[curPatrolPoint]);

        curPatrolPoint = (curPatrolPoint + 1) % patrolPoints.Length;

        lookingForPlayer = false;
    }
}
