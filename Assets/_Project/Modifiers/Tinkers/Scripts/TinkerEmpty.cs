using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinkerEmpty : TinkerBase
{
    public override string GetDescription()
    {
        return $"An empty Tinker Slot! Attach a Tinker to this tower to power it up!";
    }

    public override void Initialize(Tower recipient)
    {
        _tower = recipient;   
    }
}
