using System.Runtime.InteropServices;
using UnityEngine;

[CreateAssetMenu(fileName = "new Buff Card", menuName = "Scriptable Objects/Cards/Buff Card", order = 1)]
public class BuffCard : CardBase
{
    //구현 하나도 안했고 그냥 형태 잡기 위해 만든겁니다.

    public override string FormatDescription()
    {
        throw new System.NotImplementedException();
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
