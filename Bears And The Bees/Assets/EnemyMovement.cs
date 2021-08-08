using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private EnemyVision vision;
    private NavMeshAgent navAgent;
    private int curPatrolPoint;
    public Vector3[] patrolPoints;

    // Start is called before the first frame update
    void Start()
    {
        vision = GetComponentInChildren<EnemyVision>();
        navAgent = GetComponent<NavMeshAgent>();
        curPatrolPoint = 0;

        GoToNextPoint();
    }

    // Update is called once per frame
    void Update()
    {

        switch (vision.getState())
        {
            case EnemyVision.STATE.PASSIVE:
                navAgent.isStopped = false;
                if (!navAgent.pathPending && navAgent.remainingDistance < 0.5f)
                {
                    GoToNextPoint();
                }
                break;

            case EnemyVision.STATE.ALERT:
                navAgent.SetDestination(vision.getLastSeenPosition());
                navAgent.isStopped = true;
                break;

            case EnemyVision.STATE.CHASING:
                navAgent.SetDestination(vision.getLastSeenPosition());
                navAgent.autoBraking = true;
                navAgent.isStopped = false;
                break;

            case EnemyVision.STATE.SEARCHING:
                navAgent.SetDestination(vision.getLastSeenPosition());
                navAgent.autoBraking = true;
                navAgent.isStopped = false;
                break;

            default:
                navAgent.isStopped = true;
                break;
        }

    }

    private void GoToNextPoint()
    {
        navAgent.autoBraking = false;

        navAgent.SetDestination(patrolPoints[curPatrolPoint]);

        curPatrolPoint = (curPatrolPoint + 1) % patrolPoints.Length;
    }

    private bool checkReachedDestination()
    {
        // Check if we've reached the destination
        if (!navAgent.pathPending)
        {
            if (navAgent.remainingDistance <= navAgent.stoppingDistance)
            {
                if (!navAgent.hasPath || navAgent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
