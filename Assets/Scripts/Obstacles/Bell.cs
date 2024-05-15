using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bell : Obstacle
{
    [SerializeField] private BellProjectile _bellProjectilePrefab;
    public float Damage;

    public override void Attack()
    {
        BellProjectile bellProjectile = Instantiate(_bellProjectilePrefab, transform.position, Quaternion.identity);
        bellProjectile.Initialize(Damage);
    }

    public override bool CanAttack()
    {
        return false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BellProjectile>() != null)
        {
            return;
        }

        Attack();
    }
}