using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : Inventory
{
    public enum ParentId
    {
        Weapon = 0,
        Armor = 1,
        Accessory_0 = 2,
        Accessory_1 = 3,
        Charactor = 4,
        notEquipment = 5
    }


    private DeckObject[] specific = new DeckObject[2];

    public override List<List<CardObjectBase>> GetCardPile()
    {
        List<List<CardObjectBase>> combined = new();

        for (int i = 0; i < decks.Length; i++)
        {
            foreach (var card in decks[i].cards)
            {
                combined[i].Add(card);
            }
        }

        throw new NotImplementedException();

        return combined;
    }
}
