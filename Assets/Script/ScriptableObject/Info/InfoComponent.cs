using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class InfoComponent : MonoBehaviour
{
    [SerializeField]
    BaseInfo Info;//Info를 수정하지 마세요 info기반으로 작동해야합니다.

    private BaseInfo info;

    [HideInInspector]
    public List<CardObjectBase> Deck = new List<CardObjectBase>();


    public void Initialize()
    {
        Deck.Clear();

        foreach (var card in Info.defaultDeck)
        {
            Deck.Add(Instantiate(card));
        }

        info = Instantiate(Info);//info값은 변동될것이기에 참조한것이 수정되지 않게 하기위해 복제를 하고 있습니다.
    }

    void TakeDamage(int damage)
    {
        int remainingDamage = damage;

        // 회피율(evasion) 적용
        float roll = Random.value;
        if (roll < info.evasion)
        {
            Debug.Log($"[회피] 공격을 회피했습니다! (회피 확률: {info.evasion * 100:F1}%)");
            return;
        }

        // 1. 쉴드 먼저 깎기
        if (info.currentShield > 0)
        {
            int shieldDamage = Mathf.Min(info.currentShield, remainingDamage);
            info.currentShield -= shieldDamage;
            remainingDamage -= shieldDamage;
        }

        // 2. 남은 데미지를 HP에 적용
        if (remainingDamage > 0)
        {
            info.currentHp = Mathf.Max(info.currentHp - remainingDamage, 0);
            // Debug.Log($"{remainingDamage}의 피해를 입었습니다. (남은 체력: {info.currentHp})");

            if (info.currentHp <= 0)
            {
                Die();
            }
        }
    }

    public bool IsDead => info.currentHp <= 0;

    public void TakeHeal(int heal)
    {
        info.currentHp = Mathf.Min(info.currentHp + heal, info.MaxHp);
    }

    // 죽음 처리 메서드
    protected virtual void Die()
    {
        Debug.Log("이(가) 죽었습니다.");
    }

    // Cost 조정 메서드
    public bool UseCost(int cost)
    {
        if (info.currentCost < cost)
            return false;

        info.currentCost -= cost;

        return true;
    }

    public void RestoreCost()
    {
        info.currentCost = info.MaxCost;
    }
}
