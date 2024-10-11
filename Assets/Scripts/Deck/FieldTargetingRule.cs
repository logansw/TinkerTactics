using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldTargetingRule : TargetingRules
{
    public FieldTargetingRule()
    {

    }

    public override bool ValidTarget(Vector3 targetPosition)
    {
        return true;
    }
}
