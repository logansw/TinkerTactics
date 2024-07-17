using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectile : Projectile
{
    [SerializeField] private SpriteRenderer explosionSpriteRenderer;
    public override void OnImpact()
    {
        base.OnImpact();
        AnimateExplosion();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_target.transform.position, 2f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.GetComponent<Enemy>() != null)
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.OnImpact(Damage);
                }
            }
        }
    }

    private void AnimateExplosion()
    {
        SpriteRenderer explosion = Instantiate(explosionSpriteRenderer, transform.position, Quaternion.identity);
        explosion.transform.localScale = new Vector3(2, 2, 2);
        Destroy(explosion.gameObject, 0.5f);
    }
}