using Astar;
using UnityEngine;
using UnityEngine.UIElements;


[RequireComponent(typeof(InfoComponent))]
public class PlayerController : Controller
{
    private InfoComponent info;

    public override bool IsActive => GameManager.Instance.isPlayerTurn;

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

    public override void Next()
    {
        path.RemoveAt(0);
        GameManager.Instance.EndTurn();
    }
}
