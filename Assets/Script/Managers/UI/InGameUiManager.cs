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
    [SerializeField]
    CardSpawner spawner;
    [SerializeField]
    Selector selector;
    public void OnPauseUI()
    {
        Pause.SetActive(true);
    }

    private void Start()
    {
        foreach (CardObjectBase card in GameManager.Instance.cardPile.GetHandPile)
        {
            spawner.SpawnCard(card);
        }

        GameManager.CardSelectedEvent += selector.Select;
    }
}
