using UnityEngine;

[CreateAssetMenu(fileName = "AttackCardObject", menuName = "Scriptable Objects/Card/AttackCard")]
public class AttackCardObject : CardObject
{

    public int damage;
    public int test;
    public int var;
    public int ul;

    public override void UseCard()
    {
        throw new System.NotImplementedException();
    }

    public override string FormatDescription()
    {
        return string.Format(description, damage, test, var, ul);
    }
}
