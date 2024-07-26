using UnityEngine;
using System.Collections.Generic;

public class HunterBasicAttack : MonoBehaviour, IAbility
{
    private Hunter Hunter;
    [SerializeField] private string _name;
    public string Name { get; set; }
    [SerializeField] private int _energyCost;
    public int EnergyCost { get; set; }
    [SerializeField] private int _range;
    public float Range { get; set; }

    void Awake()
    {
        Hunter = GetComponent<Hunter>();
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
        if (enemies[0].Health.CurrentHealth <= Hunter.ExecuteDamage)
        {
            enemies[0].OnImpact(enemies[0].Health.CurrentHealth);
            Hunter.ExecuteDamage++;
        }
    }

    public string GetTooltipText()
    {
        return $"{Name}: Targets the most traveled enemy within {_range} range. Executes enemies with {Hunter.ExecuteDamage} or less health.";
    }
}