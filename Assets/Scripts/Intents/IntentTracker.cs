using UnityEngine;
using UnityEngine.UI;

public class IntentTracker : MonoBehaviour
{
    public Intent Intent;
    [SerializeField] private IntentUI _intentUI;
    private Enemy _enemy;

    // TODO: Make the Stun effect forcibly change the intent to Stun.
    // TODO: Make sure the next turn's move effect uses the correct MovementSpeed value. This turn's not last turn's.

    void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    void OnEnable()
    {
        BattleManager.e_OnEnemyTurnEnd += ChooseIntent;
        _enemy.EffectTracker.e_OnEffectsChanged += UpdateIntent;
    }

    void OnDisable()
    {
        BattleManager.e_OnEnemyTurnEnd -= ChooseIntent;
        _enemy.EffectTracker.e_OnEffectsChanged -= UpdateIntent;
    }

    public void ChooseIntent()
    {
        if (!gameObject.activeInHierarchy) { return;}
        Intent = new IntentMove();
        Intent.Initialize(_enemy);
        _intentUI.Render(Intent);
    }

    public void UpdateIntent()
    {
        Intent.Initialize(_enemy);
        _intentUI.Render(Intent);
    }
}