using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInfo", menuName = "Scriptable Objects/Info")]
public class PlayerInfo : BaseInfo
{
    public string ClassName;

    [SerializeField]
    public int Experience;

    [SerializeField]
    public int additionalHp;

    [SerializeField]
    public int additionalCost;

    [SerializeField]
    public int additionalShield;
}
