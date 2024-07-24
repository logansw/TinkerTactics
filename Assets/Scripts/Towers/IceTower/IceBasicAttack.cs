using UnityEngine;
using System.Collections.Generic;

public class IceBasicAttack : MonoBehaviour, IAbility
{
    private Ice Ice;
    [SerializeField] private string _name;
    public string Name { get; set; }
    [SerializeField] private int _energyCost;
    public int EnergyCost { get; set; }
    [SerializeField] private int _range;
    public float Range { get; set; }

    void Awake()
    {
        Ice = GetComponent<Ice>();
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
        enemies = TargetCalculator.GetFastest(enemies);
        Ice.ApplyChill(enemies[0]);
    }

    public string GetTooltipText()
    {
        return $"{Name}: Applies 1 Chill to the fastest enemy within {_range} range.";
    }
}