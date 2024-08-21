using UnityEngine;

public class ArcherAttack : MonoBehaviour, IAbility
{
    [SerializeField] private Archer _archer;
    public string Name { get; set; }
    public float Cooldown { get; set; }
    public float CooldownBase;
    public float InternalClock { get; set; }
    public float Damage;
    [SerializeField] private ProjectileArrow _projectileArrow;
    public float ProjectileSpeed;
    [SerializeField] private AudioSource _abilitySound;

    public void Initialize()
    {
        Name = "Archer Attack";
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
        _abilitySound.Play();

        InternalClock = 0;
    }

    public string GetTooltipText()
    {
        return "Archer Attack";
    }

    public bool IsReloaded()
    {
        return InternalClock >= Cooldown && _archer.RangeIndicator.HasEnemyInRange;
    }
}