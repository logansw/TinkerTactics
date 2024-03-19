using System;
using System.Collections;
using System.Collections.Generic;

public class Health
{
    public float MaxHealth { get; private set; }
    private float _upperBreakpoint;
    private float _lowerBreakpoint;
    private float _segmentHealth;
    public Action e_OnHealthDepleted;
    public Action e_OnHealthBreak;
    public delegate void HealthAction(float currentHealth, float maxHealth);
    public HealthAction e_OnHealthChanged;

    private float _currentHealth;
    public float CurrentHealth
    {
        get { return _currentHealth; }
        set
        {
            if (value > _upperBreakpoint)
            {
                _currentHealth = _upperBreakpoint;
            }
            else if (value < _lowerBreakpoint)
            {
                _currentHealth = _lowerBreakpoint;
            }
            else
            {
                _currentHealth = value;
            }
        }
    }

    public int SegmentCount { get; private set; }

    public Health(int maxHealth, int segmentCount)
    {
        MaxHealth = maxHealth;
        SegmentCount = segmentCount;
        _upperBreakpoint = maxHealth;
        _segmentHealth = maxHealth / segmentCount;
        _lowerBreakpoint = _upperBreakpoint - _segmentHealth;
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(int postMitigationDamage)
    {
        CurrentHealth -= postMitigationDamage;
        e_OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
        if (CurrentHealth <= 0)
        {
            e_OnHealthDepleted?.Invoke();
        }
        else if (CurrentHealth == _lowerBreakpoint)
        {
            // Trigger enemy health breakpoint event
            _upperBreakpoint -= _segmentHealth;
            _lowerBreakpoint -= _segmentHealth;
            e_OnHealthBreak?.Invoke();
        }
    }

    public void Heal(int amount)
    {
        CurrentHealth += amount;
    }
}