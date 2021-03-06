using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private EnemyVision vision;
    private NavMeshAgent navAgent;
    private int curPatrolPoint;
    //Gives Agent patrol points in local space
    public Vector3[] patrolPoints;
    private Vector3 parentPosition;

    // Start is called before the first frame update
    void Start()
    {
        vision = GetComponentInChildren<EnemyVision>();
        navAgent = GetComponent<NavMeshAgent>();
        parentPosition = transform.parent.position;
        curPatrolPoint = 0;
        navAgent.speed = PlayerPrefs.GetFloat("EnemySpeed");
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
                Vector3 dir = vision.getLastSeenPosition() - transform.position;
                dir.y = 0; // keep the direction strictly horizontal
                Quaternion rot = Quaternion.LookRotation(dir);
                // slerp to the desired rotation over time
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, 3 * Time.deltaTime);

                navAgent.isStopped = true;
                break;

            case EnemyVision.STATE.CHASING:
                navAgent.SetDestination(vision.getLastSeenPosition());
                navAgent.autoBraking = true;
                navAgent.isStopped = false;
                break;

            case EnemyVision.STATE.SEARCHING:
                if (Vector3.Distance(this.transform.position, navAgent.destination) <= 0.5)
                {
                    Vector3 randomVector3 = new Vector3(Random.Range(-10,10), Random.Range(-10, 10), Random.Range(-10, 10));
                    vision.lastSeenPosition += randomVector3;
                }
                navAgent.SetDestination(vision.getLastSeenPosition());
                navAgent.autoBraking = true;
                navAgent.isStopped = false;
                break;

            case EnemyVision.STATE.STUNNED:
                navAgent.autoBraking = true;
                navAgent.isStopped = true;
                break;

            default:
                navAgent.isStopped = true;
                break;
        }

    }

    private void GoToNextPoint()
    {
        navAgent.autoBraking = false;
        navAgent.SetDestination(parentPosition + patrolPoints[curPatrolPoint]);

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
