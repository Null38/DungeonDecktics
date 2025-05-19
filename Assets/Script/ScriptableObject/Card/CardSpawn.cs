using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    public GameObject cardPrefab;     // 카드 프리팹
    public Transform parentTransform; // 카드들이 들어갈 부모 오브젝트 (Canvas 하위)

    public CardObject cardData;       // 카드 데이터 (ScriptableObject)

    void Start()
    {
        // 카드 프리팹 복제해서 씬에 배치
        GameObject cardInstance = Instantiate(cardPrefab, parentTransform);

        // 복제된 프리팹에 ScriptableObject의 값 적용
        cardData.ApplyTo(cardInstance);
    }
}
