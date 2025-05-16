using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DualGridTile
{
    [Serializable]
    public class DualGridRuleTile : TileBase
    {
        public Tilemap reference;
        public Sprite m_DefaultSprite;
        public Tile.ColliderType m_DefaultColliderType = Tile.ColliderType.Sprite;
        [SerializeField]
        public List<DualGridTilingRule> m_TilingRules;


        readonly public List<Vector3Int> m_NeighborPositions = new List<Vector3Int>()
        {
            new Vector3Int(-1, 0, 0),
            new Vector3Int(0, 0, 0),
            new Vector3Int(-1, -1, 0),
            new Vector3Int(0, -1, 0)
        };

        [Serializable]
        public class DualGridTilingRule
        {
            public Sprite m_sprite;


            public bool[] rules;
        }

        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {

            tileData.sprite = GetSprite(position);
            tileData.colliderType = m_DefaultColliderType;
        }

        Sprite GetSprite(Vector3Int position)
        {
            if (reference == null)
            {
                return m_DefaultSprite;
            }

            foreach (var rule in m_TilingRules)
            {
                bool check = true;
                for (int i = 0; i < 4; i++)
                {
                    Vector3Int refPos = m_NeighborPositions[i] + position;
                    if ((reference.GetTile(refPos) != null) != rule.rules[i])
                    {
                        check = false;
                    }
                }

                if (check)
                    return rule.m_sprite;
            }


            return m_DefaultSprite;
        }
    }
}