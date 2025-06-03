using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject CardPrefab;
    [SerializeField]
    private Transform parent;

    public GameObject SpawnCard(CardBase info)
    {
        if (info == null)
        {
            Debug.LogError("card info is null");
            return null;
        }


        GameObject cardInstance = Instantiate(CardPrefab, parent);

        CardComponent cardComponent = cardInstance.GetComponent<CardComponent>();

        cardComponent.CardInit(info);

        return cardInstance;

    }

    public void DestroyCards()
    {
        for (int i = parent.childCount - 1; i >= 0; i--)
        {
            Destroy(parent.GetChild(i).gameObject);
        }
    }
}
