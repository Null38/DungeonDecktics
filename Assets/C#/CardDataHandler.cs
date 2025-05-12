using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class CardDataHandler : MonoBehaviour
{
    public CardData card; // ���� ī��

    public CardData LoadCardFromJson()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "cardData.json");

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            card = JsonConvert.DeserializeObject<CardData>(json);

            Debug.Log($"ī�� ������ �ε� �Ϸ�: {card.CardName}");
            return card;
        }
        else
        {
            Debug.LogError("����� ī�� �����Ͱ� �����ϴ�.");
            return null;
        }
    }
}