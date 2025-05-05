using UnityEngine;
using System.Collections.Generic;

// 아직 디버그는 못 함.

public enum CharacterClass
{
    Knight
}

[System.Serializable]
public class Card           // 임시용
{
    public string CardName;
}

[System.Serializable]
public class PlayerInfo : BaseInfo     // 임시용
{
    public string ClassName;
    public int Experience;
}

[System.Serializable]
public class EnemyInfo : BaseInfo      // 임시용
{
    public string SpeciesName;
    public int Difficulty;
}


[System.Serializable]
public class BaseInfo // monobehaviour는 하단의 BaseInfoComponent에서 상속받음
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

    // 캐릭터 초기화 (플레이어 및 에너미의 상위 개념 --> 캐릭터)
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

    // HP 조정 메서드
    public void TakeDamage(int damage)
    {
        int remainingDamage = damage;

        // 1. 쉴드 먼저 깎기
        if (CurrentShield > 0)
        {
            int shieldDamage = Mathf.Min(CurrentShield, remainingDamage);
            CurrentShield -= shieldDamage;
            remainingDamage -= shieldDamage;
        }

        // 2. 남은 데미지를 HP에 적용
        if (remainingDamage > 0)
        {
            CurrentHp = Mathf.Max(CurrentHp - remainingDamage, 0);
            Debug.Log($"{Name}이(가) {remainingDamage}의 피해를 입었습니다. (남은 체력: {CurrentHp})");

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
    
    // 죽음 처리 메서드
    protected virtual void Die()
    {
        Debug.Log(Name + "이(가) 죽었습니다.");
        RestartPrompt();
    }

    // Cost 조정 메서드
    public void UseCost(int cost)
    {
        CurrentCost = Mathf.Max(CurrentCost - cost, 0);
    }

    public void RestoreCost()
    {
        CurrentCost = MaxCost;
    }

    // 재시작 처리 메서드
    protected virtual void RestartPrompt()
    {
        // 여기서는 디버그용. 실제 게임에서는 UI로 Yes/No 버튼 띄워야 함.
        Debug.Log("다시 시작하시겠습니까? (Y:0/N:1)");

        // 테스트용으로 임시 숫자 입력 받는다고 가정
        int input = GetDebugInput(); // 여기서 임의로 입력받는 함수

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
            Debug.Log("잘못된 입력입니다. 다시 선택하세요.");
            RestartPrompt(); // 다시 입력받기
        }
    }

    private int GetDebugInput()
    {
        // 여기서 디버그 테스트니까 그냥 0 또는 1을 리턴하는 걸로 가정
        return 0; // 일단은 무조건 Yes
    }

    // 플레이어가 다시 시작하는 경우
    protected virtual void Restart()
    {
        CurrentHp = MaxHp;
        CurrentCost = MaxCost;
        // Shield는 그대로 유지

        // 여기서는 디버그용. 
        Debug.Log("다시 시작합니다.");
    }

    // 메인 메뉴로 돌아가는 경우
    protected virtual void ReturnToMainMenu()
    {
        // 여기서는 디버그용. 
        Debug.Log("메인 화면으로 돌아갑니다.");
        // SceneManager.LoadScene("MainMenu"); 이런 식으로 메인 메뉴로 이동...하라는듯??
    }
}

// 일단 캐릭터 하나하나 하드코딩 하는 방법으로 진행 중
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
                // HP 100, Cost 30, Shield 20 임시로 지정
                Info.Initialize("Knight", 100, 30, 20);
                break;
        }
    }

    private void Update()
    {
        // ...
    }
}