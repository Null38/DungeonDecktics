using UnityEngine;

[CreateAssetMenu(fileName = "EnemyInfo", menuName = "Scriptable Objects/Info")]
public class EnemyInfo : BaseInfo
{
    public string SpeciesName;

    [SerializeField]
    public int Difficulty;
}
