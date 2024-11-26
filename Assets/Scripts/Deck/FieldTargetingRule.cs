using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldTargetingRule : TargetingRules
{
    public FieldTargetingRule(CardEffect cardEffect)
    {
        e_OnCardReturned += cardEffect.OnCardReturned;
    }

    protected override bool ValidTargetHelper(Vector3 targetPosition)
    {
        return true;
    }
}
