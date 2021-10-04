using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyDifficulty : MonoBehaviour
{
    public GameObject[] enemySpawnPoints;
    public int limit1, limit2, limit3;
    private int getEnemyDifficulty;
    private int limit;

    // Start is called before the first frame update
    void Start()
    {
        getEnemyDifficulty = PlayerPrefs.GetInt("EnemyDifficulty");
        limit = 0;
        InstantiateRandomPoints();
    }

    private void InstantiateRandomPoints()
    {
        if (getEnemyDifficulty < 2)
        {
            limit = limit1;
        }
        else if (getEnemyDifficulty < 4)
        {
            limit = limit2;
        }
        else if (getEnemyDifficulty < 6)
        {
            limit = limit3;
        }

        for (int i = limit3 - 1; i > limit - 1; i--)
        {
            enemySpawnPoints[i].SetActive(false);
        }

        int newEnemyDiff = getEnemyDifficulty + 1;
        PlayerPrefs.SetInt("EnemyDifficulty", newEnemyDiff);
    }
}
