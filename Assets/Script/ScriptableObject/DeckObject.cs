using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DeckData", menuName = "Scriptable Objects/Deck", order = 1)]
public class DeckObject : ScriptableObject
{
    [SerializeField]
    public List<CardBase> cards;
}
