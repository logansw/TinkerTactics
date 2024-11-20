using System;
using System.Collections;
using System.Collections.Generic;
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

    public void AddEffect<T>(float duration, int stacks) where T : Effect
    {
        if (HasEffect<T>(out T effect))
        {
            effect.AddStacks(stacks);
        }
        else
        {
            T newEffect = gameObject.AddComponent<T>();
            if (newEffect.CheckRules())
            {
                newEffect.Initialize(duration, stacks, this);
                EffectsApplied.Add(newEffect);
            }
            else
            {
                Destroy(newEffect);
            }
        }
        _effectRenderer.RenderEffects();
        e_OnEffectsChanged?.Invoke();
    }

    public void RemoveEffect(Effect effect)
    {
        EffectsApplied.Remove(effect);
        _effectRenderer.RenderEffects();
        e_OnEffectsChanged?.Invoke();
    }

    public void RemoveStacks(Effect effect, int count)
    {
        effect.RemoveStacks(count);
        _effectRenderer.RenderEffects();
        e_OnEffectsChanged?.Invoke();
    }

    public void ClearEffects()
    {
        foreach (Effect effect in EffectsApplied)
        {
            Destroy(effect);
        }
        EffectsApplied.Clear();
        _effectRenderer.RenderEffects();
        e_OnEffectsChanged?.Invoke();
    }

    public void UpdateRenderer()
    {
        _effectRenderer.RenderEffects();
    }
}