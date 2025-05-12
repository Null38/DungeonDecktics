using UnityEngine;

public class CardExecutor : MonoBehaviour
{
    private CardDataHandler cardDataHandler; // �ùٸ� Ŭ���� ���
    private CardData card;

    void Start()
    {
        cardDataHandler = GetComponent<CardDataHandler>();

        // JSON���� ī�� �����͸� �ҷ�����
        card = cardDataHandler.LoadCardFromJson();
        if (card != null)
        {
            Debug.Log($"���� ���� �� {card.CardName} ī�� �ε� �Ϸ�!");
        }
    }

    public void ExecuteCard(State target)
    {
        if (card == null)
        {
            Debug.LogError("ī�� �����Ͱ� �����ϴ�!");
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
        Debug.Log($"[Attack] {card.CardName} �� ��󿡰� {damage} ���� (����: {card.range})");

        if (card.lifeStealPercentage > 0)
        {
            int healAmount = (int)(damage * (card.lifeStealPercentage / 100f));
            target.Heal(healAmount);
            Debug.Log($"[LifeSteal] {card.CardName} �� {healAmount} ü�� ���");
        }
    }

    void ApplyDefense(CardData card, State target)
    {
        int armorAmount = (int)card.amount;
        int duration = 3; // �⺻ ��� ���� ��
        target.ApplyTemporaryArmor(armorAmount, duration);
        Debug.Log($"[Armor] {card.CardName} �� {armorAmount} �� ���� (3��)");
    }

    void ApplyUtility(CardData card, State target)
    {
        if (card.valueType == ValueType.StatusEffect)
        {
            Debug.Log($"[Utility] �����̻� '{card.statusEffectID}' ����");
            // ���߿� �����̻� �ý��� ����
        }
        else if (card.valueType == ValueType.Flat)
        {
            target.Heal((int)card.amount);
            Debug.Log($"[Utility] {card.CardName} �� ü�� {(int)card.amount} ȸ��");
        }
    }
}