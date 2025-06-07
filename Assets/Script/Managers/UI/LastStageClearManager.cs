using UnityEngine;
using UnityEngine.UI;

public class LastStageClearManager : MonoBehaviour
{
    [SerializeField] private Button btnEnding;


    void Awake()
    {
        btnEnding.onClick.AddListener(() => { 
            SceneLoadManager.LoadEnding(); 
        });

    }
}
