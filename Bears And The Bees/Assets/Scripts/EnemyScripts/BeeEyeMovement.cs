using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeEyeMovement : MonoBehaviour
{
    private bool currentlyChasing = false;
    public Vector3 alertReposition = new Vector3(0,0.35f,-0.5f);


    public void UpdatePosition(bool isChasing)
    {
        if (isChasing && !currentlyChasing)
        {
            currentlyChasing = true;
            transform.Translate(alertReposition);

        }
        else if(!isChasing && currentlyChasing)
        {
            currentlyChasing = false;
            transform.Translate(-alertReposition);
        }
    }
}
