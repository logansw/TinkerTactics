using UnityEngine;

public class ProjectileExplosive : Projectile
{
    private bool _exploded;
    [HideInInspector] public float ExplosionRadius;
    [SerializeField] private GameObject _explosionPrefab;

    public void SetExplosionRadius(float explosionRadius)
    {
        ExplosionRadius = explosionRadius;
    }

    public override void OnImpact(Enemy enemy)
    {
        if (_exploded) { return; }
        _exploded = true;
        GameObject explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        explosion.transform.localScale = new Vector3(ExplosionRadius, ExplosionRadius, 1);
        Destroy(explosion, 1f);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius / 2f);
        foreach (Collider2D collider in colliders)
        {
            Enemy nearbyEnemy = collider.GetComponent<Enemy>();
            if (nearbyEnemy != null )
            {
                nearbyEnemy.ReceiveProjectile(this);
            }
        }
        CleanUp();
    }
}