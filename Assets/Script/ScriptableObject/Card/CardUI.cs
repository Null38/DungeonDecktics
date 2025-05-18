using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardUI : MonoBehaviour
{
    public Image artworkImage;
    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public TMP_Text costText;

    public void SetCard(CardObject card)
    {
        artworkImage.sprite = card.artwork;
        nameText.text = card.CardName;
        descriptionText.text = card.FormatDescription();
        costText.text = card.cost.ToString();
    }
}
