using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializationManager : Singleton<InitializationManager>
{
    void Awake()
    {
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
        // Initialize all singletons
        DeckManager.s_Instance.Initialize();
        BattleManager.s_Instance.Initialize();
        // ColorManager.s_Instance.Initialize();
        // GameManager.s_Instance.Initialize();
        SelectionManager.s_Instance.Initialize();
        // CurrencyManager.s_Instance.Initialize();
        StateController.s_Instance.Initialize();
    }
}