using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    public GameObject cardPrefab;     // ī�� ������
    public Transform parentTransform; // ī����� �� �θ� ������Ʈ (Canvas ����)

    public CardObject cardData;       // ī�� ������ (ScriptableObject)

    void Start()
    {
        // ī�� ������ �����ؼ� ���� ��ġ
        GameObject cardInstance = Instantiate(cardPrefab, parentTransform);

        // ������ �����տ� ScriptableObject�� �� ����
        cardData.ApplyTo(cardInstance);
    }
}
