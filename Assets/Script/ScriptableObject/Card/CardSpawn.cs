using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    public GameObject cardPrefab;        // 카드 UI 프리팹
    public Transform parentTransform;    // 카드가 생성될 부모 (예: Canvas 안의 Hand 패널)
    public CardObject[] cardObjects;     // 생성할 카드 데이터들

    void Start()
    {
        foreach (var card in cardObjects)
        {
            // 프리팹 인스턴스 생성
            GameObject cardInstance = Instantiate(cardPrefab, parentTransform);

            // CardUI 스크립트 가져오기
            CardUI cardUI = cardInstance.GetComponent<CardUI>();

            // 카드 데이터 할당
            cardUI.SetCard(card);
        }
    }
}
