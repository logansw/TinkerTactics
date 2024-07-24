using UnityEngine;
using System.Collections.Generic;

public class ArcherBasicAttack : MonoBehaviour, IAbility
{
    private Archer Archer;
    [SerializeField] private string _name;
    public string Name { get; set; }
    [SerializeField] private int _energyCost;
    public int EnergyCost { get; set; }
    [SerializeField] private int _range;
    public float Range { get; set; }
    [SerializeField] private int BaseDamage;

    void Awake()
    {
        Archer = GetComponent<Archer>();
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
        enemies[0].OnImpact(BaseDamage + Archer.Energy);
    }

    public string GetTooltipText()
    {
        return $"{Name}: Deal {BaseDamage} + ({Archer.Energy}) damage to the most travleled enemy within {_range} range.";
    }
}