using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject CardPrefab;
    public void SpawnCard(CardObjectBase info)
    {
        if (info == null)
        {
            Debug.LogError("card info is null");
            return;
        }


        GameObject cardInstance = Instantiate(CardPrefab);

        CardComponent cardComponent = cardInstance.GetComponent<CardComponent>();

        cardComponent.CardInit(info);

    }
}
