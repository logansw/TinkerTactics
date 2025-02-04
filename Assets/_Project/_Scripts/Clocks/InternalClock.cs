using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class InternalClock
{
    public GameObject Parent;
    public Action e_OnTimerDone;
    public float TimeElapsed { get; private set; }
    public float TimeToWait { get; private set; }
    public bool Paused { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="timeToWait">Time until clock is complete</param>
    /// <param name="parent">The parent game object that created clock</param>
    public InternalClock(float timeToWait, GameObject parent)
    {
        TimeToWait = timeToWait;
        ClockManager.s_Instance.AddClock(this);
        Parent = parent;
    }

    public void Reset()
    {
        TimeElapsed = 0;
    }

    // Update is called once per frame
    public void Tick()
    {
        if (Paused) { return; }
        
        TimeElapsed += Time.deltaTime;
        if (TimeElapsed >= TimeToWait)
        {
            TimeElapsed = 0;
            e_OnTimerDone?.Invoke();
        }
    }

    public void SetTimeToWait(float timeToWait)
    {
        TimeToWait = timeToWait;
    }

    public void IncreaseTime(float additionalTime)
    {
        TimeToWait += additionalTime;
    }

    public void Delete()
    {
        ClockManager.s_Instance.RemoveClock(this);
    }
}