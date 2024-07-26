using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "IntentStun", menuName = "Intent/Stun", order = 3)]
public class IntentStun : Intent
{
    public IntentStun(Enemy enemy, int value) : base(enemy, value)
    {
        IconColor = new Color32(255, 240, 128, 255);
    }

    public override void Execute()
    {
        // Do Nothing
    }

    public override void Calculate()
    {
        // Do Nothing
    }

    public override string GetValueText()
    {
        return ValueBase.ToString();
    }

    public override string GetAbbreviationText()
    {
        return "STUN";
    }
}