using UnityEngine;
using System.Collections.Generic;

public class IntentMove : Intent
{
    public override string GetValueText()
    {
        return Value.ToString();
    }

    public override string GetAbbreviationText()
    {
        return "MOVE";
    }
}