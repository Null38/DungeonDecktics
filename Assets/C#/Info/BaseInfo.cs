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
public class Card           // 임시용
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

    // 회피율 Evasion (난이도 문제 발생시 삭제)
    [SerializeField]
    public float evasion;
}