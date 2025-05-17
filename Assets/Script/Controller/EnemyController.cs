using Astar;
using UnityEngine;

public class EnemyController : Controller
{
    public override bool IsActive => !GameManager.Instance.isPlayerTurn;

    private bool active = false;

    static int count = 0;


    void Update()
    {
        if (IsActive)
        {
            GetPath(transform.position + new Vector3(Random.Range(-1,2), Random.Range(-1, 2)));
        }
    }

    protected override void GetPath(Vector3 targetPos)
    {
        if (path != null && path.Count != 0 || !active)
            return;

        Vector2Int start = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        Vector2Int end = new Vector2Int(Mathf.RoundToInt(targetPos.x), Mathf.RoundToInt(targetPos.y));
        path = PathFinder.FindPath(start, end);
        if (path != null && path.Count != 0)
            path.RemoveAt(0);
    }

    private void OnEnable()
    {
        GameManager.EnemyTurnEvent += StartTrun;
    }

    private void OnDisable()
    {
        GameManager.EnemyTurnEvent -= StartTrun;

    }

    private void StartTrun()
    {
        count++;
        active = true;
    }


    public override void Next()
    {
        path.RemoveAt(0);

        count--;
        active = false;
        Debug.Log(count);
        if (count <= 0)
        {
            GameManager.Instance.EndTurn();
        }
    }
}
