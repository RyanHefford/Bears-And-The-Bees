using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyDifficultyA1 : MonoBehaviour
{
    private int getEnemyDifficulty;
    public GameObject basicBeeEnemy;
    public GameObject saxBeeEnemy;
    public GameObject sunEnemy;
    public GameObject parentArea;

    // Start is called before the first frame update
    void Start()
    {
        getEnemyDifficulty = PlayerPrefs.GetInt("EnemyDifficulty");
        SpawnEnemy();
    }

    // Spawn enemies at specific points depending on difficulty
    private void SpawnEnemy()
    {
        if (getEnemyDifficulty < 2)
        {
            Vector3 position1 = new Vector3(-3f, 0.5f, -5f);
            GameObject bee1 = Instantiate(basicBeeEnemy, position1 + transform.position, Quaternion.identity);
            bee1.transform.parent = transform;

            Vector3 position2 = new Vector3(-12.75f, 0.5f, -32f);
            GameObject bee2 = Instantiate(basicBeeEnemy, position2 + transform.position, Quaternion.identity);
            bee2.transform.parent = transform;
        }
        else if (getEnemyDifficulty < 4)
        {
            Vector3 position1 = new Vector3(-3f, 0.5f, -5f);
            GameObject bee1 = Instantiate(basicBeeEnemy, position1 + transform.position, Quaternion.identity);
            bee1.transform.parent = transform;

            Vector3 position2 = new Vector3(-12.75f, 0.5f, -32f);
            GameObject bee2 = Instantiate(basicBeeEnemy, position2 + transform.position, Quaternion.identity);
            bee2.transform.parent = transform;

            Vector3 position3 = new Vector3(-19.5f, 0.5f, -18f);
            GameObject bee3 = Instantiate(saxBeeEnemy, position3 + transform.position, Quaternion.identity);
            bee3.transform.parent = transform;
        }
        else if (getEnemyDifficulty < 6)
        {
            Vector3 position1 = new Vector3(-3f, 0.5f, -5f);
            GameObject bee1 = Instantiate(basicBeeEnemy, position1 + transform.position, Quaternion.identity);
            bee1.transform.parent = transform;

            Vector3 position2 = new Vector3(-12.75f, 0.5f, -32f);
            GameObject bee2 = Instantiate(basicBeeEnemy, position2 + transform.position, Quaternion.identity);
            bee2.transform.parent = transform;

            Vector3 position3 = new Vector3(-19.5f, 0.5f, -18f);
            GameObject bee3 = Instantiate(basicBeeEnemy, position3 + transform.position, Quaternion.identity);
            bee3.transform.parent = transform;

            Vector3 position4 = new Vector3(-3.2f, 0.5f, -42f);
            GameObject bee4 = Instantiate(saxBeeEnemy, position4 + transform.position, Quaternion.identity);
            bee4.transform.parent = transform;
        }

        int newEnemyDiff = getEnemyDifficulty + 1;
        PlayerPrefs.SetInt("EnemyDifficulty", newEnemyDiff);
    }
}
