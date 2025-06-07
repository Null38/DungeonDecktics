using System.Collections.Generic;
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
    
    
    [SerializeField] private List<EquipmentItemObject> equipList;
    [SerializeField] private EquipmentItemObject selectedEquip;


    private void Awake()
    {
        buttonNextStage.onClick.AddListener(OnNextStageClicked);
        buttonPass.onClick.AddListener(() => EquipmentChangeTap.SetActive(false));
        buttonChange.onClick.AddListener(SetNewEq);

        selectedEquip = equipList[Random.Range(0, equipList.Count - 1)];

        Debug.Log(selectedEquip.name);
        CurrEq.sprite = GameManager.Instance.inventory.sprite[(int)selectedEquip.type];
        newEq.sprite = selectedEquip.img;
    }

    private void SetNewEq()
    {
        EquipmentChangeTap.SetActive(false);
        DumbAssSave.eqSave[(int)selectedEquip.type] = selectedEquip;
    }

    private void OnNextStageClicked()
    {
        SceneManager.UnloadSceneAsync("StageClearScene");
        SceneLoadManager.LoadNextGame(DumbAssSave.nextStage);
    }
}
