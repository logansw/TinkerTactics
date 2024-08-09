using UnityEngine;

public class ArcherAbility : MonoBehaviour, IAbility
{
    [SerializeField] private Archer _archer;
    public string Name { get; set; }
    public float Cooldown { get; set; }
    public float CooldownBase;
    public float InternalClock { get; set; }
    public float Damage;
    [SerializeField] private ProjectileArrow _projectileArrow;
    public float ProjectileSpeed;

    public void Initialize()
    {
        Name = "Arrow Storm";
        Cooldown = CooldownBase;
    }

    void Update()
    {
        Cooldown = CooldownBase;
    }

    public void Activate()
    {
        Enemy target = _archer.RangeIndicator.GetEnemiesInRange()[0];
        ProjectileArrow arrow = Instantiate(_projectileArrow, _archer.transform.position, Quaternion.identity);
        arrow.Initialize(Damage, ProjectileSpeed, _archer);
        arrow.Launch(target);

        InternalClock = 0;
    }

    public string GetTooltipText()
    {
        return "Archer Abiilty";
    }

    public bool IsReloaded()
    {
        return InternalClock >= Cooldown && _archer.RangeIndicator.HasEnemyInRange;
    }
}