using UnityEngine;

public class CannonAttack : MonoBehaviour, IAbility
{
    [SerializeField] private Cannon _cannon;
    public string Name { get; set; }
    public float Cooldown { get; set; }
    public float CooldownBase;
    public float InternalClock { get; set; }
    public float Damage;
    public float ProjectileSpeed;
    [SerializeField] private ProjectileExplosive _projectileExplosive;
    [SerializeField] private AudioSource _abilitySound;

    public void Initialize()
    {
        Name = "Cannon Attack";
        Cooldown = CooldownBase;
    }

    void Update()
    {
        Cooldown = CooldownBase;
    }

    public void Activate()
    {
        Enemy target = _cannon.RangeIndicator.GetEnemiesInRange()[0];
        ProjectileExplosive bomb = Instantiate(_projectileExplosive, _cannon.transform.position, Quaternion.identity);
        bomb.Initialize(Damage, ProjectileSpeed, _cannon);
        bomb.SetExplosionRadius(_cannon.ExplosionRadius);
        bomb.Launch(target);
        _abilitySound.Play();
        
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