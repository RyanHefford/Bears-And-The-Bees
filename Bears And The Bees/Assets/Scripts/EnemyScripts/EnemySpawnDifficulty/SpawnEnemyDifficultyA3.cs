using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyDifficultyA3 : MonoBehaviour
{
    private int getEnemyDifficulty;
    public GameObject basicBeeEnemy;
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
        if (getEnemyDifficulty == 1)
        {
            Vector3 position1 = new Vector3(-13f, 0.5f, -25f);
            GameObject bee1 = Instantiate(basicBeeEnemy, position1, Quaternion.identity);
            bee1.transform.parent = transform;

            Vector3 position2 = new Vector3(-46f, 0.5f, -20f);
            GameObject bee2 = Instantiate(basicBeeEnemy, position2, Quaternion.identity);
            bee2.transform.parent = transform;
        }
        else if (getEnemyDifficulty == 2)
        {
            Vector3 position1 = new Vector3(-13f, 0.5f, -25f);
            GameObject bee1 = Instantiate(basicBeeEnemy, position1, Quaternion.identity);
            bee1.transform.parent = transform;

            Vector3 position2 = new Vector3(-46f, 0.5f, -20f);
            GameObject bee2 = Instantiate(basicBeeEnemy, position2, Quaternion.identity);
            bee2.transform.parent = transform;

            Vector3 position3 = new Vector3(-30f, 0.5f, -26f);
            GameObject bee3 = Instantiate(basicBeeEnemy, position3, Quaternion.identity);
            bee3.transform.parent = transform;

            Vector3 positionSun1 = new Vector3(-28f, 8f, -0.5f);
            GameObject sun1 = Instantiate(sunEnemy, positionSun1, Quaternion.identity);
            sun1.transform.parent = transform;
            sun1.transform.Rotate(0f, 180f, 0f);
        }
        else if (getEnemyDifficulty == 3)
        {
            Vector3 position1 = new Vector3(-13f, 0.5f, -25f);
            GameObject bee1 = Instantiate(basicBeeEnemy, position1, Quaternion.identity);
            bee1.transform.parent = transform;

            Vector3 position2 = new Vector3(-46f, 0.5f, -20f);
            GameObject bee2 = Instantiate(basicBeeEnemy, position2, Quaternion.identity);
            bee2.transform.parent = transform;

            Vector3 position3 = new Vector3(-30f, 0.5f, -26f);
            GameObject bee3 = Instantiate(basicBeeEnemy, position3, Quaternion.identity);
            bee3.transform.parent = transform;

            Vector3 position4 = new Vector3(-30f, 0.5f, -26f);
            GameObject bee4 = Instantiate(basicBeeEnemy, position4, Quaternion.identity);
            bee4.transform.parent = transform;

            Vector3 positionSun1 = new Vector3(-28f, 8f, -0.5f);
            GameObject sun1 = Instantiate(sunEnemy, positionSun1, Quaternion.identity);
            sun1.transform.parent = transform;
            sun1.transform.Rotate(0f, 180f, 0f);

            Vector3 positionSun2 = new Vector3(-0.5f, 8f, -22f);
            GameObject sun2 = Instantiate(sunEnemy, positionSun2, Quaternion.identity);
            sun2.transform.parent = transform;
            sun2.transform.Rotate(0f, -90f, 0f);

            Vector3 positionSun3 = new Vector3(-56.5f, 8f, -30f);
            GameObject sun3 = Instantiate(sunEnemy, positionSun3, Quaternion.identity);
            sun3.transform.parent = transform;
            sun3.transform.Rotate(0f, 90f, 0f);
        }
    }
}
