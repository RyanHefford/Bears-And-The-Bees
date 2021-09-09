using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeThoughSync : MonoBehaviour
{
    public static int posID = Shader.PropertyToID("_position");
    public static int sizeID = Shader.PropertyToID("_size");

    public Material[] wallMaterials;
    public Camera mainCamera;
    public LayerMask mask;

    private float smoothnessSpeed = 3f;
    private float currentSize = 0f;
    public float maxHoleSize = 2f;

    private void Start()
    {
        foreach (Material currMaterial in wallMaterials)
        {
            currMaterial.SetFloat(sizeID, currentSize);
        }
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
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
                foreach (Material currMaterial in wallMaterials)
                {
                    currMaterial.SetFloat(sizeID, currentSize);
                }
            }

        }
        else
        {
            if (currentSize > 0)
            {
                currentSize -= smoothnessSpeed * Time.deltaTime;
                foreach (Material currMaterial in wallMaterials)
                {
                    currMaterial.SetFloat(sizeID, currentSize);
                }
            }
        }

        Vector3 view = mainCamera.WorldToViewportPoint(transform.position);
        foreach (Material currMaterial in wallMaterials)
        {
            currMaterial.SetVector(posID, view);
        }
    }
}
