using System.Collections;
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
    public int ArrowCount;
    [SerializeField] private AudioSource _abilitySound;
    [SerializeField] private AudioSource _powerupSound;

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
        _powerupSound.Play();
        StartCoroutine(Volley());
        ArrowCount += 3;
        InternalClock = 0;
    }

    private IEnumerator Volley()
    {
        for (int i = 0; i < ArrowCount; i++)
        {
            Enemy target = _archer.RangeIndicator.GetEnemiesInRange()[Random.Range(0, _archer.RangeIndicator.GetEnemiesInRange().Count)];
            ProjectileArrow arrow = Instantiate(_projectileArrow, _archer.transform.position, Quaternion.identity);
            arrow.Initialize(Damage, ProjectileSpeed, _archer);
            arrow.Launch(target);
            _abilitySound.Play();
            yield return new WaitForSeconds(0.1f);
        }
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