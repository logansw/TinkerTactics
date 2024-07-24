using UnityEngine;
using System.Collections.Generic;

public class HunterAbility : MonoBehaviour, IAbility
{
    private Hunter Hunter;
    [SerializeField] private string _name;
    public string Name { get; set; }
    [SerializeField] private int _energyCost;
    public int EnergyCost { get; set; }
    [SerializeField] private int _range;
    public float Range { get; set; }
    [SerializeField] private int BaseDamage;

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
        for (int i = 0; i < 2; i++)
        {
            List<Enemy> enemies = TargetCalculator.GetEnemiesInRange(transform, Range);
            enemies = TargetCalculator.GetLeastHealth(enemies);
            enemies[0].OnImpact(BaseDamage);
            if (enemies[0].Health.CurrentHealth <= Hunter.ExecuteDamage)
            {
                enemies[0].OnImpact(enemies[0].Health.CurrentHealth);
                Hunter.ExecuteDamage++;
            }
        }
    }

    public string GetTooltipText()
    {
        return $"{Name}: Activates twice. Targets the lowest health enemies within {_range} range. Deals {BaseDamage} damage and executes them below {Hunter.ExecuteDamage} health.";
    }
}