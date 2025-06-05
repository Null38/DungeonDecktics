using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class InfoComponent : MonoBehaviour
{
    [SerializeField]
    BaseInfo Real;
    BaseInfo info;

    public static event Action OnPlayerDied;
    public static event Action<EnemyController> OnEnemyDied;


    public int MaxHp
    {
        get => info.MaxHp;
        set => info.MaxHp = value;
    }

    public int currentHp
    {
        get => info.currentHp;
    }

    public int MaxCost
    {
        get => info.MaxCost;
    }

    public int currentCost
    {
        get => info.currentCost;
    }

    public int currentShield
    {
        get => info.currentShield;
        set => info.currentShield = value;
    }

    [HideInInspector]
    public List<CardBase> Deck = new List<CardBase>();

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetInfo()
    {
        info = Instantiate(Real);
    }

    public void dumb()
    {
        info.MaxHp += DumbAssSave.HP;
        info.MaxCost += DumbAssSave.COST;
    }

    public void Initialize()
    {
        info.currentHp = info.MaxHp;
        info.currentShield = 0;
        info.currentCost = info.MaxCost;
    }


    public void InitCost()
    {
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

            if (info.currentHp > 0 && animator != null)
            {
                animator.SetTrigger("Hit");
            }

            if (info.currentHp <= 0)
            {
                if (animator != null)
                {
                    animator.SetTrigger("Die");
                }
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
        var playerCtrl = GetComponent<PlayerController>();
        if (playerCtrl != null)
        {           
            OnPlayerDied?.Invoke();            
            return;
        }
        // 적 죽음 처리
        var enemyCtrl = GetComponent<EnemyController>();
        if (enemyCtrl != null)
        {
            OnEnemyDied?.Invoke(enemyCtrl);
            Debug.Log($"[InfoComponent] 적({gameObject.name}) 사망 이벤트 발생");
            Destroy(gameObject);
            return;
        }
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
