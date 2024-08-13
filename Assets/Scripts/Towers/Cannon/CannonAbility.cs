using UnityEngine;

public class CannonAbility : MonoBehaviour, IAbility
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
        Name = "Cannon Ability";
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
        bomb.transform.localScale = new Vector2(2f, 2f);
        bomb.Initialize(Damage, ProjectileSpeed, _cannon);
        bomb.SetExplosionRadius(_cannon.ExplosionRadius * 3);
        bomb.Launch(target);
        _abilitySound.Play();
        
        InternalClock = 0;
    }

    public string GetTooltipText()
    {
        return "Cannon Ability";
    }

    public bool IsReloaded()
    {
        return InternalClock >= Cooldown && _cannon.RangeIndicator.HasEnemyInRange;
    }
}