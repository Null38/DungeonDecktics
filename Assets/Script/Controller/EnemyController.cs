using System.Collections.Generic;
using UnityEngine;
using Astar;


public class EnemyController : Controller, ITurnBased
{
    public static Dictionary<Controller, ITurnBased> ActiveEnemy = new();
       
    private Vector3? target;
    
    private bool hasActed;

    // 공격 시 사용할 데미지(로그 출력용)
    [SerializeField] private int attackDamage = 1;

    void OnEnable() => ActiveEnemy[this] = this;
    void OnDisable() => ActiveEnemy.Remove(this);

    public void OnTurnBegin()
    {
        hasActed = false;
        target = null;
        path.Clear();  
    }

    void Update()
    {
        if (!hasActed)
        {
            Act();
            hasActed = true;
        }
    }

    /// <summary>
    /// 이 턴에 수행할 행동:
    /// 1) 플레이어와 거리가 1 이내면 공격 로그 출력 → 턴 종료  
    /// 2) 아니면 플레이어 주변 이동 목표 칸을 찾아 경로 생성 → 첫 노드만 target으로 설정  
    /// </summary>
    private void Act()
    {
        // 내 위치와 플레이어 위치를 그리드 좌표로 변환
        Vector2Int myPos = new Vector2Int(
            Mathf.RoundToInt(transform.position.x),
            Mathf.RoundToInt(transform.position.y)
        );
        Vector3 playerWorld = DataManager.player.transform.position;
        Vector2Int playerPos = new Vector2Int(
            Mathf.RoundToInt(playerWorld.x),
            Mathf.RoundToInt(playerWorld.y)
        );

        int dx = playerPos.x - myPos.x;
        int dy = playerPos.y - myPos.y;

        // 대각선 포함 8방향 인접 체크
        if (Mathf.Abs(dx) <= 1 && Mathf.Abs(dy) <= 1)
        {
            Debug.Log($"[EnemyController] {name}이(가) 플레이어에게 {attackDamage} 데미지 공격!");
            OnTurnEnd();
            return;
        }

        // 인접 8칸 중 가장 가까운 통과 가능한 칸을 골라 goal로 설정
        var adjacent = new List<Vector2Int>(8);
        for (int ix = -1; ix <= 1; ix++)
            for (int iy = -1; iy <= 1; iy++)
                if (ix != 0 || iy != 0)
                    adjacent.Add(playerPos + new Vector2Int(ix, iy));

        adjacent.Sort((a, b) =>
            (a - myPos).sqrMagnitude.CompareTo((b - myPos).sqrMagnitude)
        );

        Vector2Int goal = playerPos;
        bool found = false;
        foreach (var cell in adjacent)
        {
            if (IsPassable(cell))
            {
                goal = cell;
                found = true;
                break;
            }
        }
        if (!found)
        {
            Debug.LogWarning($"[EnemyController] {name} 주변 8칸 모두 막힘 → 턴 종료");
            OnTurnEnd();
            return;
        }

        // goal로 A* 경로 생성
        Vector3 goalWorld = new Vector3(goal.x, goal.y, 0f);
        GetPath(goalWorld);

        //Debug.Log($"[EnemyController] {name} ▶ 목표 인접칸={goal}, 경로 노드 수={path.Count}");

        if (path.Count > 0)
        {
            var next = path.First.Value;
            target = new Vector3(next.x, next.y, 0f);
            //Debug.Log($"[EnemyController] {name} 이동 목표 → {target.Value}");
        }
        else
        {
            Debug.Log($"[EnemyController] {name} 경로 없음 → 턴 종료");
            OnTurnEnd();
        }
    }

    public override Vector3? TargetPos
    {
        get
        {
            if (GameManager.Instance == null || GameManager.Instance.IsPlayerTurn)
                return null;
            return target;
        }
    }

    public override void NextStep()
    {
        // 한 턴에 한 칸만 이동 → 바로 턴 종료
        path.Clear();
        target = null;
        OnTurnEnd();
    }

    public void OnTurnEnd()
    {
        GameManager.Instance.EntityActionComplete(this);
    }
}
