using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InitializationManager : Singleton<InitializationManager>
{
    private void DebugInitialize()
    {
        InitializeBattleScene(1);
    }

    protected override void Awake()
    {
        base.Awake();
        StartCoroutine(DelayedInitialize());
    }

    public IEnumerator DelayedInitialize()
    {
        yield return new WaitForEndOfFrame();
        Initialize();
    }

    public override void Initialize()
    {
        base.Initialize();
        InitializeBaseScene();
    }

    public void InitializeScene(SceneType sceneType)
    {
        switch (sceneType)
        {
            case SceneType.Base:
                InitializeBaseScene();
                break;
            case SceneType.Map:
                InitializeMapScene();
                break;
            case SceneType.Battle:
                InitializeBattleScene(GameManager.s_Instance.CurrentLevelIndex);
                break;
            case SceneType.TinkerShop:
                InitializeTinkerShopScene();
                break;
            case SceneType.TowerShop:
                InitializeTowerShopScene();
                break;
            default:
                Debug.LogError($"Initialization for SceneType: {sceneType} not imlemented");
                break;
        }
    }

    private void InitializeBaseScene()
    {
        SceneLoader.s_Instance.Initialize();
        GameManager.s_Instance.Initialize();
        DeckManager.s_Instance.Initialize();
        SelectionManager.s_Instance.Initialize();
        StateController.s_Instance.Initialize();
    }

    private void InitializeMapScene()
    {
        MapManager.s_Instance.Initialize();
    }

    private void InitializeBattleScene(int level)
    {
        EnemyManager.s_Instance.Initialize(level);
        PathDrawer.s_Instance.Initialize();
        BlockManager.s_Instance.Initialize();
        WaveSpawnerManager.s_Instance.Initialize();
        BattleManager.s_Instance.Initialize();
        Camera.main.GetComponent<CustomCamera>().AddControls();
    }

    private void InitializeTinkerShopScene()
    {
        MarketplaceManager.s_Instance.Initialize();
    }

    private void InitializeTowerShopScene()
    {
        MarketplaceManager.s_Instance.Initialize();        
    }
}