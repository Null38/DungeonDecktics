using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using TMPro;

public class InGameUIManager : MonoBehaviour
{
    [SerializeField]
    Slider HpBar;
    [SerializeField]
    Slider ShildBar;
    [SerializeField]
    Button settingIcon;
    [SerializeField]
    Button LoadMainButton;
    [SerializeField]
    Button OptionButton;
    [SerializeField]
    Button invenIcon;
    [SerializeField]
    Button restIcon;
    [SerializeField]
    Button disCardPile;
    [SerializeField]
    Button drawPile;
    [SerializeField]
    TextMeshProUGUI CostText;

    [SerializeField]
    RectTransform deckUI;
    [SerializeField]
    GameObject Pause;

    [SerializeField]
    CardSpawner spawner;
    [SerializeField]
    Selector selector;

    [SerializeField]
    Vector2 DeckUIShowPos;
    [SerializeField]
    Vector2 DeckUIHidePos;

    bool isDeckUIHide = false;

    public static event Action GetRestEvent;

    public void OnPauseUI()
    {
        Pause.SetActive(true);
    }
    public void OpenInventory()
    {
        Debug.Log("인벤 버튼 눌림");
    }
    public void GetRest()
    {
        GetRestEvent?.Invoke();
    }
    public void CheckDrawPile()
    {
        Debug.Log("뽑을 카드 확인 버튼 눌림");
    }
    public void CheckDisCardPile()
    {
        Debug.Log("버린 카드 확인 버튼 눌림");
    }
    public void ClosePauseUI()
    {
        Pause.SetActive(false);
    }

    private void Start()
    {
        if (deckUI == null)
            throw new ArgumentNullException("deckUI is null");

        foreach (CardBase card in GameManager.Instance.cardPile.GetHandPile)
        {
            spawner.SpawnCard(card);
        }

        GameManager.CardSelectedEvent += selector.Select;

        LoadMainButton.onClick.AddListener(SceneLoadManager.LoadMain);
        OptionButton.onClick.AddListener(SceneLoadManager.LoadOption);
        settingIcon.onClick.AddListener(OnPauseUI);

        invenIcon.onClick.AddListener (OpenInventory);
        restIcon.onClick.AddListener(GetRest);
        disCardPile.onClick.AddListener(CheckDisCardPile);
        drawPile.onClick.AddListener(CheckDrawPile);
    }

    private void Update()
    {
        HpBar.maxValue = DataManager.player.info.MaxHp;
        HpBar.value = DataManager.player.info.currentHp;
        ShildBar.maxValue = DataManager.player.info.currentHp;
        ShildBar.value = Mathf.Min(DataManager.player.info.currentShield, ShildBar.maxValue);
        CostText.text = DataManager.player.info.currentCost.ToString();
    }


    bool isMoving = false;

    public void DeckUImove()
    {
        if (deckUI == null || isMoving) return;

        isMoving = true;

        isDeckUIHide = !isDeckUIHide;

        StartCoroutine(MoveUI());
    }

    IEnumerator MoveUI()
    {
        Vector2 startPos = deckUI.anchoredPosition;
        Vector2 endPos = isDeckUIHide ? DeckUIHidePos : DeckUIShowPos;
        float elapsed = 0f;
        float duration = 0.1f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            deckUI.anchoredPosition = Vector2.Lerp(startPos, endPos, elapsed / duration);
            yield return null;
        }

        deckUI.anchoredPosition = endPos;

        isMoving = false;
    }
}
