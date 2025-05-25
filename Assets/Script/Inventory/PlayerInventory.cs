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

    public override List<CardObjectBase>[] GetCardPile()
    {
        List<CardObjectBase>[] combined = new List<CardObjectBase>[Enum.GetValues(typeof(ParentId)).Length];

        for (int i = 0; i < decks.Length; i++)
        {
            if (decks[i] == null)
            {
                continue;
            }

            combined[i] = new();

            foreach (var card in decks[i].cards)
            {
                combined[i].Add(card);
            }
        }

        Debug.LogError("캐릭터 특수 카드랑 인벤 템은 안보내고 있어요.\n구현해야합니다.");

        return combined;
    }
}
