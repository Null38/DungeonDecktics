using TMPro;
using UnityEngine;

public enum CardCategory { Attack, Armor, Utility }       // 카드 종류
public enum TargetType { Self, Enemy, Area }              // 타겟 타입
public enum ValueType { Flat, Percentage, StatusEffect }  // 효과 타입

[CreateAssetMenu(menuName = "Card")]
public class CardData : ScriptableObject
{
    // [카드 기본 정보]
    public string cardName;
    public string description;
    public Sprite artwork;
    public int cost;

    // [카드 분류 및 타겟 정보]
    public CardCategory category;
    public TargetType targetType;
    public int range; // 공격/유틸 범위 (예: 1이면 인접, 3이면 3칸 등)

    // [카드 효과 정보]
    public ValueType valueType;
    public float amount;             // Flat 데미지, 퍼센트 비율 등
    public string statusEffectID;    // 상태이상 이름 (예: "Burn", "Stun")
    public float lifeStealPercentage; // 피흡 공격
}

public class CardExecutor : MonoBehaviour
{
    public void ExecuteCard(CardData card, State target)
    {
        switch (card.category)
        {
            case CardCategory.Attack:
                ApplyAttack(card, target);
                break;
            case CardCategory.Armor:
                ApplyDefense(card, target);
                break;
            case CardCategory.Utility:
                ApplyUtility(card, target);
                break;
        }
    }

    void ApplyAttack(CardData card, State target)
    {
        int damage = 0;

        if (card.valueType == ValueType.Flat)
        {
            damage = (int)card.amount;
        }
        else if (card.valueType == ValueType.Percentage)
        {
            damage = (int)(target.CurrentHealth * (card.amount / 100f));
        }

        target.TakeDamage(damage);
        Debug.Log($"[Attack] {card.cardName} → 대상에게 {damage} 피해 (범위: {card.range})");

        if (card.lifeStealPercentage > 0)
        {
            int healAmount = (int)(damage * (card.lifeStealPercentage / 100f));
            target.Heal(healAmount);
            Debug.Log($"[LifeSteal] {card.cardName} → {healAmount} 체력 흡수");
        }
    }

    void ApplyDefense(CardData card, State target)
    {
        int armorAmount = (int)card.amount;
        int duration = 3; // 기본 방어 지속 턴
        target.ApplyTemporaryArmor(armorAmount, duration);
        Debug.Log($"[Armor] {card.cardName} → {armorAmount} 방어도 적용 (3턴)");
    }

    void ApplyUtility(CardData card, State target)
    {
        if (card.valueType == ValueType.StatusEffect)
        {
            Debug.Log($"[Utility] 상태이상 '{card.statusEffectID}' 적용");
            // 나중에 상태이상 시스템 연동
        }
        else if (card.valueType == ValueType.Flat)
        {
            target.Heal((int)card.amount);
            Debug.Log($"[Utility] {card.cardName} → 체력 {(int)card.amount} 회복");
        }
    }
}


//우선 임시적으로 State 클래스 생성해둠. 나중에 합칠 경우 수정 필요.

//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;

// 여기 부분은 state 랑 함께 연계해야 함
//void ApplyAttack(State target)
//{
//    int damage = 0;

//    // Flat 데미지
//    if ((valueType & ValueType.Flat) == ValueType.Flat)
//    {
//        damage += (int)amount;
//    }
//    //최대 체력 공격 부분. 일단 주석처리 함. 나중에 현 체력 비례 공격 부분도 넣어야 할듯.
//    */ //여기는 지우기

//        // Percentage 데미지
//        if ((valueType & ValueType.Percentage) == ValueType.Percentage)
//    {
//        damage += (int)(target.maxHealth * (amount / 100f));
//    }

//void ApplyUtility(State target)
//{
//    if (valueType == ValueType.StatusEffect)
//    {
//        Debug.Log($"적용된 상태이상: {statusEffectID}"); // 나중에 상태이상 시스템 연동
//    }
//    else if (valueType == ValueType.Flat && amount > 0)
//    {
//        target.Heal((int)amount);
//    }
//    else
//    {
//        Debug.Log("유틸 효과 없음");
//    }
//}
//}


//    target.TakeDamage(damage);

//void ApplyDefense(State target)
//{
//    int armorAmount = (int)amount;
//    int duration = 3; // 기본 방어도 유지 턴 수

//    target.ApplyTemporaryArmor(armorAmount, duration);
//}
////여기는 지우기

//public class CardUI : MonoBehaviour
//{
//    public Image backgroundImage;
//    public Image iconImage;
//    public TMP_Text nameText;
//    public TMP_Text descriptionText;
//    public TMP_Text costText;

//    public void SetCard(CardData data)
//    {
//        nameText.text = data.cardName;
//        descriptionText.text = data.description;
//        costText.text = data.cost.ToString();
//        iconImage.sprite = data.artwork;

