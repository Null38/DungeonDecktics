using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject CardPrefab;
    public GameObject SpawnCard(CardObjectBase info, GameObject parent)
    {
        if (info == null)
        {
            Debug.LogError("card info is null");
            return null;
        }


        GameObject cardInstance = Instantiate(CardPrefab, parent.transform);

        CardComponent cardComponent = cardInstance.GetComponent<CardComponent>();

        cardComponent.CardInit(info);

        return cardInstance;

    }
}
