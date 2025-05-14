using UnityEngine;

public class State : MonoBehaviour
{
    public int CurrentHealth { get; private set; } = 100;
    public int MaxHealth => 100;
    public int armor;

    private int tempArmor;
    private int tempArmorTurns;

    public void TakeDamage(int amount)
    {
        int effectiveDamage = Mathf.Max(0, amount - armor);
        CurrentHealth = Mathf.Max(0, CurrentHealth - effectiveDamage);
    }

    public void Heal(int amount)
    {
        CurrentHealth = Mathf.Min(MaxHealth, CurrentHealth + amount);
    }

    public void ApplyTemporaryArmor(int amount, int duration)
    {
        tempArmor += amount;
        armor += amount;
        tempArmorTurns = Mathf.Max(tempArmorTurns, duration);
    }

    public void OnTurnEnd()
    {
        if (tempArmorTurns > 0)
        {
            tempArmorTurns--;
            if (tempArmorTurns == 0)
            {
                armor -= tempArmor;
                tempArmor = 0;
            }
        }
    }
}
