using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    public override void Awake()
    {
        base.Awake();
        Health = new Health(10, 1);
        MovementSpeed = 1;
        Armor = 0;
    }
}
