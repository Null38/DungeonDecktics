using UnityEngine;

[CreateAssetMenu(fileName = "ArmorCardObject", menuName = "Scriptable Objects/Card/ArmorCard")]
public class ArmorCardObject : CardObject
{
    public int armor;

    public override void UseCard()
    {
        throw new System.NotImplementedException();
    }

    public override string FormatDescription()
    {
        return string.Format(description, armor);
    }
}
