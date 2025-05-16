using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace DualGridTile
{
    [ExecuteAlways]
    [RequireComponent(typeof(Tilemap))]
    public class DualGrid : MonoBehaviour
    {
        Vector3Int[] offsets = new Vector3Int[]
        {
            new Vector3Int(0, 1, 0),
            new Vector3Int(1, 1, 0),
            new Vector3Int(0, 0, 0),
            new Vector3Int(1, 0, 0)
        };


        private Tilemap reference;
        private bool _isEventHooked = false;

        public DualGridRuleTile Rule;
        private DualGridRuleTile rule;
        public Tilemap RenderTileMap;



        private void Awake()
        {
            reference = GetComponent<Tilemap>();
            reference.origin = Vector3Int.zero;
            RenderTileMap.origin = Vector3Int.zero;
        }

        private void OnEnable()
        {
            if (!_isEventHooked)
            {
                Tilemap.tilemapTileChanged += OnTilemapChanged;
                _isEventHooked = true;
                rule = Instantiate(Rule);
                rule.reference = reference;
            }
        }

        private void OnDisable()
        {
            if (_isEventHooked)
            {
                Tilemap.tilemapTileChanged -= OnTilemapChanged;
                _isEventHooked = false;
            }
        }

        void OnTilemapChanged(Tilemap changedTilemap, Tilemap.SyncTile[] changedTiles)
        {
            if (changedTilemap == reference)
            {
                RenderTile(changedTiles);
            }
        }

        void RenderTile(Tilemap.SyncTile[] changedTiles)
        {
            foreach (var tile in changedTiles)
            {
                Vector3Int basePos = tile.position;

                if (tile.tile != null)
                {
                    foreach (var off in offsets)
                    {
                        Vector3Int targetPos = basePos + off;
                        RenderTileMap.SetTile(targetPos, rule);
                    }
                }
                else
                {
                    TileRemoved(tile);
                }
            }
        }

        void TileRemoved(Tilemap.SyncTile tile)
        {
            foreach (var off in offsets)
            {
                Vector3Int targetPos = tile.position + off;

                if (!IsRenderHaveNeighbor(targetPos))
                {

                    RenderTileMap.SetTile(targetPos, null);
                }
            }

            
        }

        bool IsRenderHaveNeighbor(Vector3Int pos)
        {

            foreach (var off in offsets)
            {
                Vector3Int refPos = pos - off;

                if (reference.GetTile(refPos) != null)
                {
                    return true;
                }
            }

            return false;
            
        }
    }
}