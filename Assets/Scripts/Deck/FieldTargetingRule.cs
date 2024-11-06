using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldTargetingRule : TargetingRules
{
    public FieldTargetingRule()
    {

    }

    protected override bool ValidTargetHelper(Vector3 targetPosition)
    {
        return true;
    }
}
