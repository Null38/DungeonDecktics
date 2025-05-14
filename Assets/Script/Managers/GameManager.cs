using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } 
    public int currentTurn = 0; 
    public bool isGameOver = false; // ���� ���� ���� üũ
    public GameObject player; // �÷��̾� ������Ʈ
    public GameObject enemy; // �� ������Ʈ

    // �� ������ ���� �⺻ ��
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

        DontDestroyOnLoad(gameObject); // GameManager �ı� ����
    }

    private void Start()
    {
        StartGame();
    }

    // ���� �ʱ�ȭ
    public void StartGame()
    {
        currentTurn = 1; // ���� ���� �� �� 1�� ����
        isGameOver = false;
        isPlayerTurn = true;

        // �ʿ��� �ʱ�ȭ �۾� �߰� (��: �÷��̾�, �� �ʱ�ȭ)
        Debug.Log("���� ����!");
        BeginTurn();
    }

    // ���� �����ϴ� �Լ�
    private void BeginTurn()
    {
        if (isGameOver)
        {
            Debug.Log("������ ����Ǿ����ϴ�.");
            return;
        }

        if (isPlayerTurn)
        {
            Debug.Log("�÷��̾��� ���Դϴ�.");
            // �÷��̾� �� ���� ���� �߰� (��: �÷��̾� �ൿ ����)
            // �� ���ÿ����� �ܼ��� �����̸� �ְ� ���� ������ �Ѿ���� ����
            StartCoroutine(PlayerTurn());
        }
        else
        {
            Debug.Log("���� ���Դϴ�.");
            // �� �� ���� ���� �߰� (��: AI �ൿ)
            StartCoroutine(EnemyTurn());
        }
    }

    // �÷��̾� �� ó�� (���÷� �����̸� �ְ� ���� ��ȯ)
    private IEnumerator PlayerTurn()
    {
        // �÷��̾ �ൿ�ϴ� �ð� (��: �ൿ ���� ȭ��)
        yield return new WaitForSeconds(2f);

        // �÷��̾ �̵��ϰų� �ൿ�ϴ� ������ ����
        if (player != null)
        {
            player.transform.position = new Vector3(1, 0, 0); // �÷��̾� �̵�
            Debug.Log("�÷��̾� �̵�: " + player.transform.position);
        }

        // �� ��ȯ
        isPlayerTurn = false;
        currentTurn++;
        BeginTurn(); // ���� ������ �Ѿ
    }

    // �� �� ó�� (���÷� ������ ������ �� ���� ��ȯ)
    private IEnumerator EnemyTurn()
    {
        // ���� �ൿ �ð� (AI�� �� ó��)
        yield return new WaitForSeconds(2f);

        // ���� �ൿ ����: �� �̵�
        if (enemy != null)
        {
            enemy.transform.position = new Vector3(-1, 0, 0); // �� �̵�
            Debug.Log("�� �̵�: " + enemy.transform.position);
        }

        // �� ��ȯ
        isPlayerTurn = true;
        currentTurn++;
        BeginTurn(); // �÷��̾��� ������ �Ѿ
    }

    // ���� ���� ó��
    public void EndGame()
    {
        isGameOver = true;
        Debug.Log("���� ����!");
    }
}
