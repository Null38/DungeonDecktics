using Astar;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

abstract public class Controller : MonoBehaviour
{
    [SerializeField]
    protected LinkedList<Vector2Int> path = new();


    protected virtual void GetPath(Vector3 targetPos)
    {
        path.Clear();

        Vector2Int start = new(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        Vector2Int end = new(Mathf.RoundToInt(targetPos.x), Mathf.RoundToInt(targetPos.y));

        var newPath = PathFinder.FindPath(start, end);
        if (newPath == null || newPath.Count == 0)
            return;

        newPath.RemoveAt(0);
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
            {
                return null;
            }


            return (Vector2)path.First.Value;
        }
    }


}
