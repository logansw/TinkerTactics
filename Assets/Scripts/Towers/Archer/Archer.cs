using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Archer : Tower
{
    protected override void OnEnable()
    {
        // Undo the default behavior of locking on playing state entered.
        base.OnEnable();
        IdleState.e_OnIdleStateEnter += Unlock;
    }
}