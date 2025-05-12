using System;
using UnityEngine;

static class Json2Map
{

    private class TemplateValues
{
    public int centerX;
    public int centerY;
}

[Serializable]
private class TemplateLayer
{
    public string name;
    public int[][] grid2D;
    public EntityData[] entities;
}

[Serializable]
private class EntityData
{
    public string name;
    public int id;
    public float x;
    public float y;
    public float originX;
    public float originY;
}

[Serializable]
private class TemplateData
{
    public int width;
    public int height;
    public TemplateValues values;
    public TemplateLayer[] layers;
}

    static (int, int[,]) Convert2Map()
    {
        throw new NotImplementedException(); 
    }
}
