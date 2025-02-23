using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour {
    public float speed = 0.0f;
    public LayerMask obstacleLayer;
    public Vector2 direction { get; private set;}
    public Vector2 nextDirection { get; private set;}
    
    
    public void SetDirection(Vector2 direction)
    {
        Debug.Log($"Setting direction to: {direction}");
        
        if (Occupied(direction)) {
            this.speed = 0.0f;
        } 
        
    }

    public bool Occupied(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.75f, 0.0f, direction, 1.5f, this.obstacleLayer);
        return hit.collider != null;
    }

   
}
