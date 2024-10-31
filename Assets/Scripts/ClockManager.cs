using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockManager : Singleton<ClockManager>
{
    private List<InternalClock> _clocks = new List<InternalClock>();
    private List<InternalClock> _clocksToRemove = new List<InternalClock>();

    // Update is called once per frame
    void Update()
    {
        for (int i = _clocksToRemove.Count - 1; i >= 0; i--)
        {
            _clocks.Remove(_clocksToRemove[i]);
            _clocksToRemove.RemoveAt(i);
        }
        foreach (InternalClock clock in _clocks)
        {
            clock.Tick();
        }
    }

    public void AddClock(InternalClock clock)
    {
        _clocks.Add(clock);
    }

    public void RemoveClock(InternalClock clock)
    {
        _clocksToRemove.Add(clock);
    }
}
