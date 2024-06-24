using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

public class DamageData
{
    public Enemy ImpactReceiver;
    public float DamageDealt;

    public DamageData(Enemy impactReceiver, float damageDealt)
    {
        ImpactReceiver = impactReceiver;
        DamageDealt = damageDealt;
    }
}
