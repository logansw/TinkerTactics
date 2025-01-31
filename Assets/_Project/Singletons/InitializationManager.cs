using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InitializationManager : Singleton<InitializationManager>
{
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

    public void InitializeBaseScene()
    {
        GameManager.s_Instance.Initialize();
        DeckManager.s_Instance.Initialize();
        SelectionManager.s_Instance.Initialize();
        StateController.s_Instance.Initialize();
    }

    public void InitializeBattleScene()
    {
        EnemyManager.s_Instance.Initialize();
        PathDrawer.s_Instance.Initialize();
        BlockManager.s_Instance.Initialize();
        WaveSpawnerManager.s_Instance.Initialize();
        BattleManager.s_Instance.Initialize();
        Camera.main.GetComponent<CustomCamera>().AddControls();
    }

    public void InitializeShopScene()
    {
        MarketplaceManager.s_Instance.Initialize();
    }

    public void InitializeMapScene()
    {
        MapManager.s_Instance.Initialize();
    }
}