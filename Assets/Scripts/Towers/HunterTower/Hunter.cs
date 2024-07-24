using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HunterBasicAttack)), RequireComponent(typeof(HunterAbility))]
public class Hunter : Tower
{
    private HunterBasicAttack _hunterBasicAttack;
    private HunterAbility _hunterAbility;
    public int ExecuteDamage;

    void Awake()
    {
        _hunterBasicAttack = GetComponent<HunterBasicAttack>();
        _hunterAbility = GetComponent<HunterAbility>();
    }

    void Start()
    {
        _hunterBasicAttack = GetComponent<HunterBasicAttack>();
        _hunterAbility = GetComponent<HunterAbility>();
        BasicAttack = _hunterBasicAttack;
        Ability = _hunterAbility;
    }

    public override string GetTooltipText()
    {
        return $"{Name}\nLong-range executor. Kills give +1 Execute Damage.\nExecute Damage: {ExecuteDamage}";
    }
}