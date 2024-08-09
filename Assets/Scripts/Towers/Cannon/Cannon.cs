using UnityEngine;
using System.Collections.Generic;

public class Cannon : Tower
{
    public float ExplosionRadius;

    protected override void Update()
    {
        base.Update();
    }

    public override string GetTooltipText()
    {
        return "Cannon";
    }
}