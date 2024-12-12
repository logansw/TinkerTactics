using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierProcessor : MonoBehaviour
{
    private List<ModifierBase> _modifiers;
    private List<ModifierBase> Modifiers
    {
        get
        {
            if (_modifiers == null)
            {
                _modifiers = new List<ModifierBase>();
            }
            return _modifiers;
        }
        set
        {
            _modifiers = value;
        }
    }
    public int TinkerCount;

    public void AddModifier(ModifierBase modifier, Tower recipient)
    {
        Modifiers.Add(modifier);
        TinkerCount++;
        if (modifier is TinkerBase)
        {
            ToastManager.s_Instance.AddToast($"{recipient.Name} has {TinkerCount}/{recipient.TinkerLimit} tinkers equipped.");
        }
    }
}