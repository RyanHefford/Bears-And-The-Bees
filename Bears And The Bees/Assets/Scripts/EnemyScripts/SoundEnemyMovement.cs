using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoundEnemyMovement : MonoBehaviour
{
    public Vector3[] patrolPoints;

    private NavMeshAgent agent;
    private PlayerMovement playerMvmt;
    private EnemyVision vision;
    private bool lookingForPlayer = false;
    private float searchTimer = 4f;
    private float lastHeardPlayer;
    private int curPatrolPoint;
    private Vector3 parentPosition;

    void Start()
    {
        vision = GetComponentInChildren<EnemyVision>();
        agent = GetComponent<NavMeshAgent>();
        playerMvmt = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        parentPosition = transform.parent.position;
        Wander();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(this.transform.position, playerMvmt.transform.position);

        if (distanceToPlayer < 10.0f)
        {
            LookForPlayer(0.2f);
        }
        else if (distanceToPlayer < 15.0f)
        {
            LookForPlayer(0.5f);
        }


        switch (vision.getState())
        {
            case EnemyVision.STATE.PASSIVE:
                agent.isStopped = false;
                if (!agent.pathPending && agent.remainingDistance < 0.5f)
                {
                    Wander();
                }
                break;

            case EnemyVision.STATE.ALERT:
                Vector3 dir = vision.getLastSeenPosition() - transform.position;
                dir.y = 0; // keep the direction strictly horizontal
                Quaternion rot = Quaternion.LookRotation(dir);
                // slerp to the desired rotation over time
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, 3 * Time.deltaTime);

                agent.isStopped = true;
                break;

            case EnemyVision.STATE.CHASING:
                agent.SetDestination(vision.getLastSeenPosition());
                agent.autoBraking = true;
                agent.isStopped = false;
                break;

            case EnemyVision.STATE.SEARCHING:
                if (Vector3.Distance(this.transform.position, agent.destination) <= 0.5)
                {
                    Vector3 randomVector3 = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
                    vision.lastSeenPosition += randomVector3;
                }
                agent.SetDestination(vision.getLastSeenPosition());
                agent.autoBraking = true;
                agent.isStopped = false;
                break;

            case EnemyVision.STATE.STUNNED:
                agent.autoBraking = true;
                agent.isStopped = true;
                break;

            default:
                agent.isStopped = true;
                break;
        }
    }

    private void LookForPlayer(float noiseThreshold)
    {
        float noise = playerMvmt.GetCurrPlayerNoise();

        if (noise > noiseThreshold)
        {
            lookingForPlayer = true;
            vision.PlayerFound(playerMvmt.transform.position);
            agent.SetDestination(playerMvmt.transform.position);

            lastHeardPlayer = Time.time;
        }

        //reset to wander after search timer
        if (lookingForPlayer && Time.time >= lastHeardPlayer + searchTimer)
        {
            lookingForPlayer = false;
        }
       
    }

    private void Wander()
    {
        agent.autoBraking = false;
        agent.SetDestination(parentPosition + patrolPoints[curPatrolPoint]);

        curPatrolPoint = (curPatrolPoint + 1) % patrolPoints.Length;
    }
}
