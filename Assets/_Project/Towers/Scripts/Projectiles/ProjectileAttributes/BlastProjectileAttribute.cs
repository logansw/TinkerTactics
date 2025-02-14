using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastProjectileAttribute : ProjectileAttribute
{
    private bool _explodedThisFrame;

    void Update()
    {
        // Refresh every frame so that secondary explosions can occur
        _explodedThisFrame = false;
    }

    public override void OnProjectileHitPreImpact(Enemy hit)
    {
        if (_explodedThisFrame) { return; }
        
        _explodedThisFrame = true;
        int explosionRadius = Stacks;
        GenerateExplosion(explosionRadius);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius / 2f);
        foreach (Collider2D collider in colliders)
        {
            Enemy nearbyEnemy = collider.GetComponent<Enemy>();
            if (nearbyEnemy != null && nearbyEnemy != hit)
            {
                nearbyEnemy.ReceiveDamage(ParentProjectile.Damage, ParentProjectile, ParentProjectile.SourceTower);
            }
        }
    }

    public override void OnProjectileHitPostImpact(Enemy hit)
    {
        // Do Nothing
    }

    public override void OnEnemyDamaged(Enemy hit)
    {
        // Do Nothing
    }

    public override void OnProjectileLaunched(Enemy target)
    {
        // Do Nothing
    }

    private void GenerateExplosion(int explosionRadius)
    {
        GameObject explosion = Instantiate(Resources.Load<GameObject>("Explosion"), transform.position, Quaternion.identity);
        explosion.transform.localScale = new Vector3(explosionRadius, explosionRadius, 1);
        Destroy(explosion, 1f);
    }
}