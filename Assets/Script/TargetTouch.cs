using UnityEngine;
using UnityEngine.EventSystems;

public class TargetTouch : MonoBehaviour
{
    public Controller target;

    public void OnMouseDown()
    {
        if (!GameManager.Instance.selectCard.cardInfo.UseCard(target))
            return;

        GameManager.Instance.RemoveAllTarget();
        GameManager.Instance.cardPile.HandToDiscardPile(GameManager.Instance.selectCard);
    }
}

