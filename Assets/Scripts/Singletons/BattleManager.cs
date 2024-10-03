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
    void Start()
    {
        StateController.s_Instance.ChangeState(StateType.Idle);
        TargetTilePlots();
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

    public void AttackTilePlots()
    {
        foreach (var tilePlot in _tilePlots)
        {
            if (tilePlot.IsTargeted)
            {
                foreach (var tower in tilePlot.Towers)
                {
                    tower.Health.ChangeHealth(-1);
                }
            }
            else if (!tilePlot.IsActivated)
            {
                foreach (var tower in tilePlot.Towers)
                {
                    tower.Health.ChangeHealth(1);
                }
            }
        }
    }

    public void TargetTilePlots()
    {
        foreach (TilePlot tilePlot in _tilePlots)
        {
            tilePlot.SetTargeted(false);
        }
        int numberOfTargets = UnityEngine.Random.Range(2, 6);
        int targetedTilePlots = 0;
        while (targetedTilePlots < numberOfTargets)
        {
            int randomTilePlotIndex = UnityEngine.Random.Range(0, _tilePlots.Count);
            if (!_tilePlots[randomTilePlotIndex].IsActivated || _tilePlots[randomTilePlotIndex].IsTargeted)
            {
                continue;
            }
            _tilePlots[randomTilePlotIndex].SetTargeted(true);
            targetedTilePlots++;
        }
    }
}