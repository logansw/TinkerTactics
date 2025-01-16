using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class InternalClock
{
    public GameObject Parent;
    public Action e_OnTimerDone;
    private float _timeElapsed;
    private float _timeToWait;

    public InternalClock(float timeToWait, GameObject gameObject)
    {
        _timeToWait = timeToWait;
        ClockManager.s_Instance.AddClock(this);
        Parent = gameObject;
    }

    public void Reset()
    {
        _timeElapsed = 0;
    }

    // Update is called once per frame
    public void Tick()
    {
        _timeElapsed += Time.deltaTime;
        if (_timeElapsed >= _timeToWait)
        {
            _timeElapsed = 0;
            e_OnTimerDone?.Invoke();
        }
    }

    public void SetTimeToWait(float timeToWait)
    {
        _timeToWait = timeToWait;
    }

    public void IncreaseTime(float additionalTime)
    {
        _timeToWait += additionalTime;
    }

    public void Delete()
    {
        ClockManager.s_Instance.RemoveClock(this);
    }
}