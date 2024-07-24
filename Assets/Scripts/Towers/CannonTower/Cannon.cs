using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CannonBasicAttack)), RequireComponent(typeof(CannonAbility))]
public class Cannon : Tower
{
    private CannonBasicAttack _cannonBasicAttack;
    private CannonAbility _cannonAbility;

    void Awake()
    {
        _cannonBasicAttack = GetComponent<CannonBasicAttack>();
        _cannonAbility = GetComponent<CannonAbility>();
    }

    void Start()
    {
        _cannonBasicAttack = GetComponent<CannonBasicAttack>();
        _cannonAbility = GetComponent<CannonAbility>();
        BasicAttack = _cannonBasicAttack;
        Ability = _cannonAbility;
    }

    public override string GetTooltipText()
    {
        return $"{Name}\nVersatile damage dealer w/ AoE and single target options.";
    }
}