using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapColliderVisualizer : MonoBehaviour {
    public TilemapCollider2D tilemapCollider;

    void OnDrawGizmos() {
        if (tilemapCollider == null) return;

        Gizmos.color = Color.green;
        Bounds bounds = tilemapCollider.bounds;
        Gizmos.DrawWireCube(bounds.center, bounds.size);
    }
}