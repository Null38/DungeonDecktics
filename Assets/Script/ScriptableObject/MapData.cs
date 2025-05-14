using System;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName = "ScriptableObjects/MapData")]
public class MapData : ScriptableObject
{
    [SerializeField]
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
    private Vector2Int size;
    private Vector2Int center;

    public int[,] Map { get; private set; }
    public Vector2Int Size { get => size; }
    public Vector2Int Center { get => center; }

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

        ConvertToVar();
    }

    private void ConvertToVar()
    {
        center = new Vector2Int(data.centerX, data.centerY);
        size.x = data.width;
        size.y = data.width;

        Map = new int[size.x, size.y];
        
        for (int i = 0; i < data.map.Length; i++)
        {
            Map[i % size.x, i / size.x] = data.map[i];
        }
    }
}
