using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "IntentMove", menuName = "Intent/Move", order = 1)]
public class IntentMove : Intent
{
    public IntentMove(Enemy enemy, int value) : base(enemy, value)
    {
        IconColor = new Color32(92, 152, 224, 255);
    }

    public override void Execute()
    {
        _enemy.Move(ValueCurrent);
    }

    public override void Calculate()
    {
        ValueCurrent = ValueBase;
        if (_enemy.EffectTracker.HasEffect<EffectUnstoppable>(out EffectUnstoppable effectUnstoppable))
        {
            return;
        }
        if (_enemy.EffectTracker.HasEffect<EffectStun>(out EffectStun effectStun))
        {
            ValueCurrent = 0;
        }
        if (_enemy.EffectTracker.HasEffect<EffectChill>(out EffectChill effectChill))
        {
            ValueCurrent = Mathf.Max((int)(ValueCurrent * effectChill.GetSpeedMultiplier()), 1);
        }
    }

    public override string GetValueText()
    {
        return ValueCurrent.ToString();
    }

    public override string GetAbbreviationText()
    {
        return "MOVE";
    }
}