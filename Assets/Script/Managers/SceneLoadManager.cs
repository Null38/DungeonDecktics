using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using AsyncOperation = UnityEngine.AsyncOperation;

public class SceneLoadManager
{
    public enum SceneState
    {
        Loading,
        Main,
        InGame
    }

    private static AsyncOperation loadingScene;
    private static string sceneName;

    public static SceneState State { get; }

    public static void SceneLoaded()
    {
        // 로드 완료됐으면 씬 활성화
        loadingScene.allowSceneActivation = true;

        // 로딩 끝났으니 초기화
        loadingScene = null;
    }

    public static void LoadGame()
    {
        DumbAssSave.nextStage = 1;
        DumbAssSave.savedHp = 0;
        DumbAssSave.eqSave = new EquipmentItemObject[2];
        LoadScene("GameTestScene");
    }

    public static void LoadNextGame(int dumint)
    {
        DumbAssSave.nextStage++;
        UnityEngine.Object.Destroy(GameManager.Instance.gameObject);
        LoadScene($"GameTestScene {dumint}");
    }

    public static void LoadMain()
    {
        LoadScene("MainScene");
        UnityEngine.Object.Destroy(GameManager.Instance.gameObject);
    }
    public static void LoadEnding()
    {
        SceneManager.LoadScene("EndingScene", LoadSceneMode.Single);
    }

    public static void LoadOption()
    {
        SceneManager.LoadSceneAsync("OptionScene", LoadSceneMode.Additive);
    }

    public static void LoadGameOver()
    {
        SceneManager.LoadSceneAsync("GameOverScene", LoadSceneMode.Additive);
    }
    public static void LoadStageClear()
    {
        SceneManager.LoadSceneAsync("StageClearScene", LoadSceneMode.Additive);
    }

    public static void LoadLastStageClear()
    {
        SceneManager.LoadSceneAsync("LastStageClearScene", LoadSceneMode.Additive);
    }

    

    private static void LoadScene(String scene)
    {
        IsSceneLoadAllowed((_) =>
        {
            loadingScene = SceneManager.LoadSceneAsync(sceneName);
            loadingScene.allowSceneActivation = false;
        });

        sceneName = scene;
    }

    public static float GetLoadingProgress()
    {
        return Mathf.Clamp01(loadingScene.progress / 0.9f);

    }


    private static void IsSceneLoadAllowed(Action<AsyncOperation> loadScene)
    {
        if (loadingScene != null)
            throw new InvalidOperationException("Another scene is already loading.");

        AsyncOperation load = SceneManager.LoadSceneAsync("LoadingScene", LoadSceneMode.Additive);
        load.completed += loadScene;
        
        return;
    }
}