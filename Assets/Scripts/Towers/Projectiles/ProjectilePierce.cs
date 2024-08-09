using UnityEngine;

public class ProjectilePierce : Projectile
{
    private Vector2 _direction;
    public override void OnImpact()
    {
        _target.OnImpact(Damage);
    }

    public override void Launch(Enemy target)
    {
        _direction = (target.transform.position - transform.position).normalized;
        Destroy(this, 3f);
    }

    protected override void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * ProjectileSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            other.GetComponent<Enemy>().OnImpact(Damage);
        }
    }
}