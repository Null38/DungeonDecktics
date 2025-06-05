using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class InfoUI : EventTrigger
{
    public TextMeshProUGUI Tmp;

    public GameObject TmpObj;
    public override void OnPointerDown(PointerEventData data)
    {
        TmpObj.SetActive(true);
    }
    public override void OnPointerExit(PointerEventData data)
    {
        TmpObj.SetActive(false);
    }

    public override void OnPointerUp(PointerEventData data)
    {
        TmpObj.SetActive(false);
    }

    public void Update()
    {
        if (TmpObj.activeSelf)
        {
            Tmp.text = GameManager.Instance.selectCard.cardInfo.FormatDescription();
        }
    }
}
