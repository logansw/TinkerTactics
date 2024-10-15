using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockManager : Singleton<ClockManager>
{
    private List<InternalClock> _clocks = new List<InternalClock>();

    // Update is called once per frame
    void Update()
    {
        foreach (InternalClock clock in _clocks)
        {
            clock.Tick();
        }
    }

    public void AddClock(InternalClock clock)
    {
        _clocks.Add(clock);
    }
}
