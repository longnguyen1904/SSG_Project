using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Behavior : MonoBehaviour {
    // private Vector2 lastDirection = Vector2.zero;
    // private Vector2 savedDirection = Vector2.zero;
    // private float changeDirectionCooldown = 0.1f;
    // private float timeSinceLastChange = 0f;
    public Movement movement { get; private set;}
    private float rotationSpeed = 200f; // Tốc độ xoay xe
    private float currentRotation = 0f; // Góc quay của xe
    private float MAXSPEED = 30f;
    private float MINSPEED = 0f;
    private float decelerationRate = 5f;
    private float accelerationRate = 6f;
    public bool gear = true;

    private void Awake()
    {
        this.movement = GetComponent<Movement>();

        Debug.Log(movement == null ? "Movement is null" : "Movement is assigned");
    }
    

    private void Update() {
        // Xác định hướng di chuyển dựa trên góc quay hiện tại
        Vector2 moveDirection = transform.right;
        // Set gear
        if (movement.speed == 0) {
            if (Input.GetKey(KeyCode.J) && !gear) {
                // Gear tiến
                gear = true;
                MAXSPEED = 30f;
            }
            if (Input.GetKey(KeyCode.K) && gear) {
                // Gear lùi
                gear = false;
                MAXSPEED = 5f;
            }            
        }
        // Update tốc theo deltaTime
        if (Input.GetKey(KeyCode.Space)) {
        // Tăng tốc dần lên MAXSPEED
            movement.speed = Mathf.MoveTowards(movement.speed, MAXSPEED, accelerationRate * Time.deltaTime);
            // transform.position += (Vector3)(moveDirection * movement.speed * Time.deltaTime);    
            this.movement.SetDirection(moveDirection);
        }
        if (Input.GetKey(KeyCode.LeftShift)){
        // Giảm tốc dần xuống MINSPEED
            // transform.position = (Vector3)(moveDirection * movement.speed * Time.deltaTime);
            movement.speed = Mathf.MoveTowards(movement.speed, MINSPEED, decelerationRate * Time.deltaTime);
        }

        if (movement.speed > 0) {
            moveDirection = transform.right; // Hướng mũi xe
            // Gear
            if (gear) {
                transform.position += (Vector3)(moveDirection * movement.speed * Time.deltaTime);
            } else {
                transform.position -= (Vector3)(moveDirection * movement.speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
                currentRotation += rotationSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
                currentRotation -= rotationSpeed * Time.deltaTime;
            }
        }

        Debug.Log($"Speed: {movement.speed}");
        
        // Cập nhật góc quay
        transform.rotation = Quaternion.Euler(0, 0, currentRotation);

        

        
    }

    public void ResetState()
    {
        this.movement.ResetState();
        this.gameObject.SetActive(true);
    }
}
