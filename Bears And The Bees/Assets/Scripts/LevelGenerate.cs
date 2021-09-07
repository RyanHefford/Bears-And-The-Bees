using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerate : MonoBehaviour
{
    public int maxAreas;
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
            int randomArea = Random.Range(0, 4);

            if (i != 0)
            {
                float currDist = areaExits[prevArea] - areaEntrances[randomArea] + prevDist;
                currXPos = currDist;
                prevDist = currDist;
            }

            Vector3 position = new Vector3(currXPos, 0f, -currZPos);

            Instantiate(areas[randomArea], position, Quaternion.identity);

            currZPos += areaLength[randomArea];

            prevArea = randomArea;
        }
    }
}
