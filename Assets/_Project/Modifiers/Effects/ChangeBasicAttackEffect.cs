using UnityEngine;

public class ChangeBasicAttackEffect<T> : IBasicAttackChangerEffect where T : ProjectileAttribute
{
    private int _amount;

    public ChangeBasicAttackEffect(int amount)
    {
        _amount = amount;
    }

    public void ChangeBasicAttack(ProjectileBase projectile)
    {
        projectile.ProjectileAttributeTracker.AddAttribute<T>(_amount);
    }
}
