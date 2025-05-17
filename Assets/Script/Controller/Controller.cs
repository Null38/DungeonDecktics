using Astar;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

abstract public class Controller : MonoBehaviour
{
    [SerializeField]
    protected List<Vector2Int> path = new();
    public abstract bool IsActive { get; }

    protected virtual void GetPath(Vector3 targetPos)
    {
        if (path != null && path.Count != 0)
            return;

        Vector2Int start = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        Vector2Int end = new Vector2Int(Mathf.RoundToInt(targetPos.x), Mathf.RoundToInt(targetPos.y));
        path = PathFinder.FindPath(start, end);
        if (path != null && path.Count != 0)
            path.RemoveAt(0);
    }

    public virtual Vector3? TargetPos 
    {
        get
        {
            if (path == null || path.Count == 0)
                return null;

            return (Vector2)path[0];
        }
    }

    public abstract void Next();
}
