using UnityEngine;

public class SniperAbility : MonoBehaviour
{
    [SerializeField] private Archer _archer;
    public string Name { get; set; }
    public float Cooldown { get; set; }
    public float CooldownBase;
    public float InternalClock { get; set; }
    public float Damage;
    [SerializeField] private ProjectilePierce _projectilePierce;
    public float ProjectileSpeed;

    public void Initialize()
    {
        Name = "Piercing Arrow";
        Cooldown = CooldownBase;
    }

    void Update()
    {
        Cooldown = CooldownBase;
    }

    public void Activate()
    {
        Enemy target = _archer.RangeIndicator.GetEnemiesInRange()[0];
        ProjectilePierce piercingArrow = Instantiate(_projectilePierce, _archer.transform.position, Quaternion.identity);
        // Rotate piercingArrow to face target
        Vector2 direction = target.transform.position - _archer.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        piercingArrow.transform.rotation = Quaternion.Euler(0, 0, angle);
        piercingArrow.Initialize(Damage, ProjectileSpeed, _archer);
        piercingArrow.Launch(target);

        InternalClock = 0;
    }

    public string GetTooltipText()
    {
        return "Sniper Abiilty";
    }

    public bool CanActivate()
    {
        return InternalClock >= Cooldown && _archer.RangeIndicator.HasEnemyInRange();
    }
}