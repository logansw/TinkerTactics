using UnityEngine;

public class CannonAttack : MonoBehaviour, IAbility
{
    [SerializeField] private Cannon _cannon;
    [SerializeField] private CannonLauncher _launcher;
    public string Name { get; set; }
    public float Range { get; set; }
    public float RangeBase;
    public float Sweep { get; set; }
    public float SweepBase;
    public float Cooldown { get; set; }
    public float CooldownBase;
    public float InternalClock { get; set; }
    public float Damage;

    public void Initialize()
    {
        Name = "Cannon Attack";
        Range = RangeBase;
        Sweep = SweepBase;
        Cooldown = CooldownBase;
    }

    void Update()
    {
        Range = RangeBase;
        Sweep = SweepBase;
        Cooldown = CooldownBase;
    }

    public void Activate()
    {
        _launcher.LaunchProjectile(_cannon.RangeIndicator.GetEnemiesInRange()[0]);
        InternalClock = 0;
    }

    public string GetTooltipText()
    {
        return "Cannon Attack";
    }

    public bool IsReloaded()
    {
        return InternalClock >= Cooldown && _cannon.RangeIndicator.HasEnemyInRange;
    }
}