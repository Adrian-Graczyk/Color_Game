using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PatrolPath : MonoBehaviour
{
    private NavPoint[] navPoints;


    void Awake()
    {
        navPoints = GetComponentsInChildren<NavPoint>();
    }

    public NavPoint StartNavPoint()
    {
        return navPoints[0];
    }
    
    public NavPoint NextNavPoint(NavPoint lastPoint)
    {
        int lastPointIndex = Array.IndexOf(navPoints, lastPoint);
        int nextPointIndex = lastPointIndex + 1;

        if (nextPointIndex >= navPoints.Length)
        {
            nextPointIndex = 0;
        }

        return navPoints[nextPointIndex];
    }
}
