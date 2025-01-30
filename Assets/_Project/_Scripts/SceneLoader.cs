using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    public void LoadScene(SceneType sceneType)
    {
        switch (sceneType)
        {
            case SceneType.Map:
                StartCoroutine(LoadMapScene());
                break;
            case SceneType.Battle:
                StartCoroutine(LoadBattleScene());
                break;
            case SceneType.Shop:
                StartCoroutine(LoadShopScene());
                break;
            default:
                Debug.LogError($"{sceneType} is not a valid scene type.");
                break;
        }
    }

    public void UnloadScene(SceneType sceneType)
    {
        switch (sceneType)
        {
            case SceneType.Map:
                SceneManager.UnloadSceneAsync("Map");
                break;
            case SceneType.Battle:
                Camera.main.GetComponent<CustomCamera>().RemoveControls();
                SceneManager.UnloadSceneAsync("Battle");
                break;
            case SceneType.Shop:
                SceneManager.UnloadSceneAsync("Shop");
                break;
            default:
                break;
        }
    }

    private IEnumerator LoadMapScene()
    {
        AsyncOperation handler = SceneManager.LoadSceneAsync("Map", LoadSceneMode.Additive);
        while (!handler.isDone)
        {
            yield return null;
        }
        InitializationManager.s_Instance.InitializeMapScene();
    }

    private IEnumerator LoadBattleScene()
    {
        AsyncOperation handler = SceneManager.LoadSceneAsync("Battle", LoadSceneMode.Additive);
        while (!handler.isDone)
        {
            yield return null;
        }
        InitializationManager.s_Instance.InitializeBattleScene();
    }

    private IEnumerator LoadShopScene()
    {
        AsyncOperation handler = SceneManager.LoadSceneAsync("Shop", LoadSceneMode.Additive);
        while (!handler.isDone)
        {
            yield return null;
        }
        InitializationManager.s_Instance.InitializeShopScene();
    }
}

[System.Serializable]
public enum SceneType
{
    Map,
    Battle,
    Shop,
    Base
}