using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public interface IAbility
{
    public string Name { get; set; }
    public int EnergyCost { get; set; }
    public float Range { get; set; }

    public abstract void Activate();
    public abstract string GetTooltipText();
}