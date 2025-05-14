using UnityEngine;
using static UnityEditor.PlayerSettings;

public class InfoComponent : MonoBehaviour
{
    BaseInfo info;


    void TakeDamage(int damage)
    {
        int remainingDamage = damage;

        // ȸ����(evasion) ����
        float roll = Random.value;
        if (roll < info.evasion)
        {
            Debug.Log($"[ȸ��] ������ ȸ���߽��ϴ�! (ȸ�� Ȯ��: {info.evasion * 100:F1}%)");
            return;
        }

        // 1. ���� ���� ���
        if (info.currentShield > 0)
        {
            int shieldDamage = Mathf.Min(info.currentShield, remainingDamage);
            info.currentShield -= shieldDamage;
            remainingDamage -= shieldDamage;
        }

        // 2. ���� �������� HP�� ����
        if (remainingDamage > 0)
        {
            info.currentHp = Mathf.Max(info.currentHp - remainingDamage, 0);
            // Debug.Log($"{remainingDamage}�� ���ظ� �Ծ����ϴ�. (���� ü��: {info.currentHp})");

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

    // ���� ó�� �޼���
    protected virtual void Die()
    {
        Debug.Log("��(��) �׾����ϴ�.");
    }

    // Cost ���� �޼���
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
