using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopLocation : MapLocation
{
    private bool _initialized;


    public override void Initialize()
    {
        if (_initialized) { return; }
        base.Initialize();
        _button.onClick.AddListener(StartShop);
        _initialized = true;
    }

    private void StartShop()
    {
        SceneLoader.s_Instance.LoadScene(SceneType.Shop); 
        SceneLoader.s_Instance.UnloadScene(SceneType.Map);
    }
}
