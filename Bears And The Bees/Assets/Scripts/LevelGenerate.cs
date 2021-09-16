using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerate : MonoBehaviour
{
    public int maxAreas;
    public GameObject doorBarrier;
    public GameObject endBarrier;
    public GameObject player;
    public GameObject honeyBottle;
    public GameObject newCamera;
    public GameObject[] areas;

    private float numAreas;
    private float transformPosX;
    private float transformPosY;
    private float transformPosZ;

    private void Awake()
    {
        numAreas = areas.Length;
        transformPosX = transform.position.x;
        transformPosY = transform.position.y;
        transformPosZ = transform.position.z;
        InstantiateAreas();
    }

    private void InstantiateAreas()
    {
        float currXPos = transformPosX;
        float currYPos = transformPosY;
        float currZPos = transformPosZ;
        float prevDist = 0f;
        int prevArea = 0;

        for (int i = 0; i < maxAreas; i++)
        {
            int randomArea = Random.Range(0, 6);
            AreaValues currAreaValues = areas[randomArea].GetComponent<AreaValues>();
            AreaValues prevAreaValues = areas[prevArea].GetComponent<AreaValues>();

            if (i != 0)
            {
                //float currDist = areaExits[prevArea] - areaEntrances[randomArea] + prevDist;
                float currDist = prevAreaValues.GetExitDist() - currAreaValues.GetEntranceDist() + prevDist;
                currXPos = currDist + transformPosX;
                prevDist = currDist;
            } 
            else if (i == 0)
            {
                Vector3 doorPosition = new Vector3(currAreaValues.GetEntranceDist() + currXPos, currYPos, 6f - currZPos);
                Instantiate(doorBarrier, doorPosition, Quaternion.identity);
                InstantiatePlayer(currAreaValues);
            }

            Vector3 position = new Vector3(currXPos, currYPos, -currZPos);

            Instantiate(areas[randomArea], position, Quaternion.identity);

            currZPos += currAreaValues.GetAreaLength();

            prevArea = randomArea;

            // Adding ending point and honey
            if (i == maxAreas - 1)
            {
                float endDist = currAreaValues.GetExitDist() + prevDist - 3.5f + transformPosX;
                float newEndZPos = currZPos - 3; 
                Vector3 endPosition = new Vector3(endDist, currYPos, -newEndZPos);
                Vector3 honeyPosition = new Vector3(endDist, currYPos + 2f, -newEndZPos);

                Instantiate(endBarrier, endPosition, Quaternion.identity);
                Instantiate(honeyBottle, honeyPosition, Quaternion.identity);
            }
        }
    }

    private void InstantiatePlayer(AreaValues currAreaValues)
    {
        Vector3 position = new Vector3(currAreaValues.GetEntranceDist() - 3.5f + transformPosX, 1f + transformPosY, 3f - transformPosZ);

        Instantiate(player, position, Quaternion.identity);
        GameObject followCamera = Instantiate(newCamera, position, Quaternion.identity);
        followCamera.transform.Rotate(45f, 135f, 0f);
    }
}
