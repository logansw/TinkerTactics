using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements.Experimental;

public abstract class Intent
{
    public Color32 IconColor;
    public int ValueBase;
    public int ValueCurrent;
    protected Enemy _enemy;

    public Intent(Enemy enemy, int value)
    {
        _enemy = enemy;
        ValueBase = value;
    }

    public abstract string GetValueText();
    public abstract string GetAbbreviationText();

    public abstract void Execute();
    public abstract void Calculate();
}