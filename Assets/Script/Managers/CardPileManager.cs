using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using static Inventory;
using Random = UnityEngine.Random;

public class CardPileManager
{

    private struct CardInfo
    {
        public equipmentId parentId;
        public CardObjectBase card;
    }

    [Header("Card Piles")]
    [SerializeField]
    private List<CardInfo> drawPile = new(); // 뽑을 카드 더미
    [SerializeField]
    private List<CardInfo> handPile = new(); // 손패
    [SerializeField]
    private List<CardInfo> discardPile = new(); // 버린 카드 더미

    [SerializeField] private Inventory inventory;


    public List<CardObjectBase> cardPile
    {
        get
        {
            throw new NotImplementedException("3개의 카드 더미를 합친걸 반환");
        }
    }


    public List<CardObjectBase> GetDrawPile() => throw new NotImplementedException("GetDrawPile 미구현");
    public List<CardObjectBase> GetHandPile() => throw new NotImplementedException("GetHandPile 미구현");
    public List<CardObjectBase> GetDiscardPile() => throw new NotImplementedException("GetDiscardPile 미구현");


    public void Initalize()
    {
        cardPileInitalize();
    }

    /// <summary>
    /// 카드 더미 초기화
    /// </summary>
    private void cardPileInitalize()
    {
        drawPile.Clear();
        handPile.Clear();
        discardPile.Clear();

        throw new NotImplementedException();

        // 인벤토리에서 캐릭터 카드, 장비 카드, 아이템 카드 합치기.
        // 합친거는 드로우 파일에 저장.

        ShuffleDrawPile();
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
