using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttributeTracker : MonoBehaviour
{
    public ProjectileBase ParentProjectile;
    private List<ProjectileAttribute> _attributesApplied;
    public List<ProjectileAttribute> AttributesApplied
    {
        get
        {
            if (_attributesApplied == null)
            {
                _attributesApplied = new List<ProjectileAttribute>();
            }
            return _attributesApplied;
        }
        set
        {
            _attributesApplied = value;
        }    
    }
    private EffectProcessor _effectProcessor;

    void Start()
    {
        _effectProcessor = ParentProjectile.SourceTower.EffectProcessor;
    }

    void OnEnable()
    {
        EventBus.Subscribe<PreEnemyImpactEvent>(RaiseOnProjectileHitPreImpact);
        EventBus.Subscribe<EnemyDamagedEvent>(RaiseOnEnemyDamaged);
        EventBus.Subscribe<PostEnemyImpactEvent>(RaiseOnProjectileHitPostImpact);
        EventBus.Subscribe<BasicAttackEvent>(RaiseOnProjectileLaunched);
    }

    void OnDestroy()
    {
        EventBus.Unsubscribe<PreEnemyImpactEvent>(RaiseOnProjectileHitPreImpact);
        EventBus.Unsubscribe<EnemyDamagedEvent>(RaiseOnEnemyDamaged);
        EventBus.Unsubscribe<PostEnemyImpactEvent>(RaiseOnProjectileHitPostImpact);
        EventBus.Unsubscribe<BasicAttackEvent>(RaiseOnProjectileLaunched);
    }


    public bool HasAttribute<T>(out T attribute) where T : ProjectileAttribute
    {
        foreach (ProjectileAttribute attributeCandidate in AttributesApplied)
        {
            if (attributeCandidate is T)
            {
                attribute = (T)attributeCandidate;
                return true;
            }
        }
        attribute = null;
        return false;
    }

    public void AddAttribute<T>(int stacks) where T : ProjectileAttribute
    {
        if (HasAttribute<T>(out T attribute))
        {
            attribute.Stacks += stacks;
        }
        else
        {
            T newProjectileAttribute = gameObject.AddComponent<T>();
            newProjectileAttribute.Stacks = stacks;
            AttributesApplied.Add(newProjectileAttribute);
            newProjectileAttribute.ParentProjectile = ParentProjectile;
        }
    }

    public void RemoveStacks<T>(int count) where T : ProjectileAttribute
    {
        for (int i = AttributesApplied.Count - 1; i >= 0; i--)
        {
            ProjectileAttribute attribute = AttributesApplied[i];
            if (attribute is T)
            {
                attribute.Stacks -= count;
                if (attribute.Stacks <= 0)
                {
                    AttributesApplied.Remove(attribute);
                }
                break;
            }
        }
    }

    public void RemoveAttribute<T>() where T : ProjectileAttribute
    {
        if (!HasAttribute<T>(out T attribute)) { return; }
        for (int i = AttributesApplied.Count - 1; i >= 0; i--)
        {
            if (AttributesApplied[i] is T)
            {
                AttributesApplied.RemoveAt(i);
            }
        }
    }

    public void RaiseOnProjectileHitPreImpact(PreEnemyImpactEvent e)
    {
        if (e.Projectile != ParentProjectile) { return; }
        foreach (ProjectileAttribute projectileAttribute in AttributesApplied)
        {
            projectileAttribute.OnProjectileHitPreImpact(e.Enemy);
        }
        List<IOnHitEffect> onHitEffects = _effectProcessor.Query<IOnHitEffect>();
        foreach (IOnHitEffect onHitEffect in onHitEffects)
        {
            onHitEffect.OnHit(e);
        }
    }

    public void RaiseOnProjectileHitPostImpact(PostEnemyImpactEvent e)
    {
        if (e.Projectile != ParentProjectile) { return; }
        foreach (ProjectileAttribute projectileAttribute in AttributesApplied)
        {
            projectileAttribute.OnProjectileHitPostImpact(e.Enemy);
        }
    }

    public void RaiseOnEnemyDamaged(EnemyDamagedEvent e)
    {
        if (e.Projectile != ParentProjectile) { return; }
        foreach (ProjectileAttribute projectileAttribute in AttributesApplied)
        {
            projectileAttribute.OnEnemyDamaged(e.Enemy);
        }
    }

    public void RaiseOnProjectileLaunched(BasicAttackEvent e)
    {
        if (e.Projectile != ParentProjectile) { return; }
        foreach (ProjectileAttribute projectileAttribute in AttributesApplied)
        {
            projectileAttribute.OnProjectileLaunched(e.Target);
        }
    }
}
