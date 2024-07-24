using UnityEngine;
using System.Collections.Generic;

public class CannonAbility : MonoBehaviour, IAbility
{
    private Cannon Cannon;
    [SerializeField] private string _name;
    public string Name { get; set; }
    [SerializeField] private int _energyCost;
    public int EnergyCost { get; set; }
    [SerializeField] private int _range;
    public float Range { get; set; }
    [SerializeField] private int BaseDamage;
    [SerializeField] private int SplashDamage;
    [SerializeField] private float SplashRange;

    void Awake()
    {
        Cannon = GetComponent<Cannon>();
    }

    void OnEnable()
    {
        Name = _name;
        EnergyCost = _energyCost;
        Range = _range + 0.5f;
    }

    public void Activate()
    {
        List<Enemy> enemies = TargetCalculator.GetEnemiesInRange(transform, Range);
        enemies = TargetCalculator.GetMostTraveled(enemies);
        enemies[0].OnImpact(BaseDamage);

        foreach (Enemy enemy in EnemyManager.s_Instance.Enemies)
        {
            if(enemy == enemies[0]) { continue; }
            if (Vector2.Distance(enemies[0].transform.position, enemy.transform.position) <= SplashRange)
            {
                enemy.OnImpact(SplashDamage);
            }
        }
    }

    public string GetTooltipText()
    {
        return $"{Name}: Deal {BaseDamage} damage to the most traveled enemy within {_range} range and {SplashDamage} damage to all enemies within {SplashRange} range of the target.";
    }
}