using UnityEditor;

public class SelectiveBonusDamageEffect<T> : IOnHitEffect where T : Enemy
{
    private float _bonusDamageAdditive;
    private float _bonusDamageMultiplicative;

    public SelectiveBonusDamageEffect(float bonusDamageAdditive = 0.0f, float bonusDamageMultiplicative = 1.0f)
    {
        _bonusDamageAdditive = bonusDamageAdditive;
        _bonusDamageMultiplicative = bonusDamageMultiplicative;
    }

    public void OnHit(PreEnemyImpactEvent preEnemyImpactEvent)
    {
        if (preEnemyImpactEvent.Enemy is T enemyType)
        {
            preEnemyImpactEvent.Enemy.ReceiveDamage(_bonusDamageAdditive);
            preEnemyImpactEvent.Projectile.Damage *= _bonusDamageMultiplicative;
        }
    }
}