using UnityEngine;

[CreateAssetMenu(fileName = "UtilityCardObject", menuName = "ScriptableObjects/Card/UtilityCard")]
public class UtilityCardObject : CardObject
{
    public int heal;
    

    public override void UseCard()
    {
        throw new System.NotImplementedException();
    }

    public override string FormatDescription()
    {
        return string.Format(description, heal);
    }
}
