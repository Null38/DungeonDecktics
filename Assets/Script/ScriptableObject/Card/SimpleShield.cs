using UnityEngine;

[CreateAssetMenu(fileName = "new Simple Shield", menuName = "Scriptable Objects/Cards/Simple Shield", order = 1)]
public class SimpleShield : CardBase
{
    public int shield;

    public override string FormatDescription()
    {
        return string.Format(description, shield);
    }

    public override string Upgrad()
    {
        throw new System.NotImplementedException();
        isUpgraded = true;
    }

    public override void RunCard(Controller user)
    {
        user.info.currentShield += shield;
    }
}
