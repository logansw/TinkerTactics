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

    void Update()
    {
        if (_updateQueued)
        {
            UpdateIntent();
            _updateQueued = false;
        }
    }

    public void ChooseIntent()
    {
        if (!gameObject.activeInHierarchy) { return;}
        Intent = _enemy.ChooseIntent();
        Intent.Calculate();
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
            Intent = new IntentStun(_enemy, effectStun.Duration);
        }
        Intent.Calculate();
        _intentUI.Render(Intent);
    }
}