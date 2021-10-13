using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyVision : MonoBehaviour
{
    public enum STATE { PASSIVE, ALERT, CHASING, SEARCHING, STUNNED }

    private STATE currState = STATE.PASSIVE;

    //basic enemy stats
    private float viewRadius = 15;
    private float viewAngle = 100;
    public float meshResolution = 0.1f;
    public float alertPauseDuration = 1f;
    public bool isAlert = false;
    private float stunDuration;
    private float stunStart;

    //audio
    private AudioSource audioSource;
    private AudioClip[] audioClips;
    private BackgroundMusicHandle backGroundMusic;

    //mask types
    public LayerMask targetMask;
    public LayerMask obtructionMask;

    //Player vision
    private bool canSeePlayer;
    private bool firstTimeSpotted = true;
    private float timeSeenPlayer = 0;
    public float maxSearchTime = 3f;
    private float searchTime = 0f;
    public float secUntilChase = 1.5f;
    private Vector3 lastSeenPosition;
    private PlayerMovement playerMovement;

    //vision cone
    public MeshFilter viewMeshFilter;
    private Mesh viewMesh;
    private MeshRenderer meshRenderer;
    public int edgeResolveInterations;
    public float edgeDistanceThreshold;
    private float visionHeightAngle = 5;

    //bee animation
    private Animator animator;
    private BeeEyeMovement eyeMovement;


    // Start is called before the first frame update
    void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        animator = gameObject.GetComponentInParent<Animator>();
        eyeMovement = GetComponent<BeeEyeMovement>();

        audioSource = GetComponent<AudioSource>();
        audioClips = Resources.LoadAll<AudioClip>("Sound/BeeSpotted");
        meshRenderer = GetComponentInChildren<MeshRenderer>();

        backGroundMusic = GameObject.FindGameObjectWithTag("Music").GetComponent<BackgroundMusicHandle>();

        secUntilChase = PlayerPrefs.GetFloat("SecTillChase");
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!isAlert && currState != STATE.STUNNED)
        {
            //Logic for changing states
            if (canSeePlayer && !playerMovement.currentStats.isInvisible)
            {
                if ((timeSeenPlayer >= secUntilChase - playerMovement.currentStats.visibility || searchTime > 0))
                {
                    currState = STATE.CHASING;
                    searchTime = maxSearchTime;
                    SlowStatus slowStatus = ScriptableObject.CreateInstance<SlowStatus>();
                    slowStatus.Init(2f, 0.15f);
                    playerMovement.GetComponent<StatusEffectHandler>().AddStatus(slowStatus);
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
            eyeMovement.UpdatePosition(currState != STATE.PASSIVE && currState != STATE.ALERT);
        }
        else if(currState == STATE.STUNNED && Time.time >= stunStart + stunDuration)
        {
            currState = STATE.ALERT;
        }


        //update music
        if (currState == STATE.CHASING)
        {
            backGroundMusic.PlayChaseMusic();
        }
    }

    private void LateUpdate()
    {
        canSeePlayer = false;
        if (currState != STATE.STUNNED) { DrawFieldOfView(); }
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
        List<Vector3> bottomViewPoints = new List<Vector3>();
        List<Vector3> topViewPoints = new List<Vector3>();

        ViewCastInfo[] oldViewCast = new ViewCastInfo[2];

        for(int i = 0; i <= stepCount; i++){

            float currAngle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
            ViewCastInfo[] newViewCast = new ViewCastInfo[2];
            newViewCast[0] = ViewCast(currAngle, true);
            newViewCast[1] = ViewCast(currAngle, false);

            newViewCast = AdjustCastPoints(newViewCast, currAngle);

            ViewCastInfo topEdgeA = new ViewCastInfo(false, Vector3.zero, 0, 0);
            ViewCastInfo topEdgeB = new ViewCastInfo(false, Vector3.zero, 0, 0);
            ViewCastInfo bottomEdgeA = new ViewCastInfo(false, Vector3.zero, 0, 0);
            ViewCastInfo bottomEdgeB = new ViewCastInfo(false, Vector3.zero, 0, 0);


            for (int j = 0; j < 2; j++)
            {
                //if top point
                if (j == 0)
                {

                    if (i > 0)
                    {
                        bool edgeDstThresholdExceeded = Mathf.Abs(oldViewCast[j].dst - newViewCast[j].dst) > edgeDistanceThreshold;
                        if (oldViewCast[j].hit != newViewCast[j].hit || (oldViewCast[j].hit && newViewCast[j].hit && edgeDstThresholdExceeded))
                        {
                            EdgeInfo edge = FindEdge(oldViewCast[j], newViewCast[j], true);

                            if (edge.pointA.point != Vector3.zero)
                            {
                                topEdgeA = edge.pointA;
                            }
                            if (edge.pointB.point != Vector3.zero)
                            {
                                topEdgeB = edge.pointB;
                            }
                        }
                    }
                }
                else
                {

                    if (i > 0)
                    {
                        bool edgeDstThresholdExceeded = Mathf.Abs(oldViewCast[j].dst - newViewCast[j].dst) > edgeDistanceThreshold;
                        if (oldViewCast[j].hit != newViewCast[j].hit || (oldViewCast[j].hit && newViewCast[j].hit && edgeDstThresholdExceeded))
                        {
                            EdgeInfo edge = FindEdge(oldViewCast[j], newViewCast[j], false);
                            if (edge.pointA.point != Vector3.zero)
                            {
                                bottomEdgeA = edge.pointA;
                            }
                            if (edge.pointB.point != Vector3.zero)
                            {
                                bottomEdgeB = edge.pointB;
                            }
                        }
                    }

                    //now update variables
                    //compare Edges

                    Vector3[] updatedEdgesA = UpdateEdges(topEdgeA, bottomEdgeA);
                    Vector3[] updatedEdgesB = UpdateEdges(topEdgeB, bottomEdgeB);


                    if (updatedEdgesA[0] != Vector3.zero && updatedEdgesA[1] != Vector3.zero)
                    {
                        topViewPoints.Add(updatedEdgesA[0]);
                        bottomViewPoints.Add(updatedEdgesA[1]);
                    }

                    if (updatedEdgesB[0] != Vector3.zero && updatedEdgesB[1] != Vector3.zero)
                    {
                        topViewPoints.Add(updatedEdgesB[0]);
                        bottomViewPoints.Add(updatedEdgesB[1]);
                    }

                    Vector3[] updatedRegularPoints = UpdateEdges(newViewCast[j - 1], newViewCast[j]);

                    topViewPoints.Add(updatedRegularPoints[0]);
                    oldViewCast[j - 1] = newViewCast[j - 1];

                    bottomViewPoints.Add(updatedRegularPoints[1]);
                    oldViewCast[j] = newViewCast[j];
                }

            }
        }

        List<Vector3> combinedList = new List<Vector3>();
        combinedList.AddRange(topViewPoints);
        combinedList.AddRange(bottomViewPoints);

        int vertexCount = combinedList.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount) * 9 + 6];

        int numTriangles = 0;

        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(combinedList[i]);
        }
        //create left triangle
        triangles[numTriangles++] = 0;
        triangles[numTriangles++] = topViewPoints.Count + 1;
        triangles[numTriangles++] = 1;

        triangles[numTriangles++] = 0;
        triangles[numTriangles++] = topViewPoints.Count;
        triangles[numTriangles++] = combinedList.Count;


        for (int i = 1; i < vertexCount - 2; i++)
        {
            //top and bottom mesh
            if (i != topViewPoints.Count)
            {
                triangles[numTriangles++] = 0;
                triangles[numTriangles++] = i;
                triangles[numTriangles++] = i + 1;

            }

            //front facing points
            if (i < topViewPoints.Count - 1)
            {
                triangles[numTriangles++] = i;
                triangles[numTriangles++] = topViewPoints.Count + i;
                triangles[numTriangles++] = i + 1;

                triangles[numTriangles++] = i + 1;
                triangles[numTriangles++] = topViewPoints.Count + i;
                triangles[numTriangles++] = topViewPoints.Count + i + 1;
            }
        }

        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    private Vector3[] UpdateEdges(ViewCastInfo topEdge, ViewCastInfo bottomEdge)
    {
        //check a points
        if (topEdge.point != Vector3.zero)
        {
            if (bottomEdge.point != Vector3.zero)
            {
                if (topEdge.dst > bottomEdge.dst)
                {
                    topEdge = UpdateCastPoint(topEdge, true ,bottomEdge.dst);
                }
                else
                {
                    bottomEdge = UpdateCastPoint(bottomEdge, false, topEdge.dst);
                }
            }
            else
            {
                bottomEdge = topEdge;
                bottomEdge = UpdateCastPoint(bottomEdge, false, topEdge.dst);
            }
        }
        else if (bottomEdge.point != Vector3.zero)
        {
            topEdge = bottomEdge;
            topEdge = UpdateCastPoint(topEdge, true, bottomEdge.dst);
        }

        return new Vector3[] { topEdge.point, bottomEdge.point };
    }

    private ViewCastInfo UpdateCastPoint(ViewCastInfo pointToUpdate, bool isTopPoint, float newDistance)
    {
        Vector3 dir = DirFromAngle(pointToUpdate.angle, true, isTopPoint);
        pointToUpdate.point = transform.position + dir * newDistance;
        pointToUpdate.dst = newDistance;

        return pointToUpdate;
    }

    private ViewCastInfo[] AdjustCastPoints(ViewCastInfo[] viewCast, float globalAngle)
    {
        int shortest = viewCast[0].dst < viewCast[1].dst ? 0 : 1;
        int longest = shortest == 0 ? 1 : 0;

        Vector3 dir = DirFromAngle(globalAngle, true, longest == 0);

        viewCast[longest] = new ViewCastInfo(false, transform.position + dir * viewCast[longest].dst, viewCast[longest].dst, globalAngle);

        return viewCast;
    }

    private Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal, bool isTopPoint)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }

        if (isTopPoint)
        {
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Sin(visionHeightAngle * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
        else
        {
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), -Mathf.Sin(visionHeightAngle * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }

    ViewCastInfo ViewCast(float globalAngle, bool isTopPoint)
    {
        Vector3 dir = DirFromAngle(globalAngle, true, isTopPoint);
        RaycastHit hit;

        //check if hit player
        if (Physics.Raycast(transform.position, dir, out hit, viewRadius, targetMask) && !playerMovement.currentStats.isInvisible)
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

    ViewCastInfo ChangeCastDistance(ViewCastInfo oldCast, bool isTopPoint, float newDistance)
    {
        RaycastHit hit;
        Vector3 dir = DirFromAngle(oldCast.angle, true, isTopPoint);

        if (Physics.Raycast(transform.position, dir, out hit, viewRadius))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, oldCast.angle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * newDistance, viewRadius, oldCast.angle);
        }
    }

    EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast, bool isTopPoint)
    {
        float minAngle = minViewCast.angle;
        float maxAngle = maxViewCast.angle;
        ViewCastInfo minPoint = new ViewCastInfo(false, Vector3.zero, 0, 0);
        ViewCastInfo maxPoint = new ViewCastInfo(false, Vector3.zero, 0, 0);

        for (int i = 0; i < edgeResolveInterations; i++)
        {
            float currAngle = (minAngle + maxAngle) / 2;
            ViewCastInfo newViewCast = ViewCast(currAngle, isTopPoint);

            bool edgeDstThresholdExceeded = Mathf.Abs(minViewCast.dst - newViewCast.dst) > edgeDistanceThreshold;
            if (newViewCast.hit == minViewCast.hit && !edgeDstThresholdExceeded)
            {
                minAngle = currAngle;
                minPoint = newViewCast;
            }
            else
            {
                maxAngle = currAngle;
                maxPoint = newViewCast;
            }
        }

        return new EdgeInfo(minPoint, maxPoint);

    }

    public void PlayerFound(Vector3 spottedPosition)
    {
        searchTime = maxSearchTime;
        currState = STATE.SEARCHING;
        lastSeenPosition = spottedPosition;
    }

    public void StunEnemy(float duration)
    {
        currState = STATE.STUNNED;
        stunDuration = duration;
        stunStart = Time.time;

        viewMesh.Clear();
        viewMesh.RecalculateNormals();

    }
    private void updateColor()
    {
        switch (currState)
        {
            case STATE.PASSIVE:
                meshRenderer.material.color = new Color(0, 1, 0, 0.25f);
                animator.SetBool("IsAlert", false);
                break;

            case STATE.ALERT:
                meshRenderer.material.color = new Color(1, 0.5f, 0, 0.25f);
                break;

            case STATE.CHASING:
                meshRenderer.material.color = new Color(1, 0, 0, 0.25f);
                animator.SetBool("IsAlert", true);
                break;

            case STATE.SEARCHING:
                meshRenderer.material.color = new Color(1f, 1f, 0, 0.25f);
                break;

            default:
                meshRenderer.material.color = new Color(0, 1, 0, 0.25f);
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
        public ViewCastInfo pointA;
        public ViewCastInfo pointB;

        public EdgeInfo(ViewCastInfo _pointA, ViewCastInfo _pointB)
        {
            pointA = _pointA;
            pointB = _pointB;
        }
    }
}
