using System.Collections.Generic;
using UnityEngine;

public static class DataManager
{
    public static Vector2Int MapSize = new Vector2Int(15, 9);//맵 생성과 연동 해야함. || 지금생각해보니 이거 왜 여따뒀지 맵으로 옮기기
    public static GameObject player; // 플레이어 오브젝트
    public static List<GameObject> enemy = new(); // 적 오브젝트 || 이거 적 클래스로 옮겨야겠다.
    public static LayerMask WallLayer = LayerMask.GetMask("Wall");

    public static float Speed = 5f;
}
