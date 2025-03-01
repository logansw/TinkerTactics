using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using UnityEngine;

public class EffectProcessor : MonoBehaviour
{
    private List<Effect> _effects;
    private List<Effect> Effects
    {
        get
        {
            if (_effects == null)
            {
                _effects = new List<Effect>();
            }
            return _effects;
        }
        set
        {
            _effects = value;
        }
    }
    
    public void AddEffect(Effect effect)
    {
        Effects.Add(effect);
        if (effect is IStatChangerEffect statChangerEffect)
        {
            statChangerEffect.ChangeStat();
        }
    }
    
    public void RemoveEffect(Effect effect)
    {
        Effects.Remove(effect);
        if (effect is IStatChangerEffect statChangerEffect)
        {
            statChangerEffect.UndoChange();
        }
    }

    public List<Effect> GetEffects()
    {
        return Effects;
    }
    
    public List<T> Query<T>() where T : Effect
    {
        List<T> relevantEffects = new List<T>();

        foreach (Effect effect in Effects)
        {
            if (effect is T relevantEffect)
            {
                relevantEffects.Add(relevantEffect);
            }
        }

        return relevantEffects;
    }
}