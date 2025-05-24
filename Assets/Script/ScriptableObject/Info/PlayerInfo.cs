using UnityEngine;

public enum CharacterClass
{
    Knight
}

[CreateAssetMenu(fileName = "new PlayerInfo", menuName = "Scriptable Objects/Info/PlayerInfo")]
public class PlayerInfo : BaseInfo
{
    public CharacterClass CharacterClass;

    [SerializeField]
    public int Experience;

    [SerializeField]
    public int additionalHp;

    [SerializeField]
    public int additionalShield;
}
