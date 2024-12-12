using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEffectTracker : MonoBehaviour
{
    private List<ProjectileEffect> _effectsApplied;
    public List<ProjectileEffect> EffectsApplied
    {
        get
        {
            if (_effectsApplied == null)
            {
                _effectsApplied = new List<ProjectileEffect>();
            }
            return _effectsApplied;
        }
        set
        {
            _effectsApplied = value;
        }    
    }

    public bool HasEffect<T>(out T effect) where T : ProjectileEffect
    {
        foreach (ProjectileEffect effectCandidate in EffectsApplied)
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

    public void AddEffect<T>(int stacks) where T : ProjectileEffect
    {
        if (HasEffect<T>(out T effect))
        {
            effect.Stacks += stacks;
        }
        else
        {
            T newProjectileEffect = gameObject.AddComponent<T>();
            newProjectileEffect.Stacks = stacks;
            EffectsApplied.Add(newProjectileEffect);
        }
    }

    public void RaiseOnProjectileHitPreDamage(Enemy hit)
    {
        foreach (ProjectileEffect projectileEffect in EffectsApplied)
        {
            projectileEffect.OnProjectileHitPreDamage(hit);
        }
    }

    public void RaiseOnProjectileHitPostDamage(Enemy hit)
    {
        foreach (ProjectileEffect projectileEffect in EffectsApplied)
        {
            projectileEffect.OnProjectileHitPostDamage(hit);
        }
    }

    public void RaiseOnProjectileLaunched()
    {
        foreach (ProjectileEffect projectileEffect in EffectsApplied)
        {
            projectileEffect.OnProjectileLaunched();
        }
    }
}
