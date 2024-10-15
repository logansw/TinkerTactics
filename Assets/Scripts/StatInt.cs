using System;
using UnityEngine;

[Serializable]
public class StatInt
{
    // Expose these fields to Unity Inspector
    [SerializeField] private int baseValue;  // Base stat value
    [SerializeField] private int minValue;   // Minimum stat value
    [SerializeField] private int maxValue;   // Maximum stat value

    // Current stat value
    private int _current;

    // Properties for accessing Base, Min, and Max
    public int Base { get => baseValue; }
    public int Min { get => minValue; }
    public int Max { get => maxValue; }

    // Current stat with clamping between Min and Max
    public int Current
    {
        get => _current;
        set
        {
            // Clamp value between Min and Max
            _current = Mathf.Clamp(value, Min, Max);

            // Invoke events based on the new value
            e_OnStatChanged?.Invoke();
            if (_current == Max)
                e_OnStatMax?.Invoke();
            if (_current == Min)
                e_OnStatMin?.Invoke();
        }
    }

    // Events to notify when stat changes
    public event Action e_OnStatChanged;
    public event Action e_OnStatMax;
    public event Action e_OnStatMin;

    // Method to modify the stat value
    public void ModifyStat(int amount)
    {
        Current += amount;
    }

    // Method to reset stat to its base value
    public void ResetStat()
    {
        Current = Base;
    }
}
