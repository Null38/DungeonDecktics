using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
    [SerializeField]
    Button settingIcon;
    [SerializeField]
    Button deckIcon;
    [SerializeField]
    GameObject deckUI;
    [SerializeField]
    Vector2 DeckUIUp;
    [SerializeField]
    Vector2 DeckUIDown;
    bool isDeckUIMove = false;
    RectTransform deckRect;

    public void OnPauseUI()
    {
        Pause.SetActive(true);
    }

    private void Start()
    {
        foreach (CardBase card in GameManager.Instance.cardPile.GetHandPile)
        {
            spawner.SpawnCard(card);
        }

        GameManager.CardSelectedEvent += selector.Select;

        if (deckIcon != null)
        {
            settingIcon.onClick.AddListener(OnPauseUI);
        }
        else
        {
        }
        if (deckUI != null)
        {
            deckRect = deckUI.GetComponent<RectTransform>();
            deckRect.anchoredPosition = DeckUIDown;
        }
    }


    bool isMoving = false;

    public void DeckUImove()
    {
        if (deckUI == null || isMoving) return;

        isMoving = true;

        foreach (Transform child in deckUI.transform)
        {
            StartCoroutine(MoveUI(child.GetComponent<RectTransform>(), isDeckUIMove ? 100f : -100f));
        }

        isDeckUIMove = !isDeckUIMove;
    }

    IEnumerator MoveUI(RectTransform target, float moveAmount)
    {
        if (target == null) yield break;

        Vector2 startPos = target.anchoredPosition;
        Vector2 endPos = new(startPos.x, startPos.y + moveAmount);
        float elapsed = 0f;
        float duration = 0.3f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            target.anchoredPosition = Vector2.Lerp(startPos, endPos, elapsed / duration);
            yield return null;
        }

        target.anchoredPosition = endPos;

        isMoving = false;
    }
}
