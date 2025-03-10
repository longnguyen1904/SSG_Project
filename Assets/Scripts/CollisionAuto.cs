using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapColliderManager : MonoBehaviour {
    public Tilemap tilemap;
    public TileBase[] tilesWithCollider; // Danh sách Tile cần Collider

    void Start() {
        TilemapCollider2D tilemapCollider = tilemap.GetComponent<TilemapCollider2D>();
        if (tilemapCollider == null) {
            tilemapCollider = tilemap.gameObject.AddComponent<TilemapCollider2D>();
        }

        BoundsInt bounds = tilemap.cellBounds;

        foreach (Vector3Int pos in bounds.allPositionsWithin) {
            TileBase tile = tilemap.GetTile(pos);
            if (tile != null) {
                TileFlags tileFlags = tilemap.GetTileFlags(pos);

                if (System.Array.Exists(tilesWithCollider, t => t == tile)) {
                    tilemap.SetColliderType(pos, Tile.ColliderType.Sprite); // Bật Collider
                } else {
                    tilemap.SetColliderType(pos, Tile.ColliderType.None); // Tắt Collider
                }
            }
        }
    }
}
