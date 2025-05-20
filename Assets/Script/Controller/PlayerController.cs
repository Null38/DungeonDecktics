using Astar;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;


[RequireComponent(typeof(InfoComponent))]
public class PlayerController : Controller, ITurnBased
{
    private InfoComponent info;
    private bool hasMove = true;
    private Vector3? target = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        DataManager.player = this;
        info = GetComponent<InfoComponent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GetPath(worldPos);
            if (path.Count != 0)
                target = (Vector2)path.First.Value;
            hasMove = true;
        }
    }

    public override void NextStep()
    {
        try
        {
            path.RemoveFirst();
            target = (Vector2)path.First.Value;
        }
        catch (System.Exception)
        {
            target = null;
        }
        OnTurnEnd();
    }

    public void OnTurnBegin()
    {
        hasMove = false;
    }

    public void OnTurnEnd()
    {
        GameManager.Instance.EntityActionComplete(gameObject.GetInstanceID());
    }
    public override Vector3? TargetPos
    {
        get
        {
            if (!GameManager.Instance.isPlayerTurn)
            {
                return null;
            }

            return target;
        }
    }
}
