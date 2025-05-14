using UnityEngine;

[CreateAssetMenu(fileName = "AttackCardObject", menuName = "ScriptableObjects/Card/AttackCard")]
public class AttackCardObject : CardObject
{

    public int damage;

    public override void UseCard()
    {
        throw new System.NotImplementedException();
    }

    public override string FormatDescription()
    {
        return string.Format(description, damage);
    }
}
