using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using AsyncOperation = UnityEngine.AsyncOperation;

[RequireComponent(typeof(GameManager))]
public static class SceneLoadManager
{
    private static AsyncOperation loadingScene;
    private static string sceneName;

    private static void OnSceneLoading(AsyncOperation op)
    {
        // 로드 완료됐으면 씬 활성화
        loadingScene.allowSceneActivation = true;

        // 로딩 끝났으니 초기화
        loadingScene = null;
    }

    public static void LoadScene(String scene)
    {
        IsSceneLoadAllowed().completed += LoadSceneAsync;

        sceneName = scene;
    }

    private static void LoadSceneAsync(AsyncOperation op)
    {

        loadingScene = SceneManager.LoadSceneAsync(sceneName);
        loadingScene.allowSceneActivation = false;
        loadingScene.completed += OnSceneLoading;
    }

    public static float GetLoadingProgress()
    {
        return Mathf.Clamp01(loadingScene.progress / 0.9f);

    }


    private static AsyncOperation IsSceneLoadAllowed()
    {
        if (loadingScene != null)
            throw new InvalidOperationException("Another scene is already loading.");

        // 로딩씬 이름을 여기에 적는다
        return SceneManager.LoadSceneAsync("LoadingScene");
    }
}