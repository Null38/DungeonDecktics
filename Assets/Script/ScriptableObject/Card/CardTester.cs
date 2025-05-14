using UnityEngine;

public class CardTester : MonoBehaviour
{
    public CardObject card;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(card.FormatDescription());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
