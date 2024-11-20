using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component attached to projectiles. Handles movement towards target and effect application on impact.
/// </summary>
public abstract class Projectile : MonoBehaviour
{
    protected Tower _source;
    [HideInInspector] public float Damage;
    [HideInInspector] public float ProjectileSpeed;
    protected Enemy _target;
    public delegate void OnImpactDelegate(Enemy hit);
    public OnImpactDelegate e_OnImpact;
    protected Vector3 _targetPosition;
    public Action e_OnDestroyed;
    [SerializeField] private SpriteRenderer _renderer;
    private bool _arrived;
    private Collider2D _collider;

    public virtual void Initialize(float damage, float projectileSpeed, Tower source) 
    {
        Damage = damage;
        ProjectileSpeed = projectileSpeed;
        _source = source;
        _collider = GetComponent<Collider2D>();
    }

    public virtual void Launch(Enemy target)
    {
        _target = target;
        Destroy(gameObject, 2f);
    }

    protected virtual void Update()
    {
        if (_arrived) { return; }
        if (_target != null)
        {
            _targetPosition = _target.gameObject.transform.position;
        }
        Vector2 differenceVector = _targetPosition - transform.position;
        float deltaPosition = Time.deltaTime * ProjectileSpeed;
        
        if (differenceVector.magnitude < deltaPosition)
        {
            transform.position = _targetPosition;
            return;
        }
        Vector2 direction = differenceVector.normalized;
        transform.Translate(direction * deltaPosition);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            OnImpact(enemy);
        }
    }

    public virtual void OnImpact(Enemy recipient)
    {
        e_OnImpact?.Invoke(recipient);
        if (recipient.EffectTracker.HasEffect<EffectVulnerable>(out EffectVulnerable effectVulnerable))
        {
            Damage *= effectVulnerable.GetDamageMultiplier();
        }
        recipient.Health.TakeDamage(Damage);
        _renderer.enabled = false;
        _collider.enabled = false;
        Destroy(gameObject, 1f);
    }

    protected virtual void OnKill(Enemy killedEnemy)
    {
        // No implementation by default.
    }
}
