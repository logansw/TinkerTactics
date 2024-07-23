using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public abstract class Ability : MonoBehaviour
{
    public string Name;
    public int EnergyCost;
    public Sprite Icon;

    public abstract void Activate();

    public void Display(Tower tower)
    {

    }
}