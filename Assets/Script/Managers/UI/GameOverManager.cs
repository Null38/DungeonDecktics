using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUIManager : MonoBehaviour
{
    [SerializeField]
    private Button mainButton;

    [SerializeField]
    private Button retryButton;
        
    private void Awake()
    {
        if (mainButton == null || retryButton == null)
        {
            return;
        }
        mainButton.onClick.AddListener(OnMainButtonClicked);
        retryButton.onClick.AddListener(OnRetryButtonClicked);
    }

    private void OnDestroy()
    {
        if (mainButton != null)
            mainButton.onClick.RemoveListener(OnMainButtonClicked);
        if (retryButton != null)
            retryButton.onClick.RemoveListener(OnRetryButtonClicked);
    }
    private void OnMainButtonClicked()
    {
        SceneLoadManager.LoadMain();
    }

    private void OnRetryButtonClicked()
    {
        UnityEngine.Object.Destroy(GameManager.Instance.gameObject);
        SceneLoadManager.LoadGame();
    }
}
