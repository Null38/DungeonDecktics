using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class CardDataHandler : MonoBehaviour
{
    public CardData card; // 현재 카드

    public CardData LoadCardFromJson()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "cardData.json");

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            card = JsonConvert.DeserializeObject<CardData>(json);

            Debug.Log($"카드 데이터 로드 완료: {card.CardName}");
            return card;
        }
        else
        {
            Debug.LogError("저장된 카드 데이터가 없습니다.");
            return null;
        }
    }
}