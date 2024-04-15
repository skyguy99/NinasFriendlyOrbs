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

    public void SetTrackingLeft(bool b) { _trackingLeft = b;}
    
    public void SetTrackingRight(bool b) { _trackingRight = b;}

    private void Update()
    {
        statusLabel.text = "Tracking left:"+_trackingLeft+"\nTracking right:"+_trackingRight+"\nTracking success: " + (_trackingLeft && _trackingRight);
    }
}
