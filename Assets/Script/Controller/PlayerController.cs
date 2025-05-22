using Astar;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(InfoComponent))]
public class PlayerController : Controller, ITurnBased
{
    private InfoComponent info;
    private bool hasMove = false;
    private Vector3? target = null;

    void Awake()
    {
        DataManager.player = this;
        info = GetComponent<InfoComponent>();
    }

    void Update()
    {
        if (!GameManager.Instance.isPlayerTurn)
            return;

        // 클릭 입력 처리
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("PlayerController: Left Click Detected");

            // 마우스 포인터 → 월드 좌표 변환 (Z값 지정!)
            Vector3 sp = Input.mousePosition;
            sp.z = Mathf.Abs(Camera.main.transform.position.z);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(sp);
            worldPos.z = 0f;
            Debug.Log($"PlayerController: World Click at {worldPos}");

            // 경로 계산
            GetPath(worldPos);
            Debug.Log($"PlayerController: Path nodes count = {path.Count}");

            if (path.Count > 0)
            {
                // LinkedList<Vector2Int> → Vector3 변환
                Vector2Int nextNode = path.First.Value;
                target = new Vector3(nextNode.x, nextNode.y, 0f);
                hasMove = true;
            }
        }
    }

    public override void NextStep()
    {
        if (path.Count > 0)
            path.RemoveFirst();

        if (path.Count > 0)
            target = new Vector3(path.First.Value.x, path.First.Value.y, 0f);
        else
            target = null;

        OnTurnEnd();
    }

    public void OnTurnBegin()
    {
        hasMove = false;
        target = null;
    }

    public void OnTurnEnd()
    {
        GameManager.Instance.EntityActionComplete(gameObject.GetInstanceID());
    }

    public override Vector3? TargetPos => GameManager.Instance.isPlayerTurn ? target : null;
}
