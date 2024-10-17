using System;
using UnityEngine;

[Serializable]
public abstract class StatBase<T> where T : struct
{
    private bool _initialized;
    [SerializeField] private T baseValue;
    public T Base
    {
        get
        {
            return baseValue;
        }
        set
        {
            baseValue = value;
        }
    }
    private T _current;
    public T Current
    {
        get
        {
            TryInitialize();
            return _current;
        }
        set
        {
            TryInitialize();
            _current = Clamp(value, _min, _max);

            e_OnStatChanged?.Invoke();
            if (_current.Equals(_max))
            {
                e_OnStatMax?.Invoke();
            }
            if (_current.Equals(_min))
            {
                e_OnStatMin?.Invoke();
            }
        }
    }
    private T _calculatedFinal;
    public T CalculatedFinal
    {
        get
        {
            TryInitialize();
            return _calculatedFinal;
        }
        set
        {
            TryInitialize();
            _calculatedFinal = value;
        }
    }
    protected T _min;
    protected T _max;

    public event Action e_OnStatChanged;
    public event Action e_OnStatMax;
    public event Action e_OnStatMin;

    public abstract void ModifyStat(T amount);
    public abstract T Clamp(T value, T min, T max);
    public abstract void SetBounds();
    protected virtual void Initialize()
    {
        _initialized = true;
        SetBounds();
        Current = baseValue;
        CalculatedFinal = Current;
    }
    public void Reset()
    {
        Current = baseValue;
    }

    private void TryInitialize()
    {
        if (!_initialized) { Initialize(); }
    }
}