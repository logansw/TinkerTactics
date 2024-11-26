using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Archer : Tower
{
    public override void Unlock()
    {
        base.Unlock();
        Player.s_Instance.Energy += 1;
    }
}