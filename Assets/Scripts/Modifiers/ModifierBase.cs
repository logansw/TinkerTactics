using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ModifierBase : MonoBehaviour
{
    protected Tower _tower;
    public abstract bool CanAddModifier(Tower recipient);
    /// <summary>
    /// Subscribe to relevant TowerEvents here.
    /// </summary>
    /// <param name="recipient"></param>
    public abstract void Initialize(Tower recipient);
    public abstract string GetDescription();
    protected virtual void OnTinkerEquipped(TinkerEquippedEvent tinkerEquippedEvent) { Debug.LogError("Not Implemented");}
}