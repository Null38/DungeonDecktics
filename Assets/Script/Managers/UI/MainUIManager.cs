using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUIManager : MonoBehaviour
{
    [SerializeField]
    Button StartBar;
    [SerializeField]
    Button OptionButton;
    [SerializeField]
    Button UpgradBar;
    [SerializeField]
    Button rankBar;
    [SerializeField]
    GameObject upgradePanel;
    [SerializeField]
    Button CloseUpgradePanel;
    [SerializeField]
    TextMeshProUGUI[] TlqkfAra; [SerializeField]
    TextMeshProUGUI ahahahahaha;
    public void GoUpgrade()
    {
        upgradePanel.SetActive(true);
    }
    public void CloseUpgrade()
    {
        upgradePanel.SetActive(false);
    }
    public void CheckRank()
    {
        Debug.Log("업그레이드 버튼 눌림");
    }

    private void Awake()
    {
        ahahahahaha.text = DumbAssSave.FUCK.ToString();
        TlqkfAra[0].text = DumbAssSave.HP.ToString();
        TlqkfAra[1].text = DumbAssSave.DAMAGE.ToString();
        TlqkfAra[2].text = DumbAssSave.COST.ToString();
        StartBar.onClick.AddListener(SceneLoadManager.LoadGame);
        OptionButton.onClick.AddListener(SceneLoadManager.LoadOption);
        UpgradBar.onClick.AddListener(GoUpgrade);
        rankBar.onClick.AddListener(CheckRank);
        CloseUpgradePanel.onClick.AddListener(CloseUpgrade);
    }

    public void Tlqkf(int i)
    {
        if (DumbAssSave.FUCK <= 0)
        {
            return;
        }
        int a = 0;
        switch (i)
        {
            case 0:
                a = ++DumbAssSave.HP;
                break;
            case 1:
                a = ++DumbAssSave.DAMAGE;
                break;
            case 2:
                a = ++DumbAssSave.COST;
                break;
        }

        TlqkfAra[i].text = a.ToString();


        ahahahahaha.text = (--DumbAssSave.FUCK).ToString();
    }
}
