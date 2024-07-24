using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Cannon))]
public class CannonBasicAttack : MonoBehaviour, IAbility
{
    private Cannon Cannon;
    [SerializeField] private string _name;
    public string Name { get; set; }
    [SerializeField] private int _energyCost;
    public int EnergyCost { get; set; }
    [SerializeField] private int _range;
    public float Range { get; set; }
    [SerializeField] private int BaseDamage;

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
    }

    public string GetTooltipText()
    {
        return $"{Name}: Deal {BaseDamage} damage to the most travleled enemy within {_range} range.";
    }
}