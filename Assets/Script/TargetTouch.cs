using UnityEngine;
using UnityEngine.EventSystems;

public class TargetTouch : MonoBehaviour
{
    public Controller target;

    public void OnMouseDown()
    {
        var selected = GameManager.Instance.selectCard;

        if (!selected.cardInfo.UseCard(target))
            return;
                
        if (selected != null && selected.cardInfo.targetType == CardBase.TargetType.other)
        {
            var playerCtrl = DataManager.player.GetComponent<PlayerController>();
            if (playerCtrl != null)
                playerCtrl.PlayAttackAnimation();

            var enemyAnim = target.GetComponent<Animator>();
            if (enemyAnim != null)
            {
                enemyAnim.SetTrigger("Hit");
            }
            if (playerCtrl != null)
                playerCtrl.CancelMove();
        }
        
        GameManager.Instance.RemoveAllTarget();
        GameManager.Instance.cardPile.HandToDiscardPile(GameManager.Instance.selectCard);
    }
}

