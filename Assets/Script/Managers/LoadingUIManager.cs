using UnityEngine;
using UnityEngine.UI;

public class LoadingUIManager : MonoBehaviour
{
    public Slider progressBar;

    private float maxWidth;

    private void Start()
    {
        Debug.Log("asdf");
        progressBar.value = 0f;

    }

    private void Update()
    {
        Debug.Log("asdg");
        progressBar.value = SceneLoadManager.GetLoadingProgress();
    }
}