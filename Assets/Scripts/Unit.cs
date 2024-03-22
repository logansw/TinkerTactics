using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public UnitSO UnitSO;
    public Health Health;
    private Healthbar _healthbar;
    [HideInInspector] public float MovementSpeed;
    [HideInInspector] public float Armor;
    [HideInInspector] public List<float> DamageMultipliers;

    public delegate void UnitAction(Unit unit);
    public UnitAction e_OnUnitDeath;
    public UnitAction e_OnUnitBreak;

    public virtual void Awake()
    {
        Initialize();
    }

    public virtual void Initialize()
    {
        float maxHealth = UnitSO.MaxHealth;
        int breakpointCount = UnitSO.SegmentCount;
        Health = new Health(UnitSO.MaxHealth, UnitSO.SegmentCount);
        MovementSpeed = UnitSO.MovementSpeed;
        Armor = UnitSO.Armor;
        _healthbar = GetComponentInChildren<Healthbar>();
        _healthbar.Initialize(Health);
        DamageMultipliers = new List<float>();
    }
}