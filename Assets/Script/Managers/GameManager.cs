using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } 

    public int currentTurn = 0;
    public bool isPlayerTurn { get; private set;}

    public event Action GameOverEvent;
    public static event Action EnemyTurnEvent;

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

    //외부에서 턴 종료 시 호출
   public void EndTurn()
    {
        isPlayerTurn = !isPlayerTurn;
        currentTurn++;

        if (false)//게임오버시
        {
            EndGame();
            return;
        }

        if (!isPlayerTurn)
        {
            EnemyTurnEvent?.Invoke();
        }
    }

    // 게임 오버 처리
    private void EndGame()
    {
        GameOverEvent?.Invoke();
        Debug.Log("게임 오버!");
    }
}
