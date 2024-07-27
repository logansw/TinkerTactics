using UnityEngine;
using System.Collections.Generic;

public class EnemySlime : Enemy
{
    private List<Intent> _intents;
    public override void Start()
    {
        base.Start();
        _intents = new List<Intent>()
        {
            new IntentMove(this, EnemySO.MovementSpeed),
            new IntentDefend(this, 5),
            new IntentMove(this, 1),
        };
    }

    public override Intent ChooseIntent()
    {
        return _intents[Random.Range(0, _intents.Count)];
    }
}