using UnityEngine;

public class CannonAttack : MonoBehaviour, IAbility
{
    [SerializeField] private Cannon _cannon;
    public string Name { get; set; }
    public float Range { get; set; }
    public float Sweep { get; set; }
    public float Cooldown { get; set; }
    public float InternalClock { get; set; }
    public float Damage;

    public void Initialize()
    {
        Name = "Cannon Attack";
        Range = 3f;
        Sweep = 180f;
        Cooldown = 1f;
    }

    public void Activate()
    {
        Debug.Log("Pew!");
        _cannon.RangeIndicator.GetEnemiesInRange()[0].OnImpact(Damage);
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