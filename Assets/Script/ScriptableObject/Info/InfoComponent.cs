using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class InfoComponent : MonoBehaviour
{
    [SerializeField]
    BaseInfo info;

    public int currentHp => info.currentHp;

    [HideInInspector]
    public List<CardBase> Deck = new List<CardBase>();


    public void Initialize()
    {
        Deck.Clear();
        info.currentHp = info.MaxHp;
        info.currentShield = 0;
        info.currentCost = info.MaxCost;
    }

     public void TakeDamage(int damage)
    {
        int remainingDamage = damage;
                
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
        Debug.Log("'player'이(가) 죽었습니다.");
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
