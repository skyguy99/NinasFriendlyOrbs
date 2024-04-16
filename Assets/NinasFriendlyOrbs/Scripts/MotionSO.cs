using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Motion", menuName = "ScriptableObjects/Motion")]
public class MotionSO : ScriptableObject
{
    public enum MotionName
    {
        MotionA,
        MotionB
    }

    public MotionName motionName;
}
