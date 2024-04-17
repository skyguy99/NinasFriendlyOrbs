using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class MotionsManager : MonoBehaviour
{
    [SerializeField] private MotionStateManager motionStateManager;
    [SerializeField] public int currentMotion;

    [SerializeField] public int motionLoopCount = 3;
    private int currentMotionLoopCount;
    
    [SerializeField] public List<Package<Vector3>> motions;

    [System.Serializable]
    public struct Package<TElement>
    {
        public GameObject splineLeft;
        public GameObject splineRight;
        public Package(GameObject l, GameObject r)
        {
            splineLeft = l;
            splineRight = r;
        }
    }

    private void Start()
    {
        motionStateManager = GetComponent<MotionStateManager>();
    }

    public void MotionDidFinish() //called by spline on end
    {
        if (currentMotionLoopCount < motionLoopCount)
            currentMotionLoopCount++;
        else
        {
            currentMotionLoopCount = 0;
            SwitchMotion();
        }
    }

    private void SwitchMotion()
    {
        if (currentMotion < motions.Count-1)
            currentMotion++;
        else
            currentMotion = 0;
        
        Debug.Log("SWITCHING MOTION TO INDEX: "+currentMotion);
        
        motionStateManager.GetTrackingLeft().SwitchMotionSpline(motions[currentMotion].splineLeft.GetComponent<SplineComputer>());
        motionStateManager.GetTrackingRight().SwitchMotionSpline(motions[currentMotion].splineRight.GetComponent<SplineComputer>());
    }
    private void Update()
    {
        
    }
}
