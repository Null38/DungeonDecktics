using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject CardPrefab;
    [SerializeField]
    private Transform parent;

    public GameObject SpawnCard(CardObjectBase info)
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
}
