using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageClearSceneUIManager : MonoBehaviour
{
    [SerializeField] private Button buttonNextStage;

    [SerializeField] private Button buttonPass;
    [SerializeField] private Button buttonChange;

    [SerializeField] private Image CurrEq;
    [SerializeField] private Image newEq;

    [SerializeField] private GameObject EquipmentChangeTap;



    private void Awake()
    {
        buttonNextStage.onClick.AddListener(OnNextStageClicked);
        buttonPass.onClick.AddListener(() => EquipmentChangeTap.SetActive(false));
        buttonChange.onClick.AddListener(SetNewEq);

        //CurrEq.sprite = GameManager.Instance.무슨무슨 장비
        //newEq.sprite = 무슨무슨 새로운 장비
        Debug.LogWarning("이 문구 보이면 구현하라 하는거임");
    }

    private void SetNewEq()
    {
        EquipmentChangeTap.SetActive(false);
        //DumbAssSave.eqSave[장] = 비;
        Debug.LogWarning("이 문구 보이면 구현하라 하는거임");
    }

    private void OnNextStageClicked()
    {
        SceneManager.UnloadSceneAsync("StageClearScene");
        SceneLoadManager.LoadNextGame(DumbAssSave.nextStage);
    }
}
