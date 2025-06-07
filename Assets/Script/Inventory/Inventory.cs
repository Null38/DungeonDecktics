using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
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

    public Sprite[] sprite;
    public DeckObject[] decks;

    public void Init()
    {
        if (DumbAssSave.eqSave[0] != null)
        {
            decks[0] = DumbAssSave.eqSave[0].deck;
            sprite[0] = DumbAssSave.eqSave[0].img;
        }

        if (DumbAssSave.eqSave[1] != null)
        {
            decks[1] = DumbAssSave.eqSave[1].deck;
            sprite[1] = DumbAssSave.eqSave[1].img;

        }
    }

    public List<CardBase>[] GetCardPile()
    {
        List<CardBase>[] combined = new List<CardBase>[Enum.GetValues(typeof(ParentId)).Length];

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
