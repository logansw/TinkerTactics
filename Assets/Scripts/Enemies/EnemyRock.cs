using UnityEngine;
using System.Collections.Generic;

public class EnemyRock : Enemy
{
    private List<Intent> _intents;
    public override void Start()
    {
        base.Start();
        EffectTracker.AddEffect<EffectUnstoppable>(99);
        _intents = new List<Intent>()
        {
            new IntentMove(this, EnemySO.MovementSpeed)
        };
    }

    public override Intent ChooseIntent()
    {
        return _intents[0];
    }
}