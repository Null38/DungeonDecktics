using UnityEngine;

[CreateAssetMenu(fileName = "new MapObject", menuName = "Scriptable Objects/Map/MapObject")]
public class MapObject : ScriptableObject
{
    public string objectName;
    public Sprite sprite;
    public GameObject basePrefab;
    public LayerMask layer;
    public int subData;
}
