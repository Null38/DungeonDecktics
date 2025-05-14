using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } 
    public int currentTurn = 0; 
    public bool isGameOver = false; // 게임 오버 상태 체크
    public GameObject player; // 플레이어 오브젝트
    public GameObject enemy; // 적 오브젝트

    // 턴 진행을 위한 기본 값
    private bool isPlayerTurn = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); 
        }

        DontDestroyOnLoad(gameObject); // GameManager 파괴 방지
    }

    private void Start()
    {
        StartGame();
    }

    // 게임 초기화
    public void StartGame()
    {
        currentTurn = 1; // 게임 시작 시 턴 1로 설정
        isGameOver = false;
        isPlayerTurn = true;

        // 필요한 초기화 작업 추가 (예: 플레이어, 적 초기화)
        Debug.Log("게임 시작!");
        BeginTurn();
    }

    // 턴을 진행하는 함수
    private void BeginTurn()
    {
        if (isGameOver)
        {
            Debug.Log("게임이 종료되었습니다.");
            return;
        }

        if (isPlayerTurn)
        {
            Debug.Log("플레이어의 턴입니다.");
            // 플레이어 턴 관련 로직 추가 (예: 플레이어 행동 선택)
            // 이 예시에서는 단순히 딜레이를 주고 적의 턴으로 넘어가도록 구현
            StartCoroutine(PlayerTurn());
        }
        else
        {
            Debug.Log("적의 턴입니다.");
            // 적 턴 관련 로직 추가 (예: AI 행동)
            StartCoroutine(EnemyTurn());
        }
    }

    // 플레이어 턴 처리 (예시로 딜레이를 주고 턴을 전환)
    private IEnumerator PlayerTurn()
    {
        // 플레이어가 행동하는 시간 (예: 행동 선택 화면)
        yield return new WaitForSeconds(2f);

        // 플레이어가 이동하거나 행동하는 간단한 예시
        if (player != null)
        {
            player.transform.position = new Vector3(1, 0, 0); // 플레이어 이동
            Debug.Log("플레이어 이동: " + player.transform.position);
        }

        // 턴 전환
        isPlayerTurn = false;
        currentTurn++;
        BeginTurn(); // 적의 턴으로 넘어감
    }

    // 적 턴 처리 (예시로 간단한 딜레이 후 턴을 전환)
    private IEnumerator EnemyTurn()
    {
        // 적의 행동 시간 (AI의 턴 처리)
        yield return new WaitForSeconds(2f);

        // 적의 행동 예시: 적 이동
        if (enemy != null)
        {
            enemy.transform.position = new Vector3(-1, 0, 0); // 적 이동
            Debug.Log("적 이동: " + enemy.transform.position);
        }

        // 턴 전환
        isPlayerTurn = true;
        currentTurn++;
        BeginTurn(); // 플레이어의 턴으로 넘어감
    }

    // 게임 오버 처리
    public void EndGame()
    {
        isGameOver = true;
        Debug.Log("게임 오버!");
    }
}
