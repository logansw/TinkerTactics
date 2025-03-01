public class KillReloadEffect : IOnEnemyDeathEffect
{
    private int _reloadAmount;

    public KillReloadEffect(int reloadAmount)
    {
        _reloadAmount = reloadAmount;
    }

    public void OnEnemyDeath(EnemyDeathEvent enemyDeathEvent)
    {
        enemyDeathEvent.Tower.Ammo.Current += _reloadAmount;
    }
}