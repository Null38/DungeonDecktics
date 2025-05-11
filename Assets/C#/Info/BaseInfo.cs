using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;


public enum CharacterClass
{
    Knight
}

public enum CharacterSpecies
{
    Goblin,
    Slime,
    Rat,
    Skeleton
}

[System.Serializable]
public class Card           // �ӽÿ�
{
    public string CardName;
}



public abstract class BaseInfo : ScriptableObject
{
    // HP
    public int MaxHp;
    public int MaxCost;


    [SerializeField]
    public int currentHp;
    // Cost
    [SerializeField]
    public int currentCost;

    // Shield
    [SerializeField]
    public int currentShield;

    // Deck
    [SerializeField]
    public List<Card> deck = new List<Card>();

    // ȸ���� Evasion (���̵� ���� �߻��� ����)
    [SerializeField]
    public float evasion;
}