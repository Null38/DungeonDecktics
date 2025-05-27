using System.Collections.Generic;
using UnityEngine;
using Astar;


public class EnemyController : Controller, ITurnBased
{
    public static Dictionary<Controller, ITurnBased> ActiveEnemy = new();

    
    private Vector3? target;
    
    private bool hasActed;

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

        // 플레이어 주변 8방향 생성
        var adjacent = new List<Vector2Int>(8);
        for (int dx = -1; dx <= 1; dx++)
            for (int dy = -1; dy <= 1; dy++)
                if (dx != 0 || dy != 0)
                    adjacent.Add(playerPos + new Vector2Int(dx, dy));

        // 내 위치와의 제곱거리 기준 정렬
        adjacent.Sort((a, b) =>
            (a - myPos).sqrMagnitude.CompareTo((b - myPos).sqrMagnitude)
        );

        // 통과 가능한 첫 칸을 goal로 선택
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
            Debug.LogWarning($"[EnemyController] {name} 주변 8칸 모두 막혀서 이동 불가 → 턴 종료");
            OnTurnEnd();
            return;
        }

        // 선택된 goal 칸으로 A* 경로 생성
        Vector3 goalWorld = new Vector3(goal.x, goal.y, 0f);
        GetPath(goalWorld);
        
        if (path.Count > 0)
        {
            // 첫 노드를 target으로 설정
            var next = path.First.Value;
            target = new Vector3(next.x, next.y, 0f);
            Debug.Log($"[EnemyController] {name} 이동 목표 → {target.Value}");
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
        if (path.Count > 0)
        {
            path.RemoveFirst();
            if (path.Count > 0)
            {
                var next = path.First.Value;
                target = new Vector3(next.x, next.y, 0f);
            }
            else
            {
                target = null;
                OnTurnEnd();
            }
        }
        else
        {
            OnTurnEnd();
        }
    }

    public void OnTurnEnd()
    {
        GameManager.Instance.EntityActionComplete(this);
    }
}
