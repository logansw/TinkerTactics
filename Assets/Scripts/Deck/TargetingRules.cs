using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public abstract class TargetingRules
{
    public bool CheckValidTarget(Vector3 targetPosition)
    {
        if (CheckTargetingReturnTray())
        {
            return false;
        }
        return ValidTargetHelper(targetPosition);
    }

    protected abstract bool ValidTargetHelper(Vector3 targetPosition);

    private bool CheckTargetingReturnTray()
    {
        return ReturnTray.s_Instance.IsHovered;
    }
}
