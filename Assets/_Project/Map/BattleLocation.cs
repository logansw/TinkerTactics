using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLocation : MapLocation
{
    private bool _initialized;

    public override void Initialize()
    {
        if (_initialized) { return; }
        base.Initialize();
        _button.onClick.AddListener(StartBattle);
        _initialized = true;
    }

    private void StartBattle()
    {
        
        SceneLoader.s_Instance.LoadScene(SceneType.Battle);
        SceneLoader.s_Instance.UnloadScene(SceneType.Map);
    }
}