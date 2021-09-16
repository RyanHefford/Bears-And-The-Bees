using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    GameObject player;
    Vector3 cameraOffset;
    private float smoothSpeed = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cameraOffset = new Vector3(-7, 9, 7);
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = player.transform.position + cameraOffset;
        //desiredPosition.x = Mathf.Clamp(desiredPosition.x, westEdge + cameraXOffset, eastEdge - cameraXOffset);
        //desiredPosition.y = Mathf.Clamp(desiredPosition.y, southEdge + cameraYOffset, northEdge - cameraYOffset);

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

    }
}
