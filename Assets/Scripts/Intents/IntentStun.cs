using UnityEngine;
using System.Collections.Generic;

public class IntentStun : Intent
{
    public override void Initialize(Enemy enemy)
    {
        IconColor = new Color32(255, 240, 128, 255);
        if (enemy.EffectTracker.HasEffect<EffectStun>(out EffectStun effectStun))
        {
            Value = effectStun.Duration;
        }
    }

    public override string GetValueText()
    {
        return Value.ToString();
    }

    public override string GetAbbreviationText()
    {
        return "STUN";
    }
}