using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Archer))]
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
        Range = _range;
    }

    public void Activate()
    {
        Enemy target = null;
        foreach (Enemy enemy in EnemyManager.s_Instance.Enemies)
        {
            if (Vector3.Distance(Archer.transform.position, enemy.transform.position) <= _range)
            {
                if (target == null)
                {
                    target = enemy;
                }
                else
                {
                    if (enemy.DistanceTraveled > target.DistanceTraveled)
                    {
                        target = enemy;
                    }
                }
            }
        }
        target.OnImpact(BaseDamage + Archer.Energy);
    }

    public string GetTooltipText()
    {
        return $"{Name}: Deal {BaseDamage} + ({Archer.Energy}) damage to the most travleled enemy within {_range} range.";
    }
}