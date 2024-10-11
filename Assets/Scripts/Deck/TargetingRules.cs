using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public abstract class TargetingRules
{
    public abstract bool ValidTarget(Vector3 targetPosition);
}
