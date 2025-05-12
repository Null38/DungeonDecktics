using UnityEngine;

[CreateAssetMenu(fileName = "AttackCardObject", menuName = "Scriptable Objects/Card/AttackCard")]
public class AttackCardObject : CardObject
{
    int damage;

    public override void UseCard()
    {
        throw new System.NotImplementedException();
    }

    public override string FormatDescription()
    {
        throw new System.NotImplementedException();
    }
}
