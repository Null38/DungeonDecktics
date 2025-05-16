using Astar;
using UnityEngine;
using UnityEngine.UIElements;


[RequireComponent(typeof(InfoComponent))]
public class PlayerController : Controller
{
    private InfoComponent info;
    [SerializeField]


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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

    public void GetPath(Vector3 targetPos)
    {
        if (path != null && path.Count != 0)
            return;

        Vector2Int start = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        Vector2Int end = new Vector2Int(Mathf.RoundToInt(targetPos.x), Mathf.RoundToInt(targetPos.y));
        path = PathFinder.FindPath(start, end);
    }

    public override void Next()
    {
        path.RemoveAt(0);
    }
}
