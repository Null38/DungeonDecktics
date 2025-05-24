using System.Runtime.InteropServices;
using UnityEngine;

[CreateAssetMenu(fileName = "new Simple Attack", menuName = "Scriptable Objects/Cards/Simple Attack", order = 1)]
public class SimpleAttack : CardObjectBase
{
    [Tooltip("이 배열 크기를 초과하게 설명창에 넣지 마세요.\n 오류납니다.")]
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
