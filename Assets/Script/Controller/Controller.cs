using Astar;
using System.Collections.Generic;
using UnityEngine;

abstract public class Controller : MonoBehaviour
{
    [SerializeField]
    protected LinkedList<Vector2Int> path = new LinkedList<Vector2Int>();

    [SerializeField]
    protected BaseInfo info;

    protected virtual void GetPath(Vector3 targetPos)
    {
        // 1) 동일 타겟 중복 요청 방지
        if (GameManager.Instance.IsSameTargetPosition(this, targetPos))
            return;

        // 2) 월드 → 그리드 정수 좌표 변환
        Vector2Int start = new Vector2Int(
            Mathf.RoundToInt(transform.position.x),
            Mathf.RoundToInt(transform.position.y)
        );
        Vector2Int end = new Vector2Int(
            Mathf.RoundToInt(targetPos.x),
            Mathf.RoundToInt(targetPos.y)
        );

        // 3) 경로 탐색
        List<Vector2Int> newPath = PathFinder.FindPath(start, end, PathFinder.GetCost, IsPassable);

        // 4) 반환값 검사
        if (newPath == null || newPath.Count == 0)
        {
            Debug.LogWarning($"[Controller] {gameObject.name} 경로 없음 (start={start}, end={end})");
            return;
        }
        
        // 5) 첫 노드는 현재 위치(중복) 이므로 제거
        newPath.RemoveAt(0);

        // 6) 이전 타겟과 같은 노드는 모두 제거
        while (newPath.Count > 0
            && GameManager.Instance.IsSameTargetPosition(this, (Vector2)newPath[0]))
        {           
            newPath.RemoveAt(0);
        }

        // 7) 필드 path에 최종 경로 설정
        path.Clear();
        foreach (var node in newPath)
            path.AddLast(node);
    }

    public abstract void NextStep();

    public virtual Vector3? TargetPos
    {
        get
        {
            if (path.First == null)
                return null;
            return (Vector2)path.First.Value;
        }
    }

    public virtual bool IsPassable(Vector2Int position)
    {
        Collider2D hit = Physics2D.OverlapPoint(position, DataManager.UnPassableLayer);
        if (hit == null || hit.gameObject == this)
            return true;

        Controller cont = hit.gameObject.GetComponent<Controller>();
        if (cont != null && (cont == this || cont.TargetPos.HasValue))
            return true;

        return false;
    }

    public void Stop()
    {
        path.Clear();
    }
}
