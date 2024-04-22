using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class MotionsManager : MonoBehaviour
{
    [SerializeField] private MotionStateManager motionStateManager;
    [SerializeField] public int currentMotion;
    
    [SerializeField] public AudioSource audioSource;
    [SerializeField] public AudioClip introAudioClip;
    [SerializeField] public AudioClip[] motionAudioClips;

    [SerializeField] public float trackerDefaultSpeed = 0.25f;
    [SerializeField] private float trackerCurrentSpeed;
    [SerializeField] public int motionLoopCount = 3;
    private int currentMotionLoopCount;

    private float t;
    
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
    
    //TODO: Output countdown with copy to a label
    IEnumerator Countdown()
    {
        PlayIntroAudioClip();
        
        int count = 3;
        while (count > 0) {
           
            // display something...
            yield return new WaitForSeconds(1);
            count --;
            Debug.Log("Start countdown: "+count);
        }
       
        // Start
        Debug.Log("== START GAME ==");

        t = 0f;
        UpdateMotionAudioClip();
        
        motionStateManager.GetTrackingLeft().SetMotionSpeed(trackerDefaultSpeed);
        motionStateManager.GetTrackingRight().SetMotionSpeed(trackerDefaultSpeed);
    }
    
    //TODO: Output countdown with copy to a label
    IEnumerator WaitToStartNextMotion()
    {
        int count = 3;
        while (count > 0) {
           
            // display something...
            yield return new WaitForSeconds(1);
            count --;
            Debug.Log("Next motion countdown: "+count);
        }
       
        // Start
        Debug.Log("== SWITCHING MOTION TO INDEX: "+currentMotion+" ==");

        t = 0f;
        UpdateMotionAudioClip();
        
        motionStateManager.GetTrackingLeft().SetMotionSpeed(trackerDefaultSpeed);
        motionStateManager.GetTrackingRight().SetMotionSpeed(trackerDefaultSpeed);
        
        motionStateManager.GetTrackingLeft().SwitchMotionSpline(motions[currentMotion].splineLeft.GetComponent<SplineComputer>());
        motionStateManager.GetTrackingRight().SwitchMotionSpline(motions[currentMotion].splineRight.GetComponent<SplineComputer>());
    }

    private void Start()
    {
        motionStateManager = GetComponent<MotionStateManager>();
        StartCoroutine(Countdown());
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
        motionStateManager.GetTrackingLeft().SetMotionSpeed(0f);
        motionStateManager.GetTrackingRight().SetMotionSpeed(0f);
        
        if (currentMotion < motions.Count-1)
            currentMotion++;
        else
            currentMotion = 0;

        StartCoroutine(WaitToStartNextMotion());
    }
    
    public void PlayIntroAudioClip()
    {
        audioSource.clip = introAudioClip;
        audioSource.Play();
    }

    public void UpdateMotionAudioClip()
    {
        audioSource.clip = motionAudioClips[currentMotion];
        audioSource.Play();
    }

    public void PrintDurationOfCurrentMotion()
    {
        //This is for audio recording purposes
        t += Time.deltaTime;
        Debug.Log("Current motion"+currentMotion+" | Time: "+t);
        
        // -- Current values --
        //Intro = 3s
        //Motion A = 10.1s
        //Motion B = (3s countdown transition) + 8.07s
    }

    private void Update()
    {
        //PrintDurationOfCurrentMotion();
    }
}
