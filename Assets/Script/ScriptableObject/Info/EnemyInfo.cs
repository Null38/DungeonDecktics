using UnityEngine;

[CreateAssetMenu(fileName = "new EnemyInfo", menuName = "Scriptable Objects/Info/Enemy Info")]
public class EnemyInfo : BaseInfo
{
    public CharacterSpecies CharacterSpecies;

    [SerializeField]
    public int Difficulty;
}
