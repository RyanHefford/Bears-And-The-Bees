using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRandomSpawn : MonoBehaviour
{
    public GameObject[] itemPool;

    // Start is called before the first frame update
    void Start()
    {
        GameObject newItem = Instantiate(itemPool[Random.Range(0, itemPool.Length)]);
        newItem.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
