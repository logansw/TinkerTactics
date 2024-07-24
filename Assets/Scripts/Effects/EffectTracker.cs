using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class EffectTracker : MonoBehaviour
{
    private Enemy _enemy;
    private List<Effect> _effectsApplied;

    void Awake()
    {
        _effectsApplied = new List<Effect>();
    }

    public bool HasEffect<T>(out T effect) where T : Effect
    {
        foreach (Effect effectCandidate in _effectsApplied)
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
        _effectsApplied.Add(effect);
    }

    public void RemoveEffect(Effect effect)
    {
        _effectsApplied.Remove(effect);
    }
}