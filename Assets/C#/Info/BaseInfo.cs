using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;


public enum CharacterClass
{
    Knight
}

[System.Serializable]
public class Card           // юс╫ц©К
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
}