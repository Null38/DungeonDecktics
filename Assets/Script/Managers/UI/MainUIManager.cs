using System;
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
        StartBar.onClick.AddListener(SceneLoadManager.LoadGame);
        OptionButton.onClick.AddListener(SceneLoadManager.LoadOption);
        UpgradBar.onClick.AddListener(GoUpgrade);
        rankBar.onClick.AddListener(CheckRank);
        CloseUpgradePanel.onClick.AddListener(CloseUpgrade);
    }
}
