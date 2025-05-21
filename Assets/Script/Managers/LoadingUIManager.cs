using UnityEngine;

public class LoadingUIManager : MonoBehaviour
{
    public RectTransform progressBar;
    private SceneLoadManager loadManager;

    private float maxWidth;

    private void Start()
    {
        loadManager = FindFirstObjectByType<SceneLoadManager>();
        maxWidth = progressBar.sizeDelta.x;
        progressBar.sizeDelta = new Vector2(0, progressBar.sizeDelta.y);
    }

    private void Update()
    {
        if (loadManager != null)
        {
            float progress = loadManager.GetLoadingProgress();
            progressBar.sizeDelta = new Vector2(maxWidth * progress, progressBar.sizeDelta.y);
        }
    }
}