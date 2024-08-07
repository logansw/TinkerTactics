using UnityEngine;
using System.Collections.Generic;

public class Cannon : Tower
{
    protected override void Awake()
    {
        base.Awake();
        attack = new CannonAttack();
        attack.Initialize();
        _rangeHandle.Initialize(attack.Range);
    }

    void Update()
    {
        attack.Initialize();
    }

    public override string GetTooltipText()
    {
        return "Cannon";
    }
}