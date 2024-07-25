using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

[RequireComponent(typeof(EffectRenderer))]
public class EffectTracker : MonoBehaviour
{
    private Enemy _enemy;
    public List<Effect> EffectsApplied { get; private set; }
    private EffectRenderer _effectRenderer;
    public Action e_OnEffectsChanged;

    void Awake()
    {
        EffectsApplied = new List<Effect>();
        _effectRenderer = GetComponent<EffectRenderer>();
    }

    public bool HasEffect<T>(out T effect) where T : Effect
    {
        foreach (Effect effectCandidate in EffectsApplied)
        {
            if (effectCandidate is T)
            {
                effect = (T)effectCandidate;
                return true;
            }
        }
        effect = null;
        return false;
    }

    public void AddEffect<T>(int duration) where T : Effect
    {
        T effect = gameObject.AddComponent<T>();
        effect.Initialize(duration);
        EffectsApplied.Add(effect);
        _effectRenderer.RenderEffects();
        e_OnEffectsChanged?.Invoke();
    }

    public void RemoveEffect(Effect effect)
    {
        EffectsApplied.Remove(effect);
        _effectRenderer.RenderEffects();
        e_OnEffectsChanged?.Invoke();
    }
}