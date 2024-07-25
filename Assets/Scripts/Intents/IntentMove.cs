using UnityEngine;
using System.Collections.Generic;

public class IntentMove : Intent
{
    public override void Initialize(Enemy enemy)
    {
        IconColor = new Color32(92, 152, 224, 255);
        Value = enemy.MovementSpeed;
    }

    public override string GetValueText()
    {
        return Value.ToString();
    }

    public override string GetAbbreviationText()
    {
        return "MOVE";
    }
}