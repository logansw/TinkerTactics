using System;
using UnityEngine;

[Serializable]
public abstract class StatBase<T> where T : struct
{
    [SerializeField] private T baseValue;
    public T Base { get => baseValue; }
    private T _current;
    public T Current
    {
        get => _current;
        set
        {
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
    protected T _min;
    protected T _max;

    public event Action e_OnStatChanged;
    public event Action e_OnStatMax;
    public event Action e_OnStatMin;

    public abstract void ModifyStat(T amount);
    public abstract T Clamp(T value, T min, T max);
    public abstract void SetBounds();
    public virtual void Initialize()
    {
        SetBounds();
        Current = baseValue;
    }
}