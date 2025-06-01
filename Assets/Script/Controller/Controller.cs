using Astar;
using System.Collections.Generic;
using UnityEngine;

abstract public class Controller : MonoBehaviour
{
    [SerializeField]
    protected LinkedList<Vector2Int> path = new();

    [SerializeField]
    public BaseInfo info;
    private List<Vector2Int> ImpossiblePath = new();

    protected virtual void GetPath(Vector3 targetPos)
    {
        if (GameManager.Instance.IsSameTargetPosition(this, targetPos))
        {
            return;
        }

        Vector2Int start = new(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        Vector2Int end = new(Mathf.RoundToInt(targetPos.x), Mathf.RoundToInt(targetPos.y));

        List<Vector2Int> newPath;
        ImpossiblePath.Clear();

        do
        {
            path.Clear();

            newPath = PathFinder.FindPath(start, end, PathFinder.GetCost, (pos) => IsPassable(pos) && !ImpossiblePath.Contains(pos));
            if (newPath == null || newPath.Count == 0)
                return;

            newPath.RemoveAt(0);


            if (newPath.Count > 0 && GameManager.Instance.IsSameTargetPosition(this, (Vector2)newPath[0]))
            {
                ImpossiblePath.Add(newPath[0]);
            }

        } while (newPath.Count > 0 && GameManager.Instance.IsSameTargetPosition(this, (Vector2)newPath[0]));


        foreach (var path in newPath)
        {
            this.path.AddLast(path);
        }
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
