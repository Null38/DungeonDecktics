using UnityEngine;
using UnityEngine.EventSystems;

public class TargetTouchEventController : MonoBehaviour, IPointerClickHandler
{
    public void OnMouseDown()
    {
        Debug.Log("터치됨");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
    }

}
}

