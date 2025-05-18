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
        throw new NotImplementedException("씬이 로딩될때 이 메소드로 작동되도록 구현하면 됩니다. \n 씬을 모두 로드하면 loadingScene을 null로 바꿔주세요");
        //loadingScene.isDone == true 이거로 로딩 완료를 체크하시면 됩니다.
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



        throw new NotImplementedException("밑에 올바른 씬 이름을 적어주세요");
        instance.loadingScene = SceneManager.LoadSceneAsync("");
        instance.loadingScene.allowSceneActivation = false;
    }

    private static void IsSceneLoadAllowed()
    {
        if (instance.loadingScene != null)
            throw new InvalidOperationException("Another scene is already loading.");


        throw new NotImplementedException("밑에 로딩 중 띄울 씬을 설정해주세요");
        SceneManager.LoadScene("");
    }
}
