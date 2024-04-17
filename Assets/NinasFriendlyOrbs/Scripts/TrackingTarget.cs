using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class TrackingTarget : MonoBehaviour
{
    public enum TargetSide
    {
        Right,
        Left
    }
    [SerializeField] public TargetSide targetSide;
    [SerializeField] private MotionStateManager motionStateManager;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LeftHand" && targetSide == TargetSide.Left)
            motionStateManager.SetTrackingLeft(true);
        
        if (other.tag == "RightHand" && targetSide == TargetSide.Right)
            motionStateManager.SetTrackingRight(true);
        
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "LeftHand" && targetSide == TargetSide.Left)
            motionStateManager.SetTrackingLeft(false);
        
        if (other.tag == "RightHand" && targetSide == TargetSide.Right)
            motionStateManager.SetTrackingRight(false);
    }

    public void SwitchMotionSpline(SplineComputer s)
    {
        GetComponent<SplineFollower>().spline = s;
    }

    public void SetMotionSpeed(float s)
    {
        GetComponent<SplineFollower>().followSpeed = s;
    }
}
