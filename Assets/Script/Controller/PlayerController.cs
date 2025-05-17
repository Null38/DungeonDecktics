using Astar;
using UnityEngine;
using UnityEngine.UIElements;


[RequireComponent(typeof(InfoComponent))]
public class PlayerController : Controller, ITurnBased
{
    private InfoComponent info;

    bool IsActive => GameManager.Instance.isPlayerTurn;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        info = GetComponent<InfoComponent>();
    }

    void Update()
    {
        if (IsActive && Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GetPath(worldPos);
        }
    }

    void ITurnBased.OnTurnBegin()
    {
        throw new System.NotImplementedException();
    }

    void ITurnBased.OnTurnEnd()
    {
        throw new System.NotImplementedException();
    }
}
