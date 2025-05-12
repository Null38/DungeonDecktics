using UnityEngine;
using System.IO;
using UnityEngine.UI; // 필요한 경우

public class JsonReader : MonoBehaviour
{
    [SerializeField] private TextAsset jsonFile; // 유니티 에디터에서 JSON 파일을 할당
    [SerializeField] private GameObject spriteObject; // Sprite가 적용될 오브젝트
    private Image spriteImage; // Sprite가 적용될 이미지 컴포넌트

    void Start()
    {
        if (jsonFile == null || spriteObject == null) return;

        spriteImage = spriteObject.GetComponent<Image>(); // 이미지를 가져옴

        // JSON 파일 로드 및 데이터 파싱
        string jsonString = jsonFile.text;
        ArtworkData artworkData = JsonUtility.FromJson<ArtworkData>(jsonString);

        // 데이터 적용 (예: 이미지 로드)
        if (artworkData != null && artworkData.images != null && artworkData.images.Length > 0)
        {
            string imagePath = artworkData.images[0].path;
            Sprite sprite = Resources.Load<Sprite>(imagePath);

            // Sprite 이미지로 적용
            if (sprite != null && spriteImage != null)
            {
                spriteImage.sprite = sprite;
            }
        }
    }

    // JSON 데이터를 저장할 클래스
    [System.Serializable]
    public class ArtworkData
    {
        public ImageInfo[] images;
        // 필요에 따라 애니메이션 등 다른 데이터 추가 가능
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
//    public List<CardData> cards = new(); // 여러 개의 카드 데이터

//    public void SaveCardsToJson()
//    {
//        string json = JsonConvert.SerializeObject(cards, Formatting.Indented);
//        string filePath = Path.Combine(Application.persistentDataPath, "cards.json");

//        File.WriteAllText(filePath, json);
//        Debug.Log($"모든 카드 데이터 저장 완료! 파일 위치: {filePath}");
//    }

//    public void LoadCardsFromJson()
//    {
//        string filePath = Path.Combine(Application.persistentDataPath, "cards.json");

//        if (File.Exists(filePath))
//        {
//            string json = File.ReadAllText(filePath);
//            cards = JsonConvert.DeserializeObject<List<CardData>>(json);
//            Debug.Log($"카드 데이터 로드 완료! 총 {cards.Count}개의 카드");
//        }
//        else
//        {
//            Debug.LogError("저장된 카드 데이터가 없습니다.");
//            cards = new List<CardData>(); // 데이터가 없으면 빈 리스트 생성
//        }
//    }
//}