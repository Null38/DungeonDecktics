using UnityEngine;
using System.Collections.Generic;

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

    // Deck
    public DeckObject defaultDeck;
}