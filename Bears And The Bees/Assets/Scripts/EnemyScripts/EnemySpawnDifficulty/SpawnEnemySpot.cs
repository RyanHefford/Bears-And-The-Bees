using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemySpot : MonoBehaviour
{
    public GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        GameObject enemy = Instantiate(enemies[Random.Range(0, enemies.Length)], transform.position, Quaternion.identity);
        enemy.transform.rotation = transform.rotation;
        enemy.transform.parent = transform.parent;
    }
}
