using System.Runtime.InteropServices;
using UnityEngine;

[CreateAssetMenu(fileName = "new Simple Attack", menuName = "Scriptable Objects/Cards/Simple Attack", order = 1)]
public class SimpleAttack : CardBase
{
    [Tooltip("이 배열 크기를 초과하게 설명창에 넣지 마세요.\n오류납니다.\n단, 마지막에는 데미지 합산이 들어가니 {배열길이}까지 가능합니다.")]
    public int[] damages;

    public override string FormatDescription()
    {
        int total = 0;

        object[] formatArray = new object[damages.Length + 1];

        for (int i = 0; i < damages.Length; i++)
        {
            formatArray[i] = damages[i];
            total += damages[i];
        }
        formatArray[damages.Length] = total;

        return string.Format(description, formatArray);
    }

    public override string Upgrad()
    {
        throw new System.NotImplementedException();
        isUpgraded = true;
    }

    protected override void RunCard(Controller target)
    {

        foreach (var damage in damages)
        {
            target.info.currentHp -= damage;
        }
    }
}
