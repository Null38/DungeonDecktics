using System;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName = "ScriptableObjects/MapData")]
public class MapData : ScriptableObject
{
    private class JsonData
    {
        public int width;
        public int height;
        public int centerX;
        public int centerY;
        
        public int[] map;
    }

    [SerializeField]
    [TextArea(10, 15)]
    private string jsonFile;
    private JsonData data;

    public int[,] Map { get; private set; }
    public Vector2Int[,] Size { get; private set; }

    private void OnEnable()
    {
        try
        {
            data = JsonUtility.FromJson<JsonData>(jsonFile);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            return;
        }

        ConvertJson();
    }

    private void ConvertJson()
    {
        int rows = data.height;
        int cols = data.width;

        Map = new int[cols, rows];
        
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                Map[x, y] = data.map[x * cols + y];
            }
        }
    }
}
