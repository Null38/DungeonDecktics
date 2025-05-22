using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using AsyncOperation = UnityEngine.AsyncOperation;

[RequireComponent(typeof(GameManager))]
public class SceneLoadManager :MonoBehaviour
{
    private static SceneLoadManager instance;

    private AsyncOperation loadingScene = null;
    void Start()
    {
        SceneLoadManager.Initialize(this);
    }

    private void Update()
    {
        if (loadingScene != null)
        {
            OnSceneLoading();
        }
    }

    private void OnSceneLoading()
    {
        if (loadingScene.isDone && false)
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
       
        // 비동기 로드 시작 (allowSceneActivation은 false로 해서 자동 전환 방지)

        instance.loadingScene = SceneManager.LoadSceneAsync("LoadingTEST");
        instance.loadingScene.allowSceneActivation = false;
    }

    public static float GetLoadingProgress()
    {
        Debug.LogWarning("asdf");
        if (instance == null)
            throw new InvalidOperationException("씬로드메니저 인스턴스가 아직 초기화 안됨");
        return Mathf.Clamp01(instance.loadingScene.progress / 0.9f);

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