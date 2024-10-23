using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Handles the game flow within a single battle.
/// Keeps track of the current turn and sends commands to towers and enemies each turn.
/// </summary>
public class BattleManager : Singleton<BattleManager>
{
    [SerializeField] private TMP_Text _button;
    [SerializeField] private List<TilePlot> _tilePlots;
    [SerializeField] private Healthbar _warlordHealthbar;

    public override void Initialize()
    {
        base.Initialize();
        StateController.s_Instance.ChangeState(StateType.Idle);
    }

    public void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
    }
    
    public void Continue()
    {
        if (StateController.CurrentState == StateType.Idle)
        {
            StateController.s_Instance.ChangeState(StateType.Playing);
            _button.text = "Pause";
        }
        else if (StateController.CurrentState == StateType.Playing)
        {
            StateController.s_Instance.ChangeState(StateType.Paused);
            _button.text = "Resume";
        }
        else if (StateController.CurrentState == StateType.Paused)
        {
            StateController.s_Instance.ChangeState(StateType.Playing);
            _button.text = "Pause";
        }
    }

    public void FinishWave()
    {
        StateController.s_Instance.ChangeState(StateType.Idle);
        _button.text = "Continue";
    }

    public void DamagePlayer(int damage = 1)
    {
        Player.s_Instance.Health.TakeDamage(damage);
    }
}