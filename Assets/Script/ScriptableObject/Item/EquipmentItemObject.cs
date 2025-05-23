using UnityEngine;

public enum EquipmentType
{
    Wapon, Armor, Accessory
}

[CreateAssetMenu(fileName = "EquipmentData", menuName = "Scriptable Objects/Equipment", order = 1)]
public class EquipmentItemObject : ItemObject
{
    public DeckObject deck;

    public EquipmentType type;

}
