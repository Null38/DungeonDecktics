using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUIManager : MonoBehaviour
{
    [SerializeField]
    Button StartBar;

    private void Awake()
    {
        StartBar.onClick.AddListener(SceneLoadManager.LoadGame);
    }
}
