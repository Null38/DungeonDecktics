using System;
using System.Collections.Generic;
using UnityEngine;
using static Inventory;
using Random = UnityEngine.Random;

public class CardPileManager
{

    private struct CardInfo
    {
        public int parentId;
        public CardBase card;

        public CardInfo(int id, CardBase card)
        {
            parentId = id;
            this.card = card;
        }
    }

    [Header("Card Piles")]
    [SerializeField]
    private List<CardInfo> drawPile = new(); // 뽑을 카드 더미
    [SerializeField]
    private List<CardInfo> handPile = new(); // 손패
    [SerializeField]
    private List<CardInfo> discardPile = new(); // 버린 카드 더미

    [SerializeField] private Inventory inventory;

    public CardPileManager(Inventory inventory)
    {
        this.inventory = inventory;
    }


    public List<CardBase> CardPile
    {
        get
        {
            throw new NotImplementedException("3개의 카드 더미를 합친걸 반환");
        }
    }


    public List<CardBase> GetDrawPile => throw new NotImplementedException("GetDrawPile 미구현");
    public List<CardBase> GetHandPile 
    {
        get
        {
            List<CardBase> value = new();

            Debug.LogWarning("임시로 대충 구현한거 아마도 위험함");
            foreach (CardInfo card in handPile)
            {
                value.Add(card.card);
            }

            return value;
        }
    }
    public List<CardBase> GetDiscardPile => throw new NotImplementedException("GetDiscardPile 미구현");


    public void Initalize()
    {
        CardPileInitalize();
        DrawToHand(5);
    }

    /// <summary>
    /// 카드 더미 초기화
    /// </summary>
    private void CardPileInitalize()
    {
        drawPile.Clear();
        handPile.Clear();
        discardPile.Clear();


        List<CardBase>[] pile =  inventory.GetCardPile();

        for (int i = 0; i < pile.Length; i++)
        {
            if (pile[i] == null)
            {
                continue;
            }
            foreach (CardBase card in pile[i])
            {
                drawPile.Add(new CardInfo(i, card));
            }
        }

        ShuffleDrawPile();
    }

    private void DrawToHand(int count)
    {
        foreach (CardInfo card in handPile)
        {
            discardPile.Add(card);
        }

        handPile.Clear();

        for (int i = 0; i < count; i++)
        {
            if (drawPile.Count == 0)
            {
                drawPile.AddRange(discardPile);
                discardPile.Clear();
            }
            CardInfo card = drawPile[drawPile.Count - 1];
            drawPile.RemoveAt(drawPile.Count - 1);
            handPile.Add(card);
        }
    }

    /// <summary>
    /// 장비 카드 제거
    /// </summary>
    public void RemoveEquipment(/* 장비 */)
    {
        throw new NotImplementedException("RemoveEquipment 미구현");
        //모든 카드더미에서 그 장비 카드 제거. parentId 참조
    }

    /// <summary>
    /// 장비 카드 추가
    /// </summary>
    public void AddEquipment(/* 장비 */)
    {
        throw new NotImplementedException("AddEquipment 미구현");
        //뽑을 카드더미에서 그 장비 카드 추가. parentId 할당
    }

    /// <summary>
    /// 인벤의 아이템 카드 변동 업데이트
    /// </summary>
    public void UpdateInventoryItems(/* 변수가 필요할 것 같다. 어떤식으로? 아니면 장비처럼 나눠야 할까?*/)
    {
        throw new NotImplementedException("UpdateInventoryItems 미구현");
        //뽑을 카드더미에서 그 장비 카드 추가. parentId 할당
    }

    /// <summary>
    /// 뽑을 카드 더미 섞기
    /// </summary>
    private void ShuffleDrawPile()
    {
        for (int i = drawPile.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            (drawPile[i], drawPile[randomIndex]) = (drawPile[randomIndex], drawPile[i]);
        }
    }


}
