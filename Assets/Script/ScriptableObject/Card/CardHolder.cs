using UnityEngine;

public class CardHolder : MonoBehaviour
{
    public CardObject cardData;

    private void Start()
    {
        if (cardData != null)
        {
            cardData.ApplyTo(gameObject);
        }
    }
}
