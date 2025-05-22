using System.Collections.Generic;
using UnityEngine;

public static class DataManager
{
    public static Vector2Int MapSize = new Vector2Int(15, 9);//맵 생성과 연동 해야함. || 지금생각해보니 이거 왜 여따뒀지 맵으로 옮기기
    public static PlayerController player; // 플레이어 오브젝트
    public static LayerMask WallLayer = LayerMask.GetMask("Wall");
    public static LayerMask UnitLayer = LayerMask.GetMask("Unit");
    public static LayerMask UnPassableLayer => UnitLayer | WallLayer;


    public static float Speed = 10f;
}
