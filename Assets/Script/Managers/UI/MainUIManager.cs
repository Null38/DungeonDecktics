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
    public void GoUpgrade()
    {
        Debug.Log("업그레이드 버튼 눌림");
    }
    public void CheckRank()
    {
        Debug.Log("랭킹 확인 버튼 눌림");
    }

    private void Awake()
    {
        StartBar.onClick.AddListener(SceneLoadManager.LoadGame);
        OptionButton.onClick.AddListener(SceneLoadManager.LoadOption);
        UpgradBar.onClick.AddListener(GoUpgrade);
        rankBar.onClick.AddListener(CheckRank);
    }
}
