using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaValues : MonoBehaviour
{
    public float length;
    public float entranceDist;
    public float exitDist;

    public float GetAreaLength()
    {
        return length;
    }

    public float GetEntranceDist()
    {
        return entranceDist;
    }

    public float GetExitDist()
    {
        return exitDist;
    }


}
