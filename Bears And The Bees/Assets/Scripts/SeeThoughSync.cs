using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeThoughSync : MonoBehaviour
{
    public static int posID = Shader.PropertyToID("_position");
    public static int sizeID = Shader.PropertyToID("_size");

    public Material wallMaterial;
    public Camera mainCamera;
    public LayerMask mask;

    private float smoothnessSpeed = 3f;
    private float currentSize = 0f;
    private bool isBehindWall = false;
    public float maxHoleSize = 2f;

    private void Start()
    {
        wallMaterial.SetFloat(sizeID, currentSize);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = mainCamera.transform.position - transform.position;
        Ray ray = new Ray(transform.position, direction.normalized);

        if (Physics.Raycast(ray, 300, mask))
        {
            if (currentSize < maxHoleSize)
            {
                currentSize += smoothnessSpeed * Time.deltaTime;
                wallMaterial.SetFloat(sizeID, currentSize);
            }

        }
        else
        {
            if (currentSize > 0)
            {
                currentSize -= smoothnessSpeed * Time.deltaTime;
                wallMaterial.SetFloat(sizeID, currentSize);
            }
        }

        Vector3 view = mainCamera.WorldToViewportPoint(transform.position);
        wallMaterial.SetVector(posID, view);
    }
}
