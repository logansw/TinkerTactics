using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ProjectileHoming : ProjectileBase
{
    private float _projectileSpeed;
    private Enemy _target;
    // TODO: Set this up in Initialize?
    private Vector3 _targetPosition;
    // TODO: When to set true?
    private bool _arrived;

    public void Initialize(Tower source, ProjectileEffectTracker projectileEffectTracker, float damage, float projectileSpeed, Enemy target)
    {
        SourceTower = source;
        ProjectileEffectTracker = projectileEffectTracker;
        Damage = damage;
        ProjectileEffectTracker.ParentProjectile = this;

        _projectileSpeed = projectileSpeed;
        _target = target;
    }

    protected override void Move()
    {
        if (_arrived) { return; }
        if (_target != null)
        {
            _targetPosition = _target.transform.position;
        }
        Vector2 differenceVector = _targetPosition - transform.position;
        float deltaPosition = Time.deltaTime * _projectileSpeed;
        
        if (differenceVector.magnitude < deltaPosition)
        {
            transform.position = _targetPosition;
            return;
        }
        Vector2 direction = differenceVector.normalized;
        transform.Translate(direction * deltaPosition);
    }
}