using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyDifficultyA5Left : MonoBehaviour
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
            Vector3 position1 = new Vector3(-50f, 0.5f, -38f);
            GameObject bee1 = Instantiate(basicBeeEnemy, position1 + transform.position, Quaternion.identity);
            bee1.transform.parent = transform;

            Vector3 position2 = new Vector3(-5f, 0.5f, -38f);
            GameObject bee2 = Instantiate(basicBeeEnemy, position2 + transform.position, Quaternion.identity);
            bee2.transform.parent = transform;

            Vector3 position3 = new Vector3(-8.5f, 0.5f, -73f);
            GameObject bee3 = Instantiate(basicBeeEnemy, position3 + transform.position, Quaternion.identity);
            bee3.transform.parent = transform;

            Vector3 position4 = new Vector3(-46.5f, 0.5f, -73f);
            GameObject bee4 = Instantiate(basicBeeEnemy, position4 + transform.position, Quaternion.identity);
            bee4.transform.parent = transform;

            Vector3 position5 = new Vector3(-28f, 0.5f, -95f);
            GameObject bee5 = Instantiate(basicBeeEnemy, position5 + transform.position, Quaternion.identity);
            bee5.transform.parent = transform;
        }
        else if (getEnemyDifficulty < 4)
        {
            Vector3 position1 = new Vector3(-50f, 0.5f, -38f);
            GameObject bee1 = Instantiate(basicBeeEnemy, position1 + transform.position, Quaternion.identity);
            bee1.transform.parent = transform;

            Vector3 position2 = new Vector3(-5f, 0.5f, -38f);
            GameObject bee2 = Instantiate(basicBeeEnemy, position2 + transform.position, Quaternion.identity);
            bee2.transform.parent = transform;

            Vector3 position3 = new Vector3(-8.5f, 0.5f, -73f);
            GameObject bee3 = Instantiate(basicBeeEnemy, position3 + transform.position, Quaternion.identity);
            bee3.transform.parent = transform;

            Vector3 position4 = new Vector3(-46.5f, 0.5f, -73f);
            GameObject bee4 = Instantiate(basicBeeEnemy, position4 + transform.position, Quaternion.identity);
            bee4.transform.parent = transform;

            Vector3 position5 = new Vector3(-28f, 0.5f, -95f);
            GameObject bee5 = Instantiate(basicBeeEnemy, position5 + transform.position, Quaternion.identity);
            bee5.transform.parent = transform;

            Vector3 position6 = new Vector3(-18f, 0.5f, -43f);
            GameObject bee6 = Instantiate(saxBeeEnemy, position6 + transform.position, Quaternion.identity);
            bee6.transform.parent = transform;

            Vector3 position7 = new Vector3(-36f, 0.5f, -43f);
            GameObject bee7 = Instantiate(saxBeeEnemy, position7 + transform.position, Quaternion.identity);
            bee7.transform.parent = transform;

            Vector3 positionSun1 = new Vector3(-33.5f, 8f, -71f);
            GameObject sun1 = Instantiate(sunEnemy, positionSun1 + transform.position, Quaternion.identity);
            sun1.transform.parent = transform;
            sun1.transform.Rotate(0f, -90f, 0f);

            Vector3 positionSun2 = new Vector3(-22.5f, 8f, -71f);
            GameObject sun2 = Instantiate(sunEnemy, positionSun2, Quaternion.identity);
            sun2.transform.parent = transform;
            sun2.transform.Rotate(0f, 90f, 0f);
        }
        else if (getEnemyDifficulty < 6)
        {
            Vector3 position1 = new Vector3(-50f, 0.5f, -38f);
            GameObject bee1 = Instantiate(basicBeeEnemy, position1 + transform.position, Quaternion.identity);
            bee1.transform.parent = transform;

            Vector3 position2 = new Vector3(-5f, 0.5f, -38f);
            GameObject bee2 = Instantiate(basicBeeEnemy, position2 + transform.position, Quaternion.identity);
            bee2.transform.parent = transform;

            Vector3 position3 = new Vector3(-8.5f, 0.5f, -73f);
            GameObject bee3 = Instantiate(basicBeeEnemy, position3 + transform.position, Quaternion.identity);
            bee3.transform.parent = transform;

            Vector3 position4 = new Vector3(-46.5f, 0.5f, -73f);
            GameObject bee4 = Instantiate(basicBeeEnemy, position4 + transform.position, Quaternion.identity);
            bee4.transform.parent = transform;

            Vector3 position5 = new Vector3(-28f, 0.5f, -95f);
            GameObject bee5 = Instantiate(basicBeeEnemy, position5 + transform.position, Quaternion.identity);
            bee5.transform.parent = transform;

            Vector3 position6 = new Vector3(-18f, 0.5f, -43f);
            GameObject bee6 = Instantiate(basicBeeEnemy, position6 + transform.position, Quaternion.identity);
            bee6.transform.parent = transform;

            Vector3 position7 = new Vector3(-36f, 0.5f, -43f);
            GameObject bee7 = Instantiate(saxBeeEnemy, position7 + transform.position, Quaternion.identity);
            bee7.transform.parent = transform;

            Vector3 position8 = new Vector3(-20f, 0.5f, -123f);
            GameObject bee8 = Instantiate(saxBeeEnemy, position8 + transform.position, Quaternion.identity);
            bee8.transform.parent = transform;

            Vector3 position9 = new Vector3(-38f, 0.5f, -123f);
            GameObject bee9 = Instantiate(saxBeeEnemy, position9 + transform.position, Quaternion.identity);
            bee9.transform.parent = transform;

            Vector3 positionSun1 = new Vector3(-33.5f, 8f, -71f);
            GameObject sun1 = Instantiate(sunEnemy, positionSun1 + transform.position, Quaternion.identity);
            sun1.transform.parent = transform;
            sun1.transform.Rotate(0f, -90f, 0f);

            Vector3 positionSun2 = new Vector3(-22.5f, 8f, -71f);
            GameObject sun2 = Instantiate(sunEnemy, positionSun2 + transform.position, Quaternion.identity);
            sun2.transform.parent = transform;
            sun2.transform.Rotate(0f, 90f, 0f);

            Vector3 positionSun3 = new Vector3(-27.5f, 8f, -103f);
            GameObject sun3 = Instantiate(sunEnemy, positionSun3 + transform.position, Quaternion.identity);
            sun3.transform.parent = transform;
            sun3.transform.Rotate(0f, 90f, 0f);

            Vector3 positionSun4 = new Vector3(-33.5f, 8f, -123f);
            GameObject sun4 = Instantiate(sunEnemy, positionSun4 + transform.position, Quaternion.identity);
            sun4.transform.parent = transform;
            sun4.transform.Rotate(0f, -90f, 0f);
        }

        int newEnemyDiff = getEnemyDifficulty + 1;
        PlayerPrefs.SetInt("EnemyDifficulty", newEnemyDiff);
    }
}
