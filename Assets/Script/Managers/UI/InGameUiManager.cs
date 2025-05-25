using UnityEngine;
using UnityEngine.UI;

public class InGameUiManager : MonoBehaviour
{
    [SerializeField]
    Slider HpBar;
    [SerializeField]
    Slider ShildBar;
    [SerializeField]
    GameObject Pause;
    public void OnPauseUI()
    {
        Pause.SetActive(true);
    }

    private void Start()
    {

    }
}
