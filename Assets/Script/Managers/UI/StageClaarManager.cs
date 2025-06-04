using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageClearSceneUIManager : MonoBehaviour
{
    [SerializeField] private Button buttonNextStage;
        
    [SerializeField] private int nextStageIndex = 1;

    private void Awake()
    {
        if (buttonNextStage == null)
        {
            return;
        }

        buttonNextStage.onClick.AddListener(OnNextStageClicked);
    }

    private void OnDestroy()
    {
        if (buttonNextStage != null)
            buttonNextStage.onClick.RemoveListener(OnNextStageClicked);
    }

    private void OnNextStageClicked()
    {
        SceneManager.UnloadSceneAsync("StageClearScene");
        SceneLoadManager.LoadNextGame(nextStageIndex);
    }
}
