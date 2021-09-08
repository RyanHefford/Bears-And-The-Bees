using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerate : MonoBehaviour
{
    public int maxAreas;
    public GameObject doorBarrier;
    public GameObject player;
    public GameObject[] areas;
    public float[] areaLength;
    public float[] areaEntrances;
    public float[] areaExits;

    private float numAreas;

    // Start is called before the first frame update
    void Start()
    {
        numAreas = areas.Length;
        InstantiateAreas();
    }

    private void InstantiateAreas()
    {
        float currZPos = 0;
        float currXPos = 0;
        float prevDist = 0;
        int prevArea = 0;

        for (int i = 0; i < maxAreas; i++)
        {
            int randomArea = Random.Range(0, 6);

            if (i != 0)
            {
                float currDist = areaExits[prevArea] - areaEntrances[randomArea] + prevDist;
                currXPos = currDist;
                prevDist = currDist;
            } 
            else
            {
                Vector3 doorPosition = new Vector3(areaEntrances[randomArea], 0f, 6f);
                Instantiate(doorBarrier, doorPosition, Quaternion.identity);
                InstantiatePlayer(randomArea);
            }

            Vector3 position = new Vector3(currXPos, 0f, -currZPos);

            Instantiate(areas[randomArea], position, Quaternion.identity);

            currZPos += areaLength[randomArea];

            prevArea = randomArea;
        }
    }

    private void InstantiatePlayer(int areaNum)
    {
        Vector3 position = new Vector3(areaEntrances[areaNum] - 3.5f, 1f, 3f);

        Instantiate(player, position, Quaternion.identity);
    }
}
