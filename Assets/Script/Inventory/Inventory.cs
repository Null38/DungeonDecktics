using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Inventory : MonoBehaviour
{
    [SerializeField]
    protected DeckObject[] decks;

    public abstract List<CardObjectBase>[] GetCardPile();
}
