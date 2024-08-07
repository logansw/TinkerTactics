using UnityEngine;
using System.Collections.Generic;

public class Cannon : Tower
{
    protected override void Awake()
    {
        base.Awake();
        Attack = new CannonAttack();
        Attack.Initialize();
        _rangeIndicator = GetComponentInChildren<RangeIndicator>();
        _rangeIndicator.Initialize(this);
    }

    void Update()
    {
        Attack.Initialize();
    }

    public override string GetTooltipText()
    {
        return "Cannon";
    }
}