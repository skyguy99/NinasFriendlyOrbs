using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    [SerializeField] public MotionStateManager motionStateManager;

    [SerializeField] public Material skybox;
    [SerializeField] public GameObject orb;
    [SerializeField] public ParticleSystem[] particleSystems;

    [SerializeField] public Color skyboxColorMin;
    [SerializeField] public Color skyboxColorMax;

    private void UpdateSkybox(float f)
    {
        Color c = Color.Lerp(skyboxColorMin, skyboxColorMax, Mathf.Pow(f,1.5f));
        skybox.SetColor("_Top", c*0.4f);
        skybox.SetColor("_Bottom", c);
    }

    private void UpdateOrb(float f)
    {
        orb.GetComponent<Renderer>().material.SetFloat("_EmissiveMult", Mathf.Pow(f,2f));
    }

    private void UpdateParticleSystem(float f)
    {
        var emitVal = MotionStateManager.ExtensionMethods.map(f, 0, 1, 0.1f, 5.5f);
        var speedVal = MotionStateManager.ExtensionMethods.map(f, 0, 1, 0.1f, 1f);

        foreach (ParticleSystem p in particleSystems)
        {
            p.emissionRate = emitVal;
            p.startSpeed = speedVal;
        }
            
    }
    private void Update()
    {
        UpdateSkybox(motionStateManager.GetMatchVal());
        UpdateOrb(motionStateManager.GetMatchVal());
        UpdateParticleSystem(motionStateManager.GetMatchVal());
    }
}
