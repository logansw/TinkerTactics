using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
    private float _current;
    public float Current
    {
        get
        {
            return _current;
        }
        set
        {
            _current = Mathf.Clamp(value, Min, Max);
            e_OnStatChanged?.Invoke();
        }
    }
    public float InitialValue;
    private float _max;
    public float Max
    {
        get
        {
            return _max;
        }
        set
        {
            _max = value;
            e_OnStatChanged?.Invoke();
        }
    }
    private float _min;
    public float Min
    {
        get
        {
            return _min;
        }
        set
        {
            _min = value;
            e_OnStatChanged?.Invoke();
        }
    }
    public Action e_OnStatChanged;
    public int CurrentInt => (int)Current;
    public int MinInt => (int)Min;
    public int MaxInt => (int)Max;
    
    public void Reset()
    {
        Current = Max;
    }

    public Stat(float min, float max, float current)
    {
        Min = min;
        Max = max;
        Current = current;
    }
}