using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject targetPrefab;
    public Inventory inventory;
    public CardPileManager cardPile;

    [Header("Turn Management")]
    public int currentTurn = 0;
    public bool IsPlayerTurn { get; private set; }

    public static event Action GameOverEvent;
    public static event Action PlayerTurnEvent;
    public static event Action EnemyTurnEvent;

    private Dictionary<Controller, ITurnBased> activeEntitys = new();
    private List<GameObject> targetObjs = new();

    public CardComponent selectCard;
    public static event Action<RectTransform> CardSelectedEvent;


    [Header("Enemy Spawning")]
    [Tooltip("씬에 배치할 적 Prefab")]
    public GameObject enemyPrefab;
    [Tooltip("적을 스폰할 위치들")]
    public List<Transform> spawnPoints = new();

    [Header("Damage Popup")]
    [SerializeField] private GameObject damagePopupPrefab;

    private void Awake()
    {
        // 싱글톤 초기화
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        InfoComponent.OnPlayerDied += HandlePlayerDeath;
        InfoComponent.OnEnemyDied += HandleEnemyDeath;

        inventory.Init();
        cardPile = new(inventory);
        cardPile.Initalize();

        Debug.Log("GameManager initialized");
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex >= 3)
        {
            StartInit();
        }
    }

    public void StartInit()
    {
        currentTurn = 0;
        IsPlayerTurn = true;
        Debug.LogWarning("던전 생성 안함\n구현은 되어있는데 맵생성이 아직 몹생성이나 플레이어 위치를 바꾸는 코드가 없음.");
        //DataManager.generator.Generate();


        Debug.Log("게임 시작!");


        // 씬 시작 시 적 스폰
        SpawnAllEnemies();

        // 플레이어 턴으로 진입
        StartPlayerTurn();
    }

    /// <summary>
    /// inspector에 지정된 위치에 enemyPrefab을 Instantiate 합니다.
    /// </summary>
    private void SpawnAllEnemies()
    {
        if (enemyPrefab == null || spawnPoints.Count == 0)
            return;

        foreach (var pt in spawnPoints)
        {
            Instantiate(enemyPrefab, pt.position, pt.rotation);
        }
        Debug.Log($"적 {spawnPoints.Count}마리 스폰 완료");
    }

    private void StartPlayerTurn()
    {
        activeEntitys.Clear();
        // DataManager.player는 이미 Awake 단계에서 세팅되어 있어야 합니다.
        activeEntitys.Add(DataManager.player, DataManager.player);

        PlayerTurnEvent?.Invoke();
        BroadcastTurnStart();

        Debug.Log($"플레이어 턴 시작 (턴: {currentTurn}) - 활성 엔티티: {activeEntitys.Count}");
    }

    private void StartEnemyTurn()
    {
        activeEntitys.Clear();
        // EnemyController.ActiveEnemy는 스폰된 Enemy들이 자동 등록됩니다.
        foreach (var kv in EnemyController.ActiveEnemy)
        {
            activeEntitys.Add(kv.Key, kv.Value);
        }

        EnemyTurnEvent?.Invoke();
        BroadcastTurnStart();

        Debug.Log($"적 턴 시작 (턴: {currentTurn}) - 활성 엔티티: {activeEntitys.Count}");
    }

    private void BroadcastTurnStart()
    {
        foreach (var each in activeEntitys)
            each.Value.OnTurnBegin();
    }

    public void EntityActionComplete(Controller entity)
    {
        if (!activeEntitys.ContainsKey(entity))
            return;

        activeEntitys.Remove(entity);
        CheckGameOverCondition();
        CheckEndTurn();
    }

    private void CheckEndTurn()
    {
        if (activeEntitys.Count > 0)
            return;

        IsPlayerTurn = !IsPlayerTurn;
        currentTurn++;

        if (IsPlayerTurn) StartPlayerTurn();
        else StartEnemyTurn();
    }

    private void CheckGameOverCondition()
    {
        // TODO: 실제 게임 오버 로직으로 교체
        bool isGameOver = false;
        if (isGameOver) EndGame();
    }

    private void EndGame()
    {
        GameOverEvent?.Invoke();
        Debug.Log("게임 오버!");
    }

    public bool IsSameTargetPosition(Controller self, Vector3? targetPos)
    {
        foreach (KeyValuePair<Controller, ITurnBased> entity in activeEntitys)
        {
            if (self == entity.Key)
            {
                continue;
            }

            Vector3? entityTarget = entity.Key.TargetPos;

            if (!entityTarget.HasValue || entityTarget != targetPos)
                continue;
            return true;
        }

        return false;
    }

    public void SpawnTarget(CardBase info)
    {
        RemoveAllTarget();

        List<Vector2> points = new();

        switch (info.targetType)
        {
            case CardBase.TargetType.Self:
                points.Add(DataManager.player.transform.position);
                break;
            case CardBase.TargetType.other:
                foreach (KeyValuePair<Controller, ITurnBased> enemy in EnemyController.ActiveEnemy)
                {
                    if (Vector2.Distance(enemy.Key.transform.position, DataManager.player.transform.position) <= info.distance + 0.5f)
                    {
                        points.Add(enemy.Key.transform.position);
                    }
                }
                break;
        }

        foreach (var position in points)
        {
            GameObject obj = Instantiate(targetPrefab, position, Quaternion.identity);

            Collider2D hit = Physics2D.OverlapPoint(position, DataManager.UnitLayer);

            if (hit == null)
                throw new InvalidOperationException($"타겟 위치 {position}에 유닛이 존재하지 않습니다.");

            obj.GetComponent<TargetTouch>().target = hit.GetComponent<Controller>();

            targetObjs.Add(obj);
        }
    }

    public void RemoveAllTarget()
    {

        foreach (var obj in targetObjs)
        {
            Destroy(obj);
        }
    }

    public static void CardSelectEvent(CardComponent cardInfo, RectTransform cardTransform)
    {
        if (Instance.selectCard == cardInfo)
        {
            Instance.selectCard = null;
            CardSelectedEvent(null);
            Instance.RemoveAllTarget();
        }
        else
        {
            Instance.selectCard = cardInfo;
            CardSelectedEvent(cardTransform);
        }
    }
    /// <summary>
    /// 적/플레이어의 데미지 팝업을 화면에 띄움.
    /// </summary>
    public void ShowDamagePopup(int dmg, Vector3 worldPos)
    {
        if (damagePopupPrefab == null) return;

        // 1) 인스턴스화 (instantiate)
        GameObject go = Instantiate(damagePopupPrefab);

        go.transform.position = worldPos;

        var popup = go.GetComponent<DamagePopup>();
        if (popup != null) popup.Init(dmg);
    }

    private void HandlePlayerDeath()
    {
        Debug.Log("[GameManager] 플레이어 사망 처리 (게임 오버 혹은 리스폰 등)");
        GameOverEvent?.Invoke();
        SceneLoadManager.LoadGameOver();
    }

    private void HandleEnemyDeath(EnemyController enemy)
    {
        if (activeEntitys.ContainsKey(enemy))
        {
            activeEntitys.Remove(enemy);
            CheckEndTurn();
        }

        var playerCtrl = DataManager.player.GetComponent<PlayerController>();
        if (playerCtrl != null)
            playerCtrl.CancelMove();

        DumbAssSave.FUCK++;
        DumbAssSave.savedHp = DataManager.player.info.currentHp;

        if (EnemyController.ActiveEnemy.Count == 1)
        {

            if (DumbAssSave.nextStage == 6)
            {
                SceneLoadManager.LoadLastStageClear();
            }
            else
                SceneLoadManager.LoadStageClear();
        }
    }



    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }

        InfoComponent.OnPlayerDied -= HandlePlayerDeath;
        InfoComponent.OnEnemyDied -= HandleEnemyDeath;
    }

}


public interface ITurnBased
{
    void OnTurnBegin();
    void OnTurnEnd();
}