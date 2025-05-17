using Astar;
using UnityEngine;
using UnityEngine.UIElements;


[RequireComponent(typeof(InfoComponent))]
public class PlayerController : Controller, ITurnBased
{
    private InfoComponent info;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
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
        }
    }

    public void OnTurnBegin()
    {
        throw new System.NotImplementedException();
    }

    void ITurnBased.OnTurnEnd()
    {
        throw new System.NotImplementedException();
    }
}
