using UnityEngine;
using UnityEngine.EventSystems;

public class TargetTouch : MonoBehaviour
{
    public void OnMouseDown()
    {
        Debug.LogWarning("아마.. 이놈과 겹쳐있는 유닛을 가져오는 식으로 뭐 할듯?");
        GameManager.Instance.UseCard();
    }
}

