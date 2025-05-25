using UnityEngine;

public class PlayerController : Controller, ITurnBased
{
    private Vector3? target = null;

    [SerializeField]
    public PlayerInventory inventory;

    public CardPileManager CardPile {get; private set;}

    void Awake()
    {
        DataManager.player = this;
        CardPile = new(inventory);
        CardPile.Initalize();
    }

    void Start()
    {
    }

    void Update()
    {
        if (!GameManager.Instance.IsPlayerTurn)
            return;

        // 클릭 입력 처리
        if (Input.GetMouseButtonDown(0))
        {

            // 마우스 포인터 → 월드 좌표 변환 (Z값 지정!)
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0f;

            // 경로 계산
            GetPath(worldPos);

            if (path.Count > 0)
            {
                // LinkedList<Vector2Int> → Vector3 변환
                Vector2Int nextNode = path.First.Value;
                target = new Vector3(nextNode.x, nextNode.y, 0f);
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
        
    }

    public void OnTurnEnd()
    {
        GameManager.Instance.EntityActionComplete(this);
    }

    public override Vector3? TargetPos => GameManager.Instance.IsPlayerTurn ? target : null;
}
