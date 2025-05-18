using System.Collections.Generic;
using UnityEngine;

public static class DataManager
{
    public static PlayerController player; // 플레이어 오브젝트
    public static LayerMask WallLayer = LayerMask.GetMask("Wall");
    public static LayerMask MapObjectLayer = LayerMask.GetMask("MapObjects");

    public static float Speed = 10f;
}
