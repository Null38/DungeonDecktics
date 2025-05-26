using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Turn Management")]
    public int currentTurn = 0;
    public bool IsPlayerTurn { get; private set; }

    public static event Action GameOverEvent;
    public static event Action PlayerTurnEvent;
    public static event Action EnemyTurnEvent;

    private Dictionary<Controller, ITurnBased> activeEntitys = new();

    [Header("Enemy Spawning")]
    [Tooltip("씬에 배치할 적 Prefab")]
    public GameObject enemyPrefab;
    [Tooltip("적을 스폰할 위치들")]
    public List<Transform> spawnPoints = new();

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

        Debug.Log("GameManager initialized");
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().rootCount >= 2)
            StartInit();
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

    public bool IsSameTargetPosition(Controller self ,Vector3? targetPos)
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
}

public interface ITurnBased
{
    void OnTurnBegin();
    void OnTurnEnd();
}
