using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Inventory : MonoBehaviour
{
    [SerializeField]
    private DeckObject[] equipment = new DeckObject[4];

    public List<CardObjectBase> GetCardPile()
    {
        // 인벤토리에서 캐릭터 카드, 장비 카드, 아이템 카드 합치기.
        throw new NotImplementedException("GetCardPile 구현안됨");
    }
}
