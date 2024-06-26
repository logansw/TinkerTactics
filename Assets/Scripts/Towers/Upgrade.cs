using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public string Name;
    public string Description;
    public int Cost;
    public delegate void UpgradeEffectHandler();
    public UpgradeEffectHandler UpgradeEffect;
}