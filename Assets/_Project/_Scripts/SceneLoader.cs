using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    public void LoadScene(string sceneName)
    {
        switch (sceneName)
        {
            case "Map":

                break;
            case "Battle":
                StartCoroutine(LoadBattleScene());
                break;
            case "Shop":

                break;
            default:
                Debug.LogError($"{sceneName} is not a valid scene name.");
                break;
        }
    }

    private IEnumerator LoadBattleScene()
    {
        Debug.Log("Battle");
        AsyncOperation handler = SceneManager.LoadSceneAsync("Battle", LoadSceneMode.Additive);
        while (!handler.isDone)
        {
            yield return null;
            Debug.Log("Waiting");
        }
        InitializationManager.s_Instance.InitializeBattleScene();
        Debug.Log("done");
    }
}