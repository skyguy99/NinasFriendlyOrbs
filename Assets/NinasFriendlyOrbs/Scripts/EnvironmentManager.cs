using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnvironmentManager : MonoBehaviour
{
    [SerializeField] public MotionStateManager motionStateManager;

    [SerializeField] public Material skybox;
    [SerializeField] public GameObject orb;
    [SerializeField] public ParticleSystem[] particleSystems;
    [SerializeField] public Light envLight;
    [SerializeField] public Volume volumeMin;
    [SerializeField] public Volume volumeMax;

    [SerializeField] public Color skyboxColorMin;
    [SerializeField] public Color skyboxColorMax;
    
    [SerializeField] public Color orbColorAMin;
    [SerializeField] public Color orbColorAMax;
    [SerializeField] public Color orbColorBMin;
    [SerializeField] public Color orbColorBMax;
    [SerializeField] public Color orbLightMin;
    [SerializeField] public Color orbLightMax;

    [SerializeField] public Color envLightMin;
    [SerializeField] public Color envLightMax;

    [SerializeField] float butterflyTexMinVal = 0.42f;
    [SerializeField] float butterflyTexMaxVal = -0.15f;

    private void UpdateVolumes(float f)
    {
        volumeMax.weight = f;
        volumeMin.weight = 1 - f;
    }
    private void UpdateEnvLight(float f)
    {
        Color colLight = Color.Lerp(envLightMin, envLightMax, Mathf.Pow(f,1.5f));
        envLight.color = colLight;
    }
    private void UpdateSkybox(float f)
    {
        Color c = Color.Lerp(skyboxColorMin, skyboxColorMax, Mathf.Pow(f,1.5f));
        skybox.SetColor("_Top", c*0.4f);
        skybox.SetColor("_Bottom", c);
    }

    private void UpdateOrb(float f)
    {

        Color colA = Color.Lerp(orbColorAMin, orbColorAMax, Mathf.Pow(f,1.5f));
        Color colB = Color.Lerp(orbColorBMin, orbColorBMax, Mathf.Pow(f,1.5f));
        Color colLight = Color.Lerp(orbLightMin, orbLightMax, Mathf.Pow(f,1.5f));

        orb.GetComponentInChildren<Light>().color = colLight;
        orb.GetComponent<Renderer>().material.SetColor("_Color_A", colA);
        orb.GetComponent<Renderer>().material.SetColor("_Color_B", colB);
    }
    
    private void UpdateParticleSystem(float f)
    {
        var emitVal = MotionStateManager.ExtensionMethods.map(f, 0, 1, 2.5f, 20f);
        var speedVal = MotionStateManager.ExtensionMethods.map(f, 0, 1, 0.1f, 1f);
        var texVal = MotionStateManager.ExtensionMethods.map(f, 0, 1, butterflyTexMinVal, butterflyTexMaxVal);

        foreach (ParticleSystem p in particleSystems)
        {
            p.emissionRate = emitVal;
            p.startSpeed = speedVal;
            
            
            //This was when I tried to set custom particle data
            // List<Vector4> customData = new List<Vector4>();
            // p.GetCustomParticleData(customData, ParticleSystemCustomData.Custom1);
            //
            // int particleCount = p.particleCount;
            // ParticleSystem.Particle[] particles = new ParticleSystem.Particle[particleCount];
            // p.GetParticles(particles);
            //
            // for (int i = 0; i < particles.Length; i++)
            // {
            //     customData[i] = new Vector4(1, texVal, 0, 0);
            // }
            // p.SetCustomParticleData(customData, ParticleSystemCustomData.Custom1);
        }
    }

    private void OnDisable()
    {
        //Reset values
        orb.GetComponent<Renderer>().material.SetFloat("_EmissiveMult", 2f);
        
        skybox.SetColor("_Top", skyboxColorMin*0.4f);
        skybox.SetColor("_Bottom", skyboxColorMin);

        orb.GetComponentInChildren<Light>().color = orbLightMin;
        orb.GetComponent<Renderer>().material.SetColor("_Color_A", orbColorAMin);
        orb.GetComponent<Renderer>().material.SetColor("_Color_B", orbColorAMax);
    }
    
    private void Update()
    {
        UpdateEnvLight(motionStateManager.GetMatchVal());
        UpdateSkybox(motionStateManager.GetMatchVal());
        UpdateOrb(motionStateManager.GetMatchVal());
        UpdateParticleSystem(motionStateManager.GetMatchVal());
        UpdateVolumes(motionStateManager.GetMatchVal());
    }
}
