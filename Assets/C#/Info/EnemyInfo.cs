using UnityEngine;

[CreateAssetMenu(fileName = "EnemyInfo", menuName = "Scriptable Objects/Info")]
public class EnemyInfo : BaseInfo
{
    public CharacterSpecies CharacterSpecies;

    [SerializeField]
    public int Difficulty;
}