// 카드 색상 (카테고리에 따라 색상 변경)
//backgroundImage.color = data.category switch
//{
//    CardCategory.Attack => Color.red,
//    CardCategory.Defense => Color.green,
//    CardCategory.Utility => Color.cyan,
//    _ => Color.white,
//};
//    }
//}

//public class CardManager : MonoBehaviour
//{
//    public CardData[] cardDataList; // 카드 데이터들
//    public GameObject cardPrefab;   // 카드 UI 프리팹
//    public Transform cardParent;    // 카드 생성 위치

//    void Start()
//    {
//        foreach (CardData data in cardDataList)
//        {
//            GameObject cardGO = Instantiate(cardPrefab, cardParent);
//            cardGO.GetComponent<CardUI>().SetCard(data);
//        }
//    }
//}
//public enum CardCategory { Attack, Defense, Utility } //카드 카테고리, 공, 방, 유 구분
//public enum TargetType { Self, Enemy, RangeAttack } //타겟 자기 자신, 적, 범위
//public enum ValueType { Flat, Percentage, StatusEffect }
////벨류. 깡딜, 체력퍼뎀(이건 최대 체력 말고 현제 채력도 필요할 거 같음), 버프 or 디버프

//[CreateAssetMenu(menuName = "Card")]
//public class CardData : ScriptableObject
//{
//    //카드 이름, 설명 문구, 이펙트 아트워크
//    public string cardName;
//    public string description;
//    public Sprite artwork; //이펙트

//    //코스트, 카드 종류, 공격범위
//    public int cost;
//    public CardCategory category;
//    public TargetType targetType;

//    //벨류타입. 카드 효과 수치, 유틸 종류(ex: n턴 버프, n턴 디버프 등)
//    public ValueType valueType;
//    public float amount;
//    public string statusEffectID;
//}

//public void Execute(State target)
//{
//    switch (category)
//    {

//        case CardCategory.Attack:
//            ApplyAttack(target);
//            break;


//        case CardCategory.Defense:
//            ApplyDefense(target);
//            break;

//        case CardCategory.Utility:
//            ApplyUtility(target);
//            break;
//    }
//}
//}




//      public class State : MonoBehaviour
//{
//    public int armor;

//    // 임시 방어도 정보
//    private int tempArmor;
//    private int tempArmorTurns;

//    public void ApplyTemporaryArmor(int amount, int duration)
//    {
//        tempArmor += amount;
//        armor += amount;
//        tempArmorTurns = Mathf.Max(tempArmorTurns, duration); // 덮어쓰기
//    }

//    public void OnTurnEnd()
//    {
//        if (tempArmorTurns > 0)
//        {
//            tempArmorTurns--;

//            if (tempArmorTurns == 0)
//            {
//                armor -= tempArmor;
//                tempArmor = 0;
//            }
//        }
//    }
//}

//using UnityEngine;

//public enum CardCategory { Attack, Defense, Utility }      // 카드 유형
//public enum TargetType { Self, SingleEnemy, AreaEnemies }  // 시전 대상
//public enum ValueType { Flat, Percentage, StatusEffect }   // 효과 계산 방식

//[CreateAssetMenu(menuName = "CardSystem/Card")]
//public class CardData : ScriptableObject
//{
//    // 기본 정보
//    public string cardName;
//    public string description;
//    public Sprite artwork;

//    // 카테고리, 시전 대상, 코스트
//    public CardCategory category;
//    public TargetType targetType;
//    public int cost;

//    // 효과 타입 및 수치
//    public ValueType valueType;
//    public float amount;             // Flat이나 퍼센트 수치
//    public string statusEffectID;    // 상태이상 이름 (ex. "Burn", "Poison")

//    // 방어도 관련 (Defense 카드일 경우)
//    public int armorDuration = 3; // 방어 지속 턴 수

//    /// <summary>
//    /// 카드 효과 실행 (외부에서 호출)
//    /// </summary>
//    public void Execute(State target)
//    {
//        switch (category)
//        {
//            case CardCategory.Attack:
//                ApplyAttack(target);
//                break;

//            case CardCategory.Defense:
//                ApplyDefense(target);
//                break;

//            case CardCategory.Utility:
//                ApplyUtility(target);
//                break;
//        }
//    }

//    void ApplyAttack(State target)
//    {
//        int damage = 0;

//        if (valueType == ValueType.Flat)
//            damage += (int)amount;

//        else if (valueType == ValueType.Percentage)
//            damage += (int)(target.currentHealth * (amount / 100f));

//        target.TakeDamage(damage);
//    }

//    void ApplyDefense(State target)
//    {
//        if (valueType == ValueType.Flat)
//        {
//            int armorAmount = (int)amount;
//            target.ApplyTemporaryArmor(armorAmount, armorDuration);
//        }
//    }

//    void ApplyUtility(State target)
//    {
//        if (valueType == ValueType.StatusEffect)
//        {
//            target.ApplyStatusEffect(statusEffectID); // 상태이상 시스템이 있다면 연동
//        }
//        else if (valueType == ValueType.Flat)
//        {
//            target.Heal((int)amount);
//        }
//    }
//}
