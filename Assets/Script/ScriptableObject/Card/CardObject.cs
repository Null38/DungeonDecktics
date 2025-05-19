using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum CardCategory { Attack, Armor, Utility }       // 카드 종류 일단 3개 설정 해놓고 공격+방어시 스크립터블 오브젝트 2개 넣는 방향으로 하면 될듯
public enum TargetType { Self, Enemy, Point }              // 타겟 타입

public abstract class CardObject : ScriptableObject
{
    public string CardName;
    [TextArea(3,5)]
    public string description;
    public Sprite artwork;
    public int cost;
    public CardCategory category;
    public TargetType targetType;
    public int range;

    public abstract void UseCard();
    public abstract string FormatDescription();
    public virtual void ApplyTo(GameObject cardGO)
    {
        // art 이미지 적용
        var artImage = cardGO.transform.Find("art").GetComponent<Image>();
        artImage.sprite = artwork;

        // 이름 적용
        var nameText = cardGO.transform.Find("cardnametext").GetComponent<TMP_Text>();
        nameText.text = CardName;

        // 설명 적용
        var descText = cardGO.transform.Find("descriptiontext").GetComponent<TMP_Text>();
        descText.text = FormatDescription();

        // 코스트 적용
        var costText = cardGO.transform.Find("costtext").GetComponent<TMP_Text>();
        costText.text = cost.ToString();
    }
}