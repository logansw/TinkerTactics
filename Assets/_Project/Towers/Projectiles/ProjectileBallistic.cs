using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileBallistic : ProjectileBase
{
    private float _projectileSpeed;
    private float _maxTravelDistance;

    public void Initialize(Tower source, ProjectileEffectTracker projectileEffectTracker, float damage, float projectileSpeed, Vector2 direction, float maxTravelDistance)
    {
        SourceTower = source;
        ProjectileEffectTracker = projectileEffectTracker;
        Damage = damage;
        ProjectileEffectTracker.ParentProjectile = this;

        _projectileSpeed = projectileSpeed;
        _maxTravelDistance = maxTravelDistance;

        RotateTowards(direction);
        projectileEffectTracker.AddEffect<PierceProjectileEffect>(0);

        Destroy(gameObject, 3f);
    }

    private void RotateTowards(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    protected override void Move()
    {
        transform.Translate(Vector2.right * Time.deltaTime * _projectileSpeed);
        float distanceFromTower = Vector2.Distance(transform.position, SourceTower.transform.position);
        if (distanceFromTower > _maxTravelDistance)
        {
            Destroy(gameObject);
        }
    }
}