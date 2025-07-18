using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class CardComponent : MonoBehaviour
{
    [SerializeField]
    public CardBase cardInfo;

    [SerializeField]
    private Image cardBase;
    [SerializeField]
    private Image artworkImage;
    [SerializeField]
    private TMP_Text nameText;
    [SerializeField]
    private TMP_Text descriptionText;
    [SerializeField]
    private TMP_Text costText;
    [SerializeField]
    private Button interaction;

    public void CardInit(CardBase card)
    {
        cardInfo = card;
        DisplayCard();

        interaction.onClick.AddListener(OnCardClicked);
    }

    private void OnCardClicked()
    {
        GameManager.Instance.SpawnTarget(cardInfo);
        GameManager.CardSelectEvent(this, transform as RectTransform);
    }

    public void DisplayCard()
    {
        cardBase.color = cardInfo.backgroundColor;
        artworkImage.sprite = cardInfo.artwork;
        nameText.text = cardInfo.CardName;
        descriptionText.text = cardInfo.FormatDescription();
        costText.text = cardInfo.cost.ToString();
    }
}
