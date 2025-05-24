using UnityEngine;

public class Inventory : MonoBehaviour
{
    public enum equipmentId
    {
        Weapon = 0,
        Armor = 1,
        Accessory_0 = 2,
        Accessory_1 = 3
    }

    [SerializeField]
    private DeckObject[] equipment = new DeckObject[4];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
