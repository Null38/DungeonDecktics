using System.Collections.Generic;
using UnityEngine;

public static class DataManager
{
    public static PlayerController player; // 플레이어 오브젝트

    public static DungeonGenerator generator;
    public static LayerMask WallLayer = LayerMask.GetMask("Wall");
    public static LayerMask UnitLayer = LayerMask.GetMask("Unit");
    public static LayerMask UnPassableLayer => UnitLayer | WallLayer;


    public static float Speed = 10f;
}
