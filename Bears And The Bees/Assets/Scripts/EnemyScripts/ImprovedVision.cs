using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImprovedVision : MonoBehaviour
{
    public float distance = 10;
    public float angle = 60;
    public float height = 1.0f;
    private Color color = Color.red;

    private Mesh mesh;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Mesh CreateWedgeMesh()
    {
        Mesh tempMesh = new Mesh();

        int numTriangles = 8;
        int numVertices = numTriangles * 3;

        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[numVertices];

        Vector3 bottomCenter = Vector3.zero;
        Vector3 bottomLeft = Quaternion.Euler(0, -angle, 0) * Vector3.forward * distance;
        Vector3 bottomRight = Quaternion.Euler(0, angle, 0) * Vector3.forward * distance;

        Vector3 topCenter = bottomCenter + Vector3.up * height;
        Vector3 topLeft = bottomLeft + Vector3.up * height;
        Vector3 topRight = bottomRight + Vector3.up * height;



        return tempMesh;
    }
}
