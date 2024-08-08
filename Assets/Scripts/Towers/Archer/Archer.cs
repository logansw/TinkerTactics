using UnityEngine;
using System.Collections.Generic;

public class Archer : Tower
{
    protected override void Awake()
    {
        base.Awake();
        Attack = GetComponent<ArcherAttack>();
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
        return "Archer";
    }
}