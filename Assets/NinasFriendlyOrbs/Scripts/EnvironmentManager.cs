using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    [SerializeField] public MotionStateManager motionStateManager;

    [SerializeField] public Material skybox;
    [SerializeField] public GameObject orb;
    [SerializeField] public ParticleSystem particles;

    private void UpdateSkybox(float f)
    {
        
    }

    private void UpdateOrb(float f)
    {
        orb.GetComponent<Renderer>().material.SetColor("_EmissiveColor", Color.white*f);
        Debug.Log(orb.GetComponent<Renderer>().material.GetColor("_EmissiveColor"));
    }

    private void UpdateParticleSystem(float f)
    {
        
    }
    private void Update()
    {
        UpdateSkybox(motionStateManager.GetMatchVal());
        UpdateOrb(motionStateManager.GetMatchVal());
        UpdateParticleSystem(motionStateManager.GetMatchVal());
    }
}
