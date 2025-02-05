using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    [SerializeField] private List<string> _levelNames = new List<string>();
    public int CurrentLevelIndex;
    public static Action e_OnNextLevel;

    public override void Initialize()
    {
        base.Initialize();
        CurrentLevelIndex = 0;
        SceneLoader.s_Instance.LoadScene(SceneType.Map);
    }

    public void FinishLevel()
    {
        TowerManager.s_Instance.ClearTowers();
        BattleManager.s_Instance.SetTimeScale(1f);

        SceneLoader.s_Instance.LoadScene(SceneType.Map);

        WaveSpawnerManager.s_Instance.UnassignSpawners();
        WaveSpawnerManager.s_Instance.ClearWaveSpawners();
        
        NextLevel();
    }

    public void NextLevel()
    {
        CurrentLevelIndex++;
        e_OnNextLevel?.Invoke();

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
}
