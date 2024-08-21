using UnityEngine;

public class ProjectileExplosive : Projectile
{
    [HideInInspector] public float ExplosionRadius;
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private AudioSource _impactSound;

    public void SetExplosionRadius(float explosionRadius)
    {
        ExplosionRadius = explosionRadius;
    }

    public override void OnImpact()
    {
        base.OnImpact();
        GameObject explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        explosion.transform.localScale = new Vector3(ExplosionRadius, ExplosionRadius, 1);
        Destroy(explosion, 1f);
        _impactSound.Play();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius / 2f);
        foreach (Collider2D collider in colliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.OnImpact(Damage);
            }
        }
    }
}