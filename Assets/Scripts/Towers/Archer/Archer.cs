using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Archer : Tower
{
    protected override void Update()
    {
        base.Update();
    }

    public override string GetTooltipText()
    {
        return "Archer";
    }
}