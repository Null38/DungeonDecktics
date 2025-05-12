using UnityEngine;

public class CardExecutor : MonoBehaviour
{
    private CardDataHandler cardDataHandler; // 올바른 클래스 사용
    private CardData card;

    void Start()
    {
        cardDataHandler = GetComponent<CardDataHandler>();

        // JSON에서 카드 데이터를 불러오기
        card = cardDataHandler.LoadCardFromJson();
        if (card != null)
        {
            Debug.Log($"게임 시작 시 {card.CardName} 카드 로드 완료!");
        }
    }

    public void ExecuteCard(State target)
    {
        if (card == null)
        {
            Debug.LogError("카드 데이터가 없습니다!");
            return;
        }

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
        Debug.Log($"[Attack] {card.CardName} → 대상에게 {damage} 피해 (범위: {card.range})");

        if (card.lifeStealPercentage > 0)
        {
            int healAmount = (int)(damage * (card.lifeStealPercentage / 100f));
            target.Heal(healAmount);
            Debug.Log($"[LifeSteal] {card.CardName} → {healAmount} 체력 흡수");
        }
    }

    void ApplyDefense(CardData card, State target)
    {
        int armorAmount = (int)card.amount;
        int duration = 3; // 기본 방어 지속 턴
        target.ApplyTemporaryArmor(armorAmount, duration);
        Debug.Log($"[Armor] {card.CardName} → {armorAmount} 방어도 적용 (3턴)");
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
            Debug.Log($"[Utility] {card.CardName} → 체력 {(int)card.amount} 회복");
        }
    }
}