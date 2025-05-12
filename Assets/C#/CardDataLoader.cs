using UnityEngine;
using System.IO;
using UnityEngine.UI; // �ʿ��� ���

public class JsonReader : MonoBehaviour
{
    [SerializeField] private TextAsset jsonFile; // ����Ƽ �����Ϳ��� JSON ������ �Ҵ�
    [SerializeField] private GameObject spriteObject; // Sprite�� ����� ������Ʈ
    private Image spriteImage; // Sprite�� ����� �̹��� ������Ʈ

    void Start()
    {
        if (jsonFile == null || spriteObject == null) return;

        spriteImage = spriteObject.GetComponent<Image>(); // �̹����� ������

        // JSON ���� �ε� �� ������ �Ľ�
        string jsonString = jsonFile.text;
        ArtworkData artworkData = JsonUtility.FromJson<ArtworkData>(jsonString);

        // ������ ���� (��: �̹��� �ε�)
        if (artworkData != null && artworkData.images != null && artworkData.images.Length > 0)
        {
            string imagePath = artworkData.images[0].path;
            Sprite sprite = Resources.Load<Sprite>(imagePath);

            // Sprite �̹����� ����
            if (sprite != null && spriteImage != null)
            {
                spriteImage.sprite = sprite;
            }
        }
    }

    // JSON �����͸� ������ Ŭ����
    [System.Serializable]
    public class ArtworkData
    {
        public ImageInfo[] images;
        // �ʿ信 ���� �ִϸ��̼� �� �ٸ� ������ �߰� ����
    }

    [System.Serializable]
    public class ImageInfo
    {
        public string path;
    }
}

//using System.IO;
//using System.Collections.Generic;
//using Newtonsoft.Json;
//using UnityEngine;

//public class CardDataLoader : MonoBehaviour
//{
//    public List<CardData> cards = new(); // ���� ���� ī�� ������

//    public void SaveCardsToJson()
//    {
//        string json = JsonConvert.SerializeObject(cards, Formatting.Indented);
//        string filePath = Path.Combine(Application.persistentDataPath, "cards.json");

//        File.WriteAllText(filePath, json);
//        Debug.Log($"��� ī�� ������ ���� �Ϸ�! ���� ��ġ: {filePath}");
//    }

//    public void LoadCardsFromJson()
//    {
//        string filePath = Path.Combine(Application.persistentDataPath, "cards.json");

//        if (File.Exists(filePath))
//        {
//            string json = File.ReadAllText(filePath);
//            cards = JsonConvert.DeserializeObject<List<CardData>>(json);
//            Debug.Log($"ī�� ������ �ε� �Ϸ�! �� {cards.Count}���� ī��");
//        }
//        else
//        {
//            Debug.LogError("����� ī�� �����Ͱ� �����ϴ�.");
//            cards = new List<CardData>(); // �����Ͱ� ������ �� ����Ʈ ����
//        }
//    }
//}