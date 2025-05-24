using UnityEngine;

[CreateAssetMenu(fileName = "new Simple Attack", menuName = "Scriptable Objects/Cards/Simple Attack", order = 1)]
public class SimpleAttack : CardObjectBase
{

    public int[] damage;

    public override string FormatDescription()
    {
        return string.Format(description, damage);
    }

    public override string Upgrad()
    {
        throw new System.NotImplementedException();
    }

    public override void UseCard(Controller user)
    {
        throw new System.NotImplementedException();
        isUpgraded = true;
    }
}
