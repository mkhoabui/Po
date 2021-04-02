using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFlowManager : MonoBehaviour
{
    [SerializeField] float slowdownFactor = 0.05f;
    [SerializeField] float slowdownLength = 4.0f;
    [SerializeField] float cooldownTime = 3f;
    [SerializeField] AudioClip timeSlowSound;
    float nextUseTime = 0f;
    bool isTimeSlowed = false;
    float slowdownStops;

    //Cached component references
    AudioSource myAudioSource;

    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextUseTime)
        {
            if (Input.GetKeyDown("space"))
            {
                DoSlowMotion();
                myAudioSource.PlayOneShot(timeSlowSound);
                nextUseTime = Time.time + cooldownTime;
                slowdownStops = Time.time + slowdownLength;
            }
        }
        if (Time.time >= slowdownStops)
        {
            isTimeSlowed = false;
        }
        Time.timeScale += (1.0f / slowdownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0, 1);
    }

    public void DoSlowMotion()
    {
        isTimeSlowed = true;
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        
    }
    public bool IsTimeSlowed()
    {
        return isTimeSlowed;
    }
}
