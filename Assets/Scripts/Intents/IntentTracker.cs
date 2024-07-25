using UnityEngine;
using UnityEngine.UI;

public class IntentTracker : MonoBehaviour
{
    public Intent Intent;
    [SerializeField] private IntentUI _intentUI;
    private Enemy _enemy;
    private bool _updateQueued;

    void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    void Update()
    {
        if (_updateQueued)
        {
            UpdateIntent();
            _updateQueued = false;
        }
    }

    void OnEnable()
    {
        BattleManager.e_OnEnemyTurnEnd += ChooseIntent;
        _enemy.EffectTracker.e_OnEffectsChanged += QueueUpdate;
    }

    void OnDisable()
    {
        BattleManager.e_OnEnemyTurnEnd -= ChooseIntent;
        _enemy.EffectTracker.e_OnEffectsChanged -= QueueUpdate;
    }

    public void ChooseIntent()
    {
        if (!gameObject.activeInHierarchy) { return;}
        Intent = new IntentMove();
        Intent.Initialize(_enemy);
        _intentUI.Render(Intent);
    }

    private void QueueUpdate()
    {
        _updateQueued = true;
    }

    public void UpdateIntent()
    {
        if (_enemy.EffectTracker.HasEffect<EffectStun>(out EffectStun effectStun))
        {
            Intent = new IntentStun();
        }
        Intent.Initialize(_enemy);
        _intentUI.Render(Intent);
    }
}