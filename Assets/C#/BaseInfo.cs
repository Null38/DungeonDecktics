using UnityEngine;
using System.Collections.Generic;

// ���� ����״� �� ��.

public enum CharacterClass
{
    Knight
}

[System.Serializable]
public class Card           // �ӽÿ�
{
    public string CardName;
}

[System.Serializable]
public class PlayerInfo : BaseInfo     // �ӽÿ�
{
    public string ClassName;
    public int Experience;
}

[System.Serializable]
public class EnemyInfo : BaseInfo      // �ӽÿ�
{
    public string SpeciesName;
    public int Difficulty;
}


[System.Serializable]
public class BaseInfo // monobehaviour�� �ϴ��� BaseInfoComponent���� ��ӹ���
{
    // HP
    public int MaxHp { get; protected set; }
    public int CurrentHp { get; protected set; }

    // Cost
    public int MaxCost { get; protected set; }
    public int CurrentCost { get; protected set; }

    // Shield
    public int CurrentShield { get; protected set; }

    // Deck
    public List<Card> Deck { get; protected set; } = new List<Card>();

    // Name (Character/Enemy)
    public string Name { get; protected set; }

    // Player/Enemy Info
    public PlayerInfo PlayerData { get; protected set; }
    public EnemyInfo EnemyData { get; protected set; }

    public BaseInfo() { }

    public BaseInfo(string name, int hp, int cost, int shield = 0)
    {
        Initialize(name, hp, cost, shield);
    }

    // ĳ���� �ʱ�ȭ (�÷��̾� �� ���ʹ��� ���� ���� --> ĳ����)
    public virtual void Initialize(string name, int hp, int cost, int shield)
    {
        Name = name;
        MaxHp = hp;
        CurrentHp = MaxHp;
        MaxCost = cost;
        CurrentCost = MaxCost;
        CurrentShield = shield;
        Deck = new List<Card>();
    }

    // HP ���� �޼���
    public void TakeDamage(int damage)
    {
        int remainingDamage = damage;

        // 1. ���� ���� ���
        if (CurrentShield > 0)
        {
            int shieldDamage = Mathf.Min(CurrentShield, remainingDamage);
            CurrentShield -= shieldDamage;
            remainingDamage -= shieldDamage;
        }

        // 2. ���� �������� HP�� ����
        if (remainingDamage > 0)
        {
            CurrentHp = Mathf.Max(CurrentHp - remainingDamage, 0);
            Debug.Log($"{Name}��(��) {remainingDamage}�� ���ظ� �Ծ����ϴ�. (���� ü��: {CurrentHp})");

            if (CurrentHp <= 0)
            {
                Die();
            }
        }
    }

    public bool IsDead => CurrentHp <= 0;

    public void TakeHeal(int heal)
    {
        CurrentHp = Mathf.Min(CurrentHp + heal, MaxHp);
    }
    
    // ���� ó�� �޼���
    protected virtual void Die()
    {
        Debug.Log(Name + "��(��) �׾����ϴ�.");
        RestartPrompt();
    }

    // Cost ���� �޼���
    public void UseCost(int cost)
    {
        CurrentCost = Mathf.Max(CurrentCost - cost, 0);
    }

    public void RestoreCost()
    {
        CurrentCost = MaxCost;
    }

    // ����� ó�� �޼���
    protected virtual void RestartPrompt()
    {
        // ���⼭�� ����׿�. ���� ���ӿ����� UI�� Yes/No ��ư ����� ��.
        Debug.Log("�ٽ� �����Ͻðڽ��ϱ�? (Y:0/N:1)");

        // �׽�Ʈ������ �ӽ� ���� �Է� �޴´ٰ� ����
        int input = GetDebugInput(); // ���⼭ ���Ƿ� �Է¹޴� �Լ�

        if (input == 0)
        {
            Restart();
        }
        else if (input == 1)
        {
            ReturnToMainMenu();
        }
        else
        {
            Debug.Log("�߸��� �Է��Դϴ�. �ٽ� �����ϼ���.");
            RestartPrompt(); // �ٽ� �Է¹ޱ�
        }
    }

    private int GetDebugInput()
    {
        // ���⼭ ����� �׽�Ʈ�ϱ� �׳� 0 �Ǵ� 1�� �����ϴ� �ɷ� ����
        return 0; // �ϴ��� ������ Yes
    }

    // �÷��̾ �ٽ� �����ϴ� ���
    protected virtual void Restart()
    {
        CurrentHp = MaxHp;
        CurrentCost = MaxCost;
        // Shield�� �״�� ����

        // ���⼭�� ����׿�. 
        Debug.Log("�ٽ� �����մϴ�.");
    }

    // ���� �޴��� ���ư��� ���
    protected virtual void ReturnToMainMenu()
    {
        // ���⼭�� ����׿�. 
        Debug.Log("���� ȭ������ ���ư��ϴ�.");
        // SceneManager.LoadScene("MainMenu"); �̷� ������ ���� �޴��� �̵�...�϶�µ�??
    }
}

// �ϴ� ĳ���� �ϳ��ϳ� �ϵ��ڵ� �ϴ� ������� ���� ��
public class BaseInfoComponent : MonoBehaviour
{
    public CharacterClass CharacterType = CharacterClass.Knight;
    public BaseInfo Info;

    private void Awake()
    {
        Info = new BaseInfo();

        switch (CharacterType)
        {
            case CharacterClass.Knight:
                // HP 100, Cost 30, Shield 20 �ӽ÷� ����
                Info.Initialize("Knight", 100, 30, 20);
                break;
        }
    }

    private void Update()
    {
        // ...
    }
}