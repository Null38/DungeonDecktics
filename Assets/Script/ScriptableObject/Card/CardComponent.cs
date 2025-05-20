using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class CardComponent : MonoBehaviour
{
    [SerializeField]
    private CardObjectBase cardInfo;

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

    public void CardInit(CardObjectBase card)
    {
        cardInfo = card;
        DisplayCard();
    }

    public void DisplayCard()
    {
        cardBase.color = Color.white;
        Debug.LogWarning("색 적용 아직 안함");
        artworkImage.sprite = cardInfo.artwork;
        nameText.text = cardInfo.CardName;
        //descriptionText.text = cardInfo.FormatDescription();
        costText.text = cardInfo.cost.ToString();
    }
}
