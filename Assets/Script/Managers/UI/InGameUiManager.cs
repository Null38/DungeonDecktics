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
        foreach (CardObjectBase card in DataManager.player.CardPile.GetHandPile)
        {
            DataManager.cardSpawner.SpawnCard(card);
        }
    }
}
