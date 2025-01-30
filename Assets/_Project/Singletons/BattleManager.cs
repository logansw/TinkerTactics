using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Profiling;
using UnityEngine;

/// <summary>
/// Handles the game flow within a single battle.
/// </summary>
public class BattleManager : Singleton<BattleManager>
{
    [SerializeField] private List<TilePlot> _tilePlots;
    [SerializeField] private Healthbar _warlordHealthbar;
    public bool WarlordDefeated;
    private float _previousTimeScale;

    public override void Initialize()
    {
        base.Initialize();
        StateController.s_Instance.ChangeState(StateType.Idle);
    }

    public void SetTimeScale(float timeScale)
    {
        _previousTimeScale = Time.timeScale;
        Time.timeScale = timeScale;
    }

    public void UndoTimeScale()
    {
        SetTimeScale(_previousTimeScale);
    }
    
    public void Continue()
    {
        if (StateController.CurrentState == StateType.Idle)
        {
            StateController.s_Instance.ChangeState(StateType.Playing);
        }
    }

    public void FinishWave()
    {
        StateController.s_Instance.ChangeState(StateType.Idle);

        if (EnemyManager.s_Instance.CurrentWarlord.IsDead)
        {
            GameManager.s_Instance.FinishLevel();
        }
    }

    public void DamagePlayer(int damage = 1)
    {
        Player.s_Instance.Health.TakeDamage(damage);
    }
}