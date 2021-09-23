using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyDifficultyA2 : MonoBehaviour
{
    private int getEnemyDifficulty;
    public GameObject basicBeeEnemy;
    public GameObject sunEnemy;
    public GameObject parentArea;

    // Start is called before the first frame update
    void Start()
    {
        getEnemyDifficulty = PlayerPrefs.GetInt("EnemyDifficulty");
        Debug.Log(getEnemyDifficulty);
        SpawnEnemy();
    }

    // Spawn enemies at specific points depending on difficulty
    private void SpawnEnemy()
    {
        if (getEnemyDifficulty < 2)
        {
            Vector3 position1 = new Vector3(-14f, 0.5f, -25f);
            GameObject bee1 = Instantiate(basicBeeEnemy, position1 + transform.position, Quaternion.identity);
            bee1.transform.parent = transform;

            Vector3 position2 = new Vector3(-38f, 0.5f, -7.5f);
            GameObject bee2 = Instantiate(basicBeeEnemy, position2 + transform.position, Quaternion.identity);
            bee2.transform.parent = transform;
        }
        else if (getEnemyDifficulty < 4)
        {
            Vector3 position1 = new Vector3(-14f, 0.5f, -25f);
            GameObject bee1 = Instantiate(basicBeeEnemy, position1 + transform.position, Quaternion.identity);
            bee1.transform.parent = transform;

            Vector3 position2 = new Vector3(-38f, 0.5f, -7.5f);
            GameObject bee2 = Instantiate(basicBeeEnemy, position2 + transform.position, Quaternion.identity);
            bee2.transform.parent = transform;

            Vector3 position3 = new Vector3(-38f, 0.5f, -40f);
            GameObject bee3 = Instantiate(basicBeeEnemy, position3 + transform.position, Quaternion.identity);
            bee3.transform.parent = transform;

            Vector3 positionSun1 = new Vector3(-20f, 8f, -47.5f);
            GameObject sun1 = Instantiate(sunEnemy, positionSun1 + transform.position, Quaternion.identity);
            sun1.transform.parent = transform;
        }
        else if (getEnemyDifficulty < 6)
        {
            Vector3 position1 = new Vector3(-14f, 0.5f, -25f);
            GameObject bee1 = Instantiate(basicBeeEnemy, position1 + transform.position, Quaternion.identity);
            bee1.transform.parent = transform;

            Vector3 position2 = new Vector3(-38f, 0.5f, -7.5f);
            GameObject bee2 = Instantiate(basicBeeEnemy, position2 + transform.position, Quaternion.identity);
            bee2.transform.parent = transform;

            Vector3 position3 = new Vector3(-38f, 0.5f, -40f);
            GameObject bee3 = Instantiate(basicBeeEnemy, position3 + transform.position, Quaternion.identity);
            bee3.transform.parent = transform;

            Vector3 position4 = new Vector3(-20f, 0.5f, -38f);
            GameObject bee4 = Instantiate(basicBeeEnemy, position4 + transform.position, Quaternion.identity);
            bee4.transform.parent = transform;

            Vector3 positionSun1 = new Vector3(-20f, 8f, -47.5f);
            GameObject sun1 = Instantiate(sunEnemy, positionSun1 + transform.position, Quaternion.identity);
            sun1.transform.parent = transform;

            Vector3 positionSun2 = new Vector3(-23f, 8f, -0.5f);
            GameObject sun2 = Instantiate(sunEnemy, positionSun2 + transform.position, Quaternion.identity);
            sun2.transform.parent = transform;
            sun2.transform.Rotate(0f, 180f, 0f);
        }

        int newEnemyDiff = getEnemyDifficulty + 1;
        PlayerPrefs.SetInt("EnemyDifficulty", newEnemyDiff);
    }
}
