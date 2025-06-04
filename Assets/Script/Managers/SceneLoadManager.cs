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
        LoadScene("GameTestScene");
    }

    public static void LoadMain()
    {
        LoadScene("MainScene");
        UnityEngine.Object.Destroy(GameManager.Instance.gameObject);
    }

    public static void LoadOption()
    {
        SceneManager.LoadSceneAsync("OptionScene", LoadSceneMode.Additive);
    }

    public static void LoadGameOver()
    {
        SceneManager.LoadSceneAsync("GameOverScene", LoadSceneMode.Additive);
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