using UnityEngine;
using System.Collections.Generic;

public class EnemyPink : Enemy
{
    private List<Intent> _intents;
    public override void Start()
    {
        base.Start();
        _intents = new List<Intent>()
        {
            new IntentMove(this, EnemySO.MovementSpeed),
        };
    }

    public override Intent ChooseIntent()
    {
        return _intents[Random.Range(0, _intents.Count)];
    }
}