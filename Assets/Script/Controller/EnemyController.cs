using Astar;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(InfoComponent))]
public class EnemyController : Controller, ITurnBased
{
    public static Dictionary<Controller, ITurnBased> ActiveEnemy = new(); 

    private bool hasActed = true;

    void Update()
    {
        if (!hasActed)
        {
            Act();
            hasActed = true;
        }
    }

    private void Act()
    {
        if (path.Count == 0)
        {
            GetPath(transform.position + new Vector3(Random.Range(-1, 2), Random.Range(-1, 2)));
        }

        if (path.Count == 0)
        {
            OnTurnEnd();
        }
    }

    public override void NextStep()
    {
        path.RemoveFirst();
        OnTurnEnd();
    }

    private void OnEnable()
    {
        ActiveEnemy.Add(this, this);
    }

    private void OnDisable()
    {
        ActiveEnemy.Remove(this);

    }

    public void OnTurnBegin()
    {
        hasActed = false;
    }

    public void OnTurnEnd()
    {
        GameManager.Instance.EntityActionComplete(this);
    }

    public override Vector3? TargetPos
    {
        get
        {
            // GameManager가 준비되지 않았거나, 플레이어 턴이면 이동 금지
            if (GameManager.Instance == null || GameManager.Instance.IsPlayerTurn)
                return null;

            return base.TargetPos;
        }
    }

}
