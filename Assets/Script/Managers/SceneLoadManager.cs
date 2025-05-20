using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using AsyncOperation = UnityEngine.AsyncOperation;

[RequireComponent(typeof(GameManager))]
public class SceneLoadManager :MonoBehaviour
{
    private static SceneLoadManager instance;

    private AsyncOperation loadingScene = null;

    private void Update()
    {
        if (loadingScene != null)
        {
            OnSceneLoading();
        }
    }

    private void OnSceneLoading()
    {
        if (loadingScene.isDone)
        {
            // 로드 완료됐으면 씬 활성화
            loadingScene.allowSceneActivation = true;

            // 로딩 끝났으니 초기화
            loadingScene = null;
        }
    }


    /// <summary>
    /// This method runs only once in the Game Manager. Do not call it carelessly.
    /// </summary>
    public static void Initialize(SceneLoadManager instance)
    {
        SceneLoadManager.instance = instance;
    }

    public static void LoadMainScene()
    {
        IsSceneLoadAllowed();

        // 실제 메인 씬 이름 적기
        string mainSceneName = "MainScene";

        // 비동기 로드 시작 (allowSceneActivation은 false로 해서 자동 전환 방지)
        instance.loadingScene = SceneManager.LoadSceneAsync(mainSceneName);
        instance.loadingScene.allowSceneActivation = false;
    }


    private static void IsSceneLoadAllowed()
    {
        if (instance.loadingScene != null)
            throw new InvalidOperationException("Another scene is already loading.");

        // 로딩씬 이름을 여기에 적는다
        SceneManager.LoadScene("LoadingScene");
    }
}
//로딩할 때 보여주는 화면이랑 씬 로딩 구현

