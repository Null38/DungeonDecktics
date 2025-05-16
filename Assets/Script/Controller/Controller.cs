using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

abstract public class Controller : MonoBehaviour
{
    [SerializeField]
    protected List<Vector2Int> path = new();

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
