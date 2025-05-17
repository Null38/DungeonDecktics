using Astar;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Controller, ITurnBased
{
    public static Dictionary<int, ITurnBased> ActiveEnemy = new(); // 적 오브젝트 || 이거 적 클래스로 옮겨야겠다.

    private bool hasActed = false;

    void Update()
    {
        if (hasActed)
        {
            GetPath(transform.position + new Vector3(Random.Range(-1,2), Random.Range(-1, 2)));
        }
    }

    protected override void GetPath(Vector3 targetPos)
    {
        if (path != null && path.Count != 0 || !hasActed)
            return;

        Vector2Int start = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        Vector2Int end = new Vector2Int(Mathf.RoundToInt(targetPos.x), Mathf.RoundToInt(targetPos.y));
        path = PathFinder.FindPath(start, end);
        if (path != null && path.Count != 0)
            path.RemoveAt(0);
    }

    private void OnEnable()
    {
        ActiveEnemy.Add(gameObject.GetInstanceID(), this);
    }

    private void OnDisable()
    {
        ActiveEnemy.Remove(gameObject.GetInstanceID());

    }

    public void OnTurnBegin()
    {
        hasActed = true;
    }

    void ITurnBased.OnTurnEnd()
    {
        GameManager.Instance.EntityActionComplete(gameObject.GetInstanceID());
        throw new System.NotImplementedException();
    }
}
