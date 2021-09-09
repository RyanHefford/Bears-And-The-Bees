using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingBottle : MonoBehaviour
{
    public float rotateDegree;
    public float rotateInterval;
    private float rotateTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(-10, 0, 20);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, -50 * Time.deltaTime, 0);
    }

    private void FixedUpdate()
    {
        
    }
}
