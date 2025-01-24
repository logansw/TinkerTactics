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

        WaveSpawnerManager.s_Instance.NextLevel();
        PathDrawer.s_Instance.NextLevel();
        BlockManager.s_Instance.InstantiateBlocks();
        WaveSpawnerManager.s_Instance.UnassignSpawners();

        StateController.s_Instance.ChangeState(StateType.Idle);
        TowerManager.s_Instance.ClearTowers();
        MarketplaceManager.s_Instance.PopulateAvailableItems();
        MarketplaceManager.s_Instance.ShowShop(true);
        BattleManager.s_Instance.SetTimeScale(1f);

        int newTowerLimit;
        switch (CurrentLevelIndex)
        {
            case 1:
                newTowerLimit = 2;
                break;
            case 2:
                newTowerLimit = 3;
                break;
            case 3:
                newTowerLimit = 4;
                break;
            case 4:
                newTowerLimit = 5;
                break;
            case 5:
                newTowerLimit = 6;
                break;
            default:
                newTowerLimit = 7;
                break;
        }
        TowerManager.s_Instance.TowerDeployLimit = newTowerLimit;
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
