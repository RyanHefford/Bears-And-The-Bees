using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFloat : MonoBehaviour
{
    private float itemFloatHeight = 0.5f;
    private float degreesPerSec = 10f;
    private float frequency = 0.5f;
    private float amplitude = 0.5f;

    // Position Storage Variables
    Vector3 posOffset;
    Vector3 tempPos = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        posOffset = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSec, 0f), Space.World);

        tempPos = posOffset;
        tempPos.y += Mathf.Cos(Time.fixedTime * Mathf.PI * frequency) * amplitude + itemFloatHeight;

        transform.position = tempPos;
    }
}
