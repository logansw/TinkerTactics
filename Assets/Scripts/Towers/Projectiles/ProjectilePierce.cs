using UnityEngine;

public class ProjectilePierce : Projectile
{
    private Vector2 _direction;
    public int Pierce;

    public override void Initialize(float damage, float projectileSpeed, Tower source)
    {
        base.Initialize(damage, projectileSpeed, source);
        Pierce = 2;
    }

    public override void OnImpact(Enemy enemy)
    {
        enemy.OnImpact(Damage);
        Pierce--;
        if (Pierce == 0)
        {
            Destroy(gameObject);
        }
    }

    public override void Launch(Enemy target)
    {
        _direction = (target.transform.position - transform.position).normalized;
        // Rotate to match direction
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    protected override void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * ProjectileSpeed);
        float distanceFromTower = Vector2.Distance(transform.position, _source.transform.position);
        if (distanceFromTower > _source.Range.Current)
        {
            Destroy(gameObject);
        }
    }
}