using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyVision : MonoBehaviour
{
    public enum STATE { PASSIVE, ALERT, CHASING, SEARCHING }

    private STATE currState = STATE.PASSIVE;

    //basic enemy stats
    private float viewRadius = 15;
    private float viewAngle = 100;
    public float meshResolution = 0.1f;
    public float alertPauseDuration = 1f;
    public bool isAlert = false;

    //audio
    private AudioSource audioSource;
    private AudioClip[] audioClips;

    //mask types
    public LayerMask targetMask;
    public LayerMask obtructionMask;

    //Player vision
    private bool canSeePlayer;
    private bool firstTimeSpotted = true;
    private float timeSeenPlayer = 0;
    public float maxSearchTime = 3f;
    private float searchTime = 0f;
    public float secUntilChase = 2f;
    private Vector3 lastSeenPosition;
    private PlayerMovement playerMovement;

    //vision cone
    public MeshFilter viewMeshFilter;
    private Mesh viewMesh;
    private MeshRenderer meshRenderer;
    public int edgeResolveInterations;
    public float edgeDistanceThreshold;

    // Start is called before the first frame update
    void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        audioSource = GetComponent<AudioSource>();
        audioClips = Resources.LoadAll<AudioClip>("Sound/BeeSpotted");
        meshRenderer = GetComponentInChildren<MeshRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        
        if (!isAlert)
        {
            //Logic for changing states
            if (canSeePlayer)
            {
                if (timeSeenPlayer >= secUntilChase - playerMovement.visibility || searchTime > 0)
                {
                    currState = STATE.CHASING;
                    searchTime = maxSearchTime;
                }
                else
                {
                    currState = STATE.ALERT;
                    if (firstTimeSpotted)
                    {
                        //play alert sound effect
                        audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
                        firstTimeSpotted = false;
                        StartCoroutine(AlertDelay());
                    }
                }
                timeSeenPlayer += Time.deltaTime;
            }
            else
            {
                if (searchTime > 0)
                {
                    currState = STATE.SEARCHING;
                    searchTime -= Time.deltaTime;
                }
                else
                {
                    timeSeenPlayer = 0;
                    currState = STATE.PASSIVE;
                    firstTimeSpotted = true;
                }

            }

            updateColor();
        }
    }

    private void LateUpdate()
    {
        canSeePlayer = false;
        DrawFieldOfView();
    }

    public STATE getState()
    {
        return currState;
    }

    private IEnumerator AlertDelay()
    {
        isAlert = true;
        yield return new WaitForSeconds(alertPauseDuration);
        isAlert = false;
    }

    public Vector3 getLastSeenPosition()
    {
        return lastSeenPosition;
    }

    private void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();

        ViewCastInfo oldViewCast = new ViewCastInfo();

        for(int i = 0; i <= stepCount; i++){
            float currAngle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(currAngle);

            if (i > 0)
            {
                bool edgeDstThresholdExceeded = Mathf.Abs(oldViewCast.dst - newViewCast.dst) > edgeDistanceThreshold;
                if (oldViewCast.hit != newViewCast.hit || (oldViewCast.hit && newViewCast.hit && edgeDstThresholdExceeded))
                {
                    EdgeInfo edge = FindEdge(oldViewCast, newViewCast);
                    if(edge.pointA != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointA);
                    }
                    if (edge.pointB != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointB);
                    }
                }
            }

            viewPoints.Add(newViewCast.point);
            oldViewCast = newViewCast;
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;
        for(int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);
            if(i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;

            }
        }

        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    private Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;

        //check if hit player
        if (Physics.Raycast(transform.position, dir, out hit, viewRadius, targetMask))
        {
            Vector3 currPlayerPoint = hit.point;
            //check if wall is in the way
            if (Physics.Raycast(transform.position, dir, out hit, viewRadius, obtructionMask))
            {
                //check if wall is closer
                if (Vector3.Distance(transform.position, currPlayerPoint) < Vector3.Distance(transform.position, hit.point))
                {
                    canSeePlayer = true;
                    lastSeenPosition = currPlayerPoint;
                }
            }
            else
            {
                canSeePlayer = true;
                lastSeenPosition = currPlayerPoint;
            }
        }

        if (Physics.Raycast(transform.position, dir, out hit, viewRadius, obtructionMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
        }
    }

    EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
    {
        float minAngle = minViewCast.angle;
        float maxAngle = maxViewCast.angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for (int i = 0; i < edgeResolveInterations; i++)
        {
            float currAngle = (minAngle + maxAngle) / 2;
            ViewCastInfo newViewCast = ViewCast(currAngle);

            bool edgeDstThresholdExceeded = Mathf.Abs(minViewCast.dst - newViewCast.dst) > edgeDistanceThreshold;
            if (newViewCast.hit == minViewCast.hit && !edgeDstThresholdExceeded)
            {
                minAngle = currAngle;
                minPoint = newViewCast.point;
            }
            else
            {
                maxAngle = currAngle;
                maxPoint = newViewCast.point;
            }
        }

        return new EdgeInfo(minPoint, maxPoint);

    }

    private void updateColor()
    {
        switch (currState)
        {
            case STATE.PASSIVE:
                meshRenderer.material.color = new Color(0, 1, 0, 0.5f);
                break;

            case STATE.ALERT:
                meshRenderer.material.color = new Color(1, 0.5f, 0, 0.5f);
                print("Here");
                break;

            case STATE.CHASING:
                meshRenderer.material.color = new Color(1, 0, 0, 0.5f);
                break;

            case STATE.SEARCHING:
                meshRenderer.material.color = new Color(1f, 1f, 0, 0.5f);
                break;

            default:
                meshRenderer.material.color = new Color(0, 1, 0, 0.5f);
                break;
        }

    }

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }

    public struct EdgeInfo 
    {
        public Vector3 pointA;
        public Vector3 pointB;

        public EdgeInfo(Vector3 _pointA, Vector3 _pointB)
        {
            pointA = _pointA;
            pointB = _pointB;
        }
    }
}
