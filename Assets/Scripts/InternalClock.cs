using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class InternalClock
{
    public Action e_OnTimerDone;
    private float _timeElapsed;
    private float _timeToWait;

    public InternalClock(float timeToWait)
    {
        _timeToWait = timeToWait;
        ClockManager.s_Instance.AddClock(this);
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
}