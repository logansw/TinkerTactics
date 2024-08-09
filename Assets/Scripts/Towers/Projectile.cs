using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component attached to projectiles. Handles movement towards target and effect application on impact.
/// </summary>
public abstract class Projectile : MonoBehaviour
{
    private Tower _source;
    [HideInInspector] public float Damage;
    [HideInInspector] public float ProjectileSpeed;
    protected Enemy _target;
    public delegate void OnImpactDelegate(Enemy hit);
    public OnImpactDelegate e_OnImpact;
    private Vector3 _targetPosition;
    public Action e_OnDestroyed;

    public virtual void Initialize(float damage, float projectileSpeed, Tower source) 
    {
        Damage = damage;
        ProjectileSpeed = projectileSpeed;
        _source = source;
    }

    public void Launch(Enemy target)
    {
        _target = target;
    }

    void Update()
    {
        if (_target != null)
        {
            _targetPosition = _target.gameObject.transform.position;
        }
        Vector2 differenceVector = _targetPosition - transform.position;
        if (differenceVector.magnitude < 0.1f)
        {
            OnImpact();
            return;
        }
        Vector2 direction = differenceVector.normalized;
        transform.Translate(direction * Time.deltaTime * ProjectileSpeed);
    }

    public virtual void OnImpact()
    {
        if (_target != null)
        {
            e_OnImpact?.Invoke(_target);
        }
        gameObject.SetActive(false);
        Destroy(gameObject, 1f);
    }

    protected virtual void OnKill(Enemy killedEnemy)
    {
        // No implementation by default.
    }
}
