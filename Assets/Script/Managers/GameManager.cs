using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } 

    public int currentTurn = 0;
    public bool isPlayerTurn { get; private set;}

    public static event Action GameOverEvent;
    public static event Action PlayerTurnEvent;
    public static event Action EnemyTurnEvent;


    private Dictionary<int, ITurnBased> activeEntitys = new Dictionary<int, ITurnBased>();

    private void Awake()
    {
        if (Instance == null) 
            Instance = this;
        else 
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject); // GameManager 파괴 방지
    }

    private void Start()
    {
        StartInit();
    }

    public void StartInit()
    {
        currentTurn = 0; // 게임 시작 시 턴 0으로 설정
        isPlayerTurn = true;

        // 필요한 초기화 작업 추가 (예: 플레이어, 적 초기화)
        Debug.Log("게임 시작!");
    }

    // 플레이어 턴 시작
    private void StartPlayerTurn()
    {
        activeEntitys.Clear();


        activeEntitys.Add(DataManager.player.GetInstanceID(), value: DataManager.player);

        BroadcastTurnStart();
        // 플레이어 엔티티들 활성화
        

        // 턴 시작 이벤트 발생
        PlayerTurnEvent?.Invoke();

        Debug.Log($"플레이어 턴 시작 (턴: {currentTurn}) - 활성 엔티티: {activeEntitys.Count}");

    }

    // 적 턴 시작
    private void StartEnemyTurn()
    {
        activeEntitys.Clear();

        // 적 엔티티들 활성화
        foreach (var enemy in EnemyController.ActiveEnemy)
        {
            activeEntitys.Add(enemy.Key, enemy.Value);
            
        }

        BroadcastTurnStart();

        // 턴 시작 이벤트 발생
        EnemyTurnEvent?.Invoke();

        Debug.Log($"적 턴 시작 (턴: {currentTurn}) - 활성 엔티티: {activeEntitys.Count}");
    }

    private void BroadcastTurnStart()
    {
        foreach (var each in activeEntitys)
        {
            each.Value.OnTurnBegin();
        }
    }

    // 엔티티가 자신의 행동을 완료했을 때 호출
    public void EntityActionComplete(int entityId)
    {
        if (activeEntitys.ContainsKey(entityId))
        {
            activeEntitys.Remove(entityId);
            CheckEndTurn();
        }
    }

    // 모든 엔티티가 행동을 완료했는지 확인
    private void CheckEndTurn()
    {
        if (activeEntitys.Count <= 0)
        {
            isPlayerTurn = !isPlayerTurn;
            currentTurn++;

            if (isPlayerTurn)
            {
                StartPlayerTurn();
            }
            else
            {
                StartEnemyTurn();
            }
        }
    }

    // 게임 종료 조건 확인 및 처리
    public void CheckGameOverCondition()
    {
        // 게임 오버 조건 체크 로직
        bool isGameOver = false; // 실제 게임 오버 조건으로 대체

        if (isGameOver)
        {
            EndGame();
        }
    }

    // 게임 오버 처리
    private void EndGame()
    {
        GameOverEvent?.Invoke();
        Debug.Log("게임 오버!");
    }
}

public interface ITurnBased
{
    void OnTurnBegin();
    protected void OnTurnEnd();
}