using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MotionStateManager : MonoBehaviour
{
    [SerializeField] public TextMeshPro statusLabel;

    private bool _trackingLeft = false;
    private bool _trackingRight = false;
    private bool _trackingSuccess = false;

    [SerializeField] public TrackingTarget leftTrackingTarget;
    [SerializeField] public TrackingTarget rightTrackingTarget;
    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform rightHand;
    
    [SerializeField] private float _matchVal;
    [SerializeField] public float _matchPower = 1f;
    private float _mapScale = 3f;

    public void SetTrackingLeft(bool b) { _trackingLeft = b;}
    public void SetTrackingRight(bool b) { _trackingRight = b;}
    public TrackingTarget GetTrackingLeft() {return leftTrackingTarget;}
    public TrackingTarget GetTrackingRight() {return rightTrackingTarget;}
    public float GetMatchVal() { return _matchVal;}

    private void UpdateMatchVal()
    {
        //Perhaps inefficient - rn we pull these at each update in case the user has switched from hand tracking <> controllers or hands arent showing yet
        leftHand = GameObject.FindWithTag("LeftHand").transform;
        rightHand = GameObject.FindWithTag("RightHand").transform;

        var distLeft = Vector3.Distance(leftHand.position, leftTrackingTarget.transform.position);
        var distRight = Vector3.Distance(rightHand.position, rightTrackingTarget.transform.position);

        var matchLeftAdjusted = 1-Mathf.Clamp(ExtensionMethods.map(distLeft, 0, _mapScale, 0, 1), 0, 1);
        var matchRightAdjusted = 1-Mathf.Clamp(ExtensionMethods.map(distRight, 0, _mapScale, 0, 1), 0, 1);

        _matchVal = Mathf.Pow(((matchLeftAdjusted + matchRightAdjusted) / 2), _matchPower);

        // statusLabel.text = "Dist left: " + distLeft + "\nDist right" + distRight;
        //statusLabel.text = "Dist left: " + distLeft + "\nDist left adjusted" + matchLeftAdjusted+"\nDist right: " + distRight + "\nDist right adjusted" + matchRightAdjusted;
        statusLabel.text = "Match val" + _matchVal;
    }
    
    private void Update()
    {
        //NOTE: Comment this out if you want to control manually for testing
        //UpdateMatchVal();
    }

    #region Utility

    public static class ExtensionMethods
    {
        public static float map(float s, float a1, float a2, float b1, float b2)
        {
            return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
        }
    }

    #endregion
}
