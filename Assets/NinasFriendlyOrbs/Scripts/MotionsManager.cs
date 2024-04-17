using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionsManager : MonoBehaviour
{
    [SerializeField] public int currentMotion;

    [SerializeField] public GameObject[][] motionSplines; //[i][j] where i=currentMotion, j= [leftSpline, rightSpline]

    [SerializeField] public int motionLoopCount = 3;
    private int currentMotionCount;

    private void MotionDidFinish() //called by spline on end
    {
        if (currentMotionCount < motionLoopCount)
            motionLoopCount++;
        else
            motionLoopCount = 0;
    }

    private void SwitchMotion()
    {
        
    }
    private void Update()
    {
        
    }
}
