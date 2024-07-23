using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ArcherBasicAttack)), RequireComponent(typeof(ArcherAbility))]
public class Archer : Tower
{
    private ArcherBasicAttack _archerBasicAttack;
    private ArcherAbility _archerAbility;

    void Awake()
    {
        _archerBasicAttack = GetComponent<ArcherBasicAttack>();
        _archerAbility = GetComponent<ArcherAbility>();
    }

    void Start()
    {
        _archerBasicAttack = GetComponent<ArcherBasicAttack>();
        _archerAbility = GetComponent<ArcherAbility>();
        BasicAttack = _archerBasicAttack;
        Ability = _archerAbility;
    }

    public override string GetTooltipText()
    {
        return $"{Name}\nSingle-target tower that deals +1 damage per remaining Energy.";
    }
}