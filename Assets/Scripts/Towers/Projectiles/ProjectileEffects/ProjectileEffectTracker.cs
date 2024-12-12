using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEffectTracker : MonoBehaviour
{
    public Projectile ParentProjectile;
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

    void OnEnable()
    {
        EventBus.Subscribe<PreEnemyImpactEvent>(RaiseOnProjectileHitPreImpact);
        EventBus.Subscribe<EnemyDamagedEvent>(RaiseOnEnemyDamaged);
        EventBus.Subscribe<PostEnemyImpactEvent>(RaiseOnProjectileHitPostImpact);
    }

    void OnDestroy()
    {
        EventBus.Unsubscribe<PreEnemyImpactEvent>(RaiseOnProjectileHitPreImpact);
        EventBus.Unsubscribe<EnemyDamagedEvent>(RaiseOnEnemyDamaged);
        EventBus.Unsubscribe<PostEnemyImpactEvent>(RaiseOnProjectileHitPostImpact);
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
            newProjectileEffect.ParentProjectile = ParentProjectile;
        }
    }

    public void RaiseOnProjectileHitPreImpact(PreEnemyImpactEvent e)
    {
        if (e.Projectile != ParentProjectile) { return; }
        foreach (ProjectileEffect projectileEffect in EffectsApplied)
        {
            projectileEffect.OnProjectileHitPreImpact(e.Enemy);
        }
    }

    public void RaiseOnProjectileHitPostImpact(PostEnemyImpactEvent e)
    {
        if (e.Projectile != ParentProjectile) { return; }
        foreach (ProjectileEffect projectileEffect in EffectsApplied)
        {
            projectileEffect.OnProjectileHitPostImpact(e.Enemy);
        }
    }

    public void RaiseOnEnemyDamaged(EnemyDamagedEvent e)
    {
        if (e.Projectile != ParentProjectile) { return; }
        foreach (ProjectileEffect projectileEffect in EffectsApplied)
        {
            projectileEffect.OnEnemyDamaged(e.Enemy);
        }
    }
}
