using UnityEngine;
using UnityEngine.UI;

public class EndingUIManager : MonoBehaviour
{
    [SerializeField] private Button btnMain;
    

    void Awake()
    {
        btnMain.onClick.AddListener(() => {
            SceneLoadManager.LoadMain();
        });
        
    }
}
