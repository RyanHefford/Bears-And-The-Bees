using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    private float radius = 15;
    private float angle = 100;
    public float meshResolution = 0.1f;

    private GameObject player;

    public LayerMask targetMask;
    public LayerMask obtructionMask;

    private bool canSeePlayer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        DrawFieldOfView();
        if (canSeePlayer)
        {
            //todo
        }
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOFViewCheck();
        }
    }

    private void FieldOFViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if(Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obtructionMask))
                {
                    canSeePlayer = true;
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                canSeePlayer = false;
            }
        }else if (canSeePlayer)
        {
            canSeePlayer = false;
        }
    }

    private void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(angle * meshResolution);
        float stepAngleSize = angle / stepCount;

        for(int i = 0; i <= stepCount; i++){
            float currAngle = transform.eulerAngles.y - angle / 2 + stepAngleSize * i;
            Debug.DrawLine(transform.position, transform.position + new Vector3(Mathf.Sin(currAngle * Mathf.Deg2Rad), 0, Mathf.Cos(currAngle * Mathf.Deg2Rad)) * radius, Color.red);
        }
    }
}
