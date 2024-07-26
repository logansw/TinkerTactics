using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "IntentDefend", menuName = "Intent/Defend", order = 2)]
public class IntentDefend : Intent
{
    public IntentDefend(Enemy enemy, int value) : base(enemy, value)
    {
        IconColor = new Color32(83, 191, 237, 255);
    }

    public override void Execute()
    {
        // TODO:
    }

    public override void Calculate()
    {
        ValueCurrent = ValueBase;
    }

    public override string GetValueText()
    {
        return ValueBase.ToString();
    }

    public override string GetAbbreviationText()
    {
        return "DEF";
    }
}