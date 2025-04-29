using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;
using Random = UnityEngine.Random;

public class DungeonGenerator : MonoBehaviour
{
    public Vector2Int mapSize;

    private int MinRoomSize = 6;
    private int MaxRoomSize;
    private int RoomCount;
    private int Padding = 4;

    TileType[,] map;

    enum TileType 
    {
        floor = 1,
        door = 2,
        stone = 4,
        softWall = 5,
        wall = 10,

        empty = 0
    };

    private void Initialize()
    {
        MinRoomSize = 6;
        MaxRoomSize = (int)(Math.Min(mapSize.x, mapSize.y) / 4f);
        RoomCount = (int)(Math.Sqrt(mapSize.x * mapSize.y) / 2f);
    }

    public void Generate()
    {
        Initialize();

        List<RectInt> rooms = new List<RectInt>();
        Dictionary<int, int> excludedPoints = new Dictionary<int ,int>();

        int padConst = Padding * 2;
        int randomSize = (mapSize.x - padConst) * (mapSize.y - padConst);

        while (rooms.Count < RoomCount)
        {
            int index = Random.Range(0, randomSize);

            while (excludedPoints.ContainsKey(index))
            {
                index = randomSize + excludedPoints[index];
            }

            Vector2Int point = Int2Vector2Int(index, mapSize.x - padConst) + new Vector2Int(Padding, Padding);

            RectInt? area = IdentifyArea(point);
            if (area == null)
            {
                excludedPoints.Add(index, excludedPoints.Count);
            }
            
            rooms.Add(MakeRoom(area));
            
        }
    }

    private RectInt? IdentifyArea(Vector2Int point)
    {
        Stack<Vector2Int> stack = new Stack<Vector2Int>();
        Stack<Vector2Int> corners = new Stack<Vector2Int>();

        stack.Push(point);

        while (stack.Count > 0)
        {
            Vector2Int curr = stack.Pop();

            if (curr.y == point.y &&
                IsValidTile(curr + Vector2Int.right, TileType.floor, (x, y) => x != y))
            {
                stack.Push(curr + Vector2Int.right);
            }

            Vector2Int newValue = curr + Vector2Int.up;

            if (IsValidTile(newValue, TileType.floor, (x, y) => x != y) &&
               (corners.Count == 0 || curr.y < corners.Peek().y))
            {
                stack.Push(newValue);
            }
            else
            {
                if (corners.TryPeek(out Vector2Int value) && value.y == newValue.y)
                {
                    corners.Pop();
                }

                corners.Push(newValue);
            }
        }

        List<RectInt> areas = new List<RectInt>();

        foreach (Vector2Int corner in corners)
        {
            Vector2Int size = new Vector2Int(point.x - corner.x + 1, point.y - corner.y + 1);
            if (size.x < MinRoomSize || size.y < MinRoomSize)
            {
                continue;
            }

            areas.Add(new RectInt(point, size));
        }

        return areas.Count == 0 ? null : areas[Random.Range(0, areas.Count)];
    }


    private RectInt MakeRoom(RectInt? area)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Checks whether the given position is within the valid map bounds and
    /// whether the tile at that position matches the specified type.
    /// Default comparison is equality (==).
    /// </summary>
    /// <param name="pos">The tile position to check.</param>
    /// <param name="requiredType">The expected tile type to compare against.</param>
    /// <returns>True if the position is valid and the tile matches the type; otherwise, false.</returns>
    bool IsValidTile(Vector2Int pos, TileType requiredType)
    {
        return IsValidTile(pos, requiredType, (x, y) => x == y);
    }

    /// <summary>
    /// Checks whether the given position is within the valid map bounds and
    /// whether the tile at that position satisfies a custom comparison condition against the specified type.
    /// </summary>
    /// <param name="pos">The tile position to check.</param>
    /// <param name="requiredType">The expected tile type to compare against.</param>
    /// <param name="comp">A comparison function that takes the tile's type and the required type, and returns true if valid.</param>
    /// <returns>True if the position is valid and the comparison condition is met; otherwise, false.</returns>
    bool IsValidTile(Vector2Int pos, TileType requiredType, Func<TileType, TileType, bool> comp)
    {
        if (pos.x < Padding || pos.y < Padding || pos.x >= mapSize.x - Padding || pos.y >= mapSize.y - Padding)
            return false;

        return comp(map[pos.x, pos.y], requiredType);
    }


    public Vector2Int Int2Vector2Int(int index, int gridWidth)
    {
        // X는 나머지 값, Y는 몫으로 계산
        int x = index % gridWidth;
        int y = index / gridWidth;

        return new Vector2Int(x, y);
    }
}
