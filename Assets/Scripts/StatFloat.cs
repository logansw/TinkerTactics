using System;
using UnityEngine;

[Serializable]
public class StatFloat
{
    // Expose these fields to Unity Inspector
    [SerializeField] private float baseValue;  // Base stat value
    [SerializeField] private float minValue;   // Minimum stat value
    [SerializeField] private float maxValue;   // Maximum stat value

    // Current stat value
    private float _current;

    // Properties for accessing Base, Min, and Max
    public float Base { get => baseValue; }
    public float Min { get => minValue; }
    public float Max { get => maxValue; }

    // Current stat with clamping between Min and Max
    public float Current
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
    public void ModifyStat(float amount)
    {
        Current += amount;
    }

    // Method to reset stat to its base value
    public void ResetStat()
    {
        Current = Base;
    }
}
