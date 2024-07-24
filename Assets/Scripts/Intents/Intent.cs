using UnityEngine;
using System.Collections.Generic;

public abstract class Intent
{
    public Color32 IconColor;
    public int Value;

    public abstract string GetValueText();
    public abstract string GetAbbreviationText();

    public virtual void Initialize(int value)
    {
        Value = value;
    }
}