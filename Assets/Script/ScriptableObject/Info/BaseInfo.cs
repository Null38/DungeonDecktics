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

public abstract class BaseInfo : ScriptableObject
{
    // HP
    public int MaxHp;
    public int MaxCost;

    [HideInInspector]
    public int currentHp;
    // Cost
    [HideInInspector]
    public int currentCost;

    // Shield
    [HideInInspector]
    public int currentShield;

    // 회피율 Evasion (난이도 문제 발생시 삭제)
    public float evasion;

    // Deck
    public List<CardObjectBase> defaultDeck = new List<CardObjectBase>();
}