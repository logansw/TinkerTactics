using UnityEngine;
using System.Collections.Generic;

public class Cannon : Tower
{
    protected override void Awake()
    {
        base.Awake();
        Attack = GetComponent<CannonAttack>();
        Attack.Initialize();
        RangeIndicator = GetComponentInChildren<RangeIndicator>();
        RangeIndicator.Initialize(this);
    }

    protected override void Update()
    {
        base.Update();
    }

    public override string GetTooltipText()
    {
        return "Cannon";
    }
}