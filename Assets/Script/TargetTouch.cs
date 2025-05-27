using UnityEngine;
using UnityEngine.EventSystems;

public class TargetTouch : MonoBehaviour, IPointerClickHandler
{
    public void OnMouseDown()
    {
        Debug.Log("터치됨");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}

