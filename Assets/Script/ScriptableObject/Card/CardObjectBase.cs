using UnityEngine;


public abstract class CardObjectBase : ScriptableObject
{
    public enum CardCategory { Attack, Armor, Utility }       // 카드 종류 일단 3개 설정 해놓고 공격+방어시 스크립터블 오브젝트 2개 넣는 방향으로 하면 될듯
    public enum TargetType { Self, other, Point }             // 타겟 타입

    public string CardName;
    [TextArea(3,5)]
    public string description;
    public Color backgroundColor;

    public Sprite artwork;
    public int cost;
    public int distance;
    public CardCategory category;
    public TargetType targetType;


    public bool isUpgraded = false;

    public abstract void UseCard(Controller user);
    public abstract string FormatDescription();

    /// <summary>
    /// 카드 강화.
    /// 끝나고서 isUpgraded는 참으로 바꿔주세요.
    /// </summary>
    public abstract string Upgrad();
}

interface IAfterUseHandler
{

}