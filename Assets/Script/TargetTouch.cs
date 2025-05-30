using UnityEngine;
using UnityEngine.EventSystems;

public class TargetTouch : MonoBehaviour
{
    public Controller target;

    public void OnMouseDown()
    {
        GameManager.Instance.selectCard.cardInfo.UseCard(target);
    }
}

