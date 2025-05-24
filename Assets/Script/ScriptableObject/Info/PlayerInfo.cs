using UnityEngine;

public enum CharacterClass
{
    Knight
}

[CreateAssetMenu(fileName = "new PlayerInfo", menuName = "Scriptable Objects/Info/PlayerInfo")]
public class PlayerInfo : BaseInfo
{
    public CharacterClass CharacterClass;

    public int Experience;

    public int additionalHp;

    public int additionalShield;

    public EquipmentItemObject[] equipment;
}
