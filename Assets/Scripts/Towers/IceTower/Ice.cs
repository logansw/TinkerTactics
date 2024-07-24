using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IceBasicAttack)), RequireComponent(typeof(IceAbility))]
public class Ice : Tower
{
    private IceBasicAttack _iceBasicAttack;
    private IceAbility _iceAbility;
    public int FrostbiteDamage;

    void Awake()
    {
        _iceBasicAttack = GetComponent<IceBasicAttack>();
        _iceAbility = GetComponent<IceAbility>();
    }

    void Start()
    {
        _iceBasicAttack = GetComponent<IceBasicAttack>();
        _iceAbility = GetComponent<IceAbility>();
        BasicAttack = _iceBasicAttack;
        Ability = _iceAbility;
    }

    public override string GetTooltipText()
    {
        return $"{Name}\nApplies Chill to enemies.\n1 Chill slows by 50%. 2 Chill stuns. 3+ Chill deals {FrostbiteDamage} damage.";
    }

    public void ApplyChill(Enemy enemy)
    {
        if (!enemy.EffectTracker.HasEffect<EffectChill>(out EffectChill effectChill))
        {
            enemy.EffectTracker.AddEffect<EffectChill>(1);
        }
        else
        {
            effectChill.Stacks++;
            if (effectChill.Stacks == 2)
            {
                enemy.EffectTracker.AddEffect<EffectStun>(1);
            }
            else if (effectChill.Stacks >= 3)
            {
                enemy.OnImpact(FrostbiteDamage);
            }
        }
    }
}