using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private List<Warlord> _warlords = new List<Warlord>();
    [SerializeField] private List<string> _levelNames = new List<string>();
    public int CurrentLevelIndex;
    private Warlord _currentWarlord;
    public static Action e_OnNextLevel;

    public override void Initialize()
    {
        base.Initialize();
        CurrentLevelIndex = 0;
        _currentWarlord = Instantiate(_warlords[CurrentLevelIndex]);
    }

    public void NextLevel()
    {
        CurrentLevelIndex++;
        _currentWarlord = Instantiate(_warlords[CurrentLevelIndex]);
        e_OnNextLevel?.Invoke();
        DeckManager.s_Instance.Reset();
        WaveSpawnerManager.s_Instance.NextLevel();
        PathDrawer.s_Instance.NextLevel();
        WaveSpawnerManager.s_Instance.UnassignSpawners();
        StateController.s_Instance.ChangeState(StateType.Idle);
        TowerManager.s_Instance.ClearTowers();
    }

    public string GetLevelName()
    {
        return _levelNames[CurrentLevelIndex];
    }

    public Warlord GetWarlord()
    {
        return _currentWarlord;
    }
}
