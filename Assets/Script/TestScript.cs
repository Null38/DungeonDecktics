using UnityEngine;
using UnityEngine.UI;

public class TestScript : MonoBehaviour
{
    public Button button;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button.onClick.AddListener(SceneLoadManager.LoadGame);
    }
}
