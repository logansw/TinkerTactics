using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    public SceneType CurrentScene { get; private set; }
    private Dictionary<SceneType, string> _sceneNames;

    public override void Initialize()
    {
        base.Initialize();
        CurrentScene = SceneType.Base;
        _sceneNames = new Dictionary<SceneType, string>();
        _sceneNames.Add(SceneType.Battle, "Battle");
        _sceneNames.Add(SceneType.Map, "Map");
        _sceneNames.Add(SceneType.TinkerShop, "TinkerShop");
        _sceneNames.Add(SceneType.TowerShop, "TowerShop");
    }

    public void LoadScene(SceneType sceneType)
    {
        if (CurrentScene != SceneType.Base)
        {
            UnloadScene(CurrentScene);
        }
        CurrentScene = sceneType;
        switch (sceneType)
        {
            case SceneType.Map:
                if (MapManager.s_Instance != null && MapManager.s_Instance.Loaded)
                {
                    MapManager.s_Instance.RestoreMapScene();
                }
                else
                {
                    StartCoroutine(LoadSceneAsync(sceneType));
                }
                break;
            default:
                StartCoroutine(LoadSceneAsync(sceneType));
                break;
        }
    }

    private void UnloadScene(SceneType sceneType)
    {
        switch (sceneType)
        {
            case SceneType.Map:
                MapManager.s_Instance.MinimizeMapScene();
                break;
            case SceneType.Battle:
                Camera.main.GetComponent<CustomCamera>().RemoveControls();
                SceneManager.UnloadSceneAsync(_sceneNames[sceneType]);
                break;
            case SceneType.TinkerShop:
                SceneManager.UnloadSceneAsync(_sceneNames[sceneType]);
                break;
            case SceneType.TowerShop:
                SceneManager.UnloadSceneAsync(_sceneNames[sceneType]);
                break;
            case SceneType.Base:
                Debug.LogError($"Should not unload Base Scene.");
                break;
            default:
                Debug.LogError($"Unload Scene: {sceneType} not implemented");
                break;
        }
    }

    private IEnumerator LoadSceneAsync(SceneType sceneType)
    {
        AsyncOperation handler = SceneManager.LoadSceneAsync(_sceneNames[sceneType], LoadSceneMode.Additive);
        while (!handler.isDone)
        {
            yield return null;
        }
        InitializationManager.s_Instance.InitializeScene(sceneType);
    }
}

[System.Serializable]
public enum SceneType
{
    Map,
    Battle,
    TinkerShop,
    TowerShop,
    Base
}