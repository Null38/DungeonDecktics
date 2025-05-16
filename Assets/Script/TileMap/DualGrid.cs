using UnityEngine;
using UnityEngine.Tilemaps;

namespace DualGridTile
{
    [ExecuteAlways]
    [RequireComponent(typeof(Tilemap))]
    public class DualGrid : MonoBehaviour
    {
        private Tilemap reference;
        private bool _isEventHooked = false;

        public DualGridRuleTile Rule;
        public Tilemap RenderTileMap;



        private void Awake()
        {
            reference = GetComponent<Tilemap>();
        }

        private void OnEnable()
        {
            if (!_isEventHooked)
            {
                Tilemap.tilemapTileChanged += OnTilemapChanged;
                _isEventHooked = true;
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
                Debug.Log($"Tile changed at position {tile.position}");
                if (tile.tile != null)
                {
                    RenderTileMap.SetTile(tile.position, Rule);
                }
                else
                {
                    RenderTileMap.SetTile(tile.position, null);
                }
            }
        }
    }
}