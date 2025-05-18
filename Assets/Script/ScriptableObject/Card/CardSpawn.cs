using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    public GameObject cardPrefab;        // ī�� UI ������
    public Transform parentTransform;    // ī�尡 ������ �θ� (��: Canvas ���� Hand �г�)
    public CardObject[] cardObjects;     // ������ ī�� �����͵�

    void Start()
    {
        foreach (var card in cardObjects)
        {
            // ������ �ν��Ͻ� ����
            GameObject cardInstance = Instantiate(cardPrefab, parentTransform);

            // CardUI ��ũ��Ʈ ��������
            CardUI cardUI = cardInstance.GetComponent<CardUI>();

            // ī�� ������ �Ҵ�
            cardUI.SetCard(card);
        }
    }
}
