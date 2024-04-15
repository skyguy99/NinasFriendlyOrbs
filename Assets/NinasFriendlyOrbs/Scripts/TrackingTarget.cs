using System;
using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        motionStateManager = GetComponentInParent<MotionStateManager>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LeftHand" && targetSide == TargetSide.Left)
            motionStateManager.SetTrackingLeft(true);
        
        if (other.tag == "RightHand" && targetSide == TargetSide.Right)
            motionStateManager.SetTrackingRight(true);
        
        Debug.Log(other.name);
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "LeftHand" && targetSide == TargetSide.Left)
            motionStateManager.SetTrackingLeft(false);
        
        if (other.tag == "RightHand" && targetSide == TargetSide.Right)
            motionStateManager.SetTrackingRight(false);
    }
}
