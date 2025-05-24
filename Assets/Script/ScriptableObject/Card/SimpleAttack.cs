using System.Runtime.InteropServices;
using UnityEngine;

[CreateAssetMenu(fileName = "new Simple Attack", menuName = "Scriptable Objects/Cards/Simple Attack", order = 1)]
public class SimpleAttack : CardObjectBase
{
    [Tooltip("이 배열 크기를 초과하게 설명창에 넣지 마세요.\n오류납니다.\n단, 마지막에는 데미지 합산이 들어가니 {배열길이}까지 가능합니다.")]
    public int[] damage;

    public override string FormatDescription()
    {
        int total = 0;

        for (int i = 0; i < damage.Length; i++)
        {
            total += damage[i];
        }

        return string.Format(description, damage, total);
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
