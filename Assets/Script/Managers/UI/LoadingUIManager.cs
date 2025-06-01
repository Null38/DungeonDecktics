using UnityEngine;
using UnityEngine.UI;

public class LoadingUIManager : MonoBehaviour
{
    public Slider progressBar;

    private void Start()
    {
        progressBar.value = 0f;

    }

    private void Update()
    {
        float value = SceneLoadManager.GetLoadingProgress();
        progressBar.value = value;

        if (value == 1)
        {
            SceneLoadManager.SceneLoaded();
        }
    }
}