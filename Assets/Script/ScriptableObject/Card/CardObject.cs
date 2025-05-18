using UnityEngine;

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
}