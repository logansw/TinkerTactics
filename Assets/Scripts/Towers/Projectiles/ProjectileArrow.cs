using UnityEngine;

public class ProjectileArrow : Projectile
{
    public override void OnImpact()
    {
        base.OnImpact();
        _target.OnImpact(Damage);
    }
}