using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector] public float Damage;
    [HideInInspector] public float ProjectileSpeed;
    private Enemy _target;

    public void Initialize(float damage, float projectileSpeed) 
    {
        Damage = damage;
        ProjectileSpeed = projectileSpeed;
    }

    public void Launch(Enemy target)
    {
        _target = target;
    }

    void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }
        Transform enemyTransform = _target.gameObject.transform;
        Vector2 differenceVector = enemyTransform.position - transform.position;
        if (differenceVector.magnitude < 0.1f)
        {
            OnImpact();
            return;
        }
        Vector2 direction = differenceVector.normalized;
        transform.Translate(direction * Time.deltaTime * ProjectileSpeed);
    }

    public void OnImpact()
    {
        _target.ReceiveDamage(Damage);
        Destroy(gameObject);
    }
}
