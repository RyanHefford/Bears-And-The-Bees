using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    GameObject player;
    Vector3 cameraOffset;
    private float smoothSpeed = 0.1f;
    private float sensitivity = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cameraOffset = new Vector3(-7, 7, 7);
    }

    void FixedUpdate()
    {
        float rotateHorizontal = Input.GetAxis("Mouse X");

        Quaternion camTurnAngle = Quaternion.AngleAxis(rotateHorizontal * sensitivity, Vector3.up);
        cameraOffset = camTurnAngle * cameraOffset;

        Vector3 newPos = player.transform.position + cameraOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, smoothSpeed);

        transform.LookAt(player.transform);
    }
}
