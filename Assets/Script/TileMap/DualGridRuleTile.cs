using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DualGridTile
{
    [Serializable]
    public class DualGridRuleTile : TileBase
    {
        public Sprite m_DefaultSprite;
        public Tile.ColliderType m_DefaultColliderType = Tile.ColliderType.Sprite;
        [SerializeField]
        public List<DualGridTilingRule> m_TilingRules;

        [SerializeField]
        public class DualGridTilingRule
        {
            public Sprite m_sprite;

            public List<Vector3Int> m_NeighborPositions = new List<Vector3Int>()
            {
                new Vector3Int(0, 1, 0),
                new Vector3Int(1, 1, 0),
                new Vector3Int(0, 0, 0),
                new Vector3Int(1, 0, 0),
            };
        }

        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            tileData.sprite = m_DefaultSprite;
            tileData.colliderType = m_DefaultColliderType;
        }
    }
}