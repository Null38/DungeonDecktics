using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUIManager : MonoBehaviour
{
 
   
    [SerializeField]
    Button StartBar;

    private void Start()
    {
        Debug.Log("로딩클릭 앞에서 확인됨");
        StartBar.onClick.AddListener(SceneLoadManager.LoadGame);
    }

    private void OnClickStartBar()
    {
        Debug.Log("로딩클릭 확인됨");
        SceneLoadManager.LoadGame();

    }
}
