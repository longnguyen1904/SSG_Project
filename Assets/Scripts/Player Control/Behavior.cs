using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Behavior : MonoBehaviour {
    public Movement movement { get; private set;}
    private float rotationSpeed = 200f; // Tốc độ xoay xe
    private float currentRotation = 0f; // Góc quay của xe
    private float MAXSPEED = 30f;
    private float MINSPEED = 0f;
    private float decelerationRate = 5f;
    private float accelerationRate = 6f;

    // gear true: số tiến, gear false: số lùi
    public bool gear = true;
    
    /*
    force: Phát hiện va chạm
    force true: khi va chạm, force false: khi không va chạm
    */ 
    public bool force = false;

    // check false: Chỉ được lùi, check true: Chỉ được tiến
    public bool check = true;

    private void Awake() {
        this.movement = GetComponent<Movement>();

        Debug.Log(movement == null ? "Movement is null" : "Movement is assigned");
    }
    

    private void Update() {
        // Xác định hướng di chuyển dựa trên góc quay hiện tại
        Vector2 moveDirection = transform.right;
        // Set gear
        if (movement.speed == 0) {
            setGear();     
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

            collisionHandler(moveDirection);

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

    public void collisionHandler(Vector2 moveDirection) {
        // Gear
        if (!force) {
            if (gear) {
                moveForwad(moveDirection);
            } else {
                movebackWard(moveDirection);
            }
        } else {
            setGear();
            if (gear && check) {
                moveForwad(moveDirection);
            }
            if (!(gear && check)) {
                movebackWard(moveDirection);
            }
        }
    }

    public void moveForwad(Vector2 moveDirection) {
        transform.position += (Vector3)(moveDirection * movement.speed * Time.deltaTime);
    }

    public void movebackWard(Vector2 moveDirection) {
        transform.position -= (Vector3)(moveDirection * movement.speed * Time.deltaTime);
    }

    public void OnCollisionEnter2D() {
        movement.speed = 0;
        if (gear) {
            this.check = false;
            this.force = true;
        } else {
            this.check = true;
            this.force = true;
        }
    }

    public void setGear() {
        // Gear tiến
        if (Input.GetKey(KeyCode.J) && !gear) {
            gear = true;
            MAXSPEED = 30f;
        }
        // Gear lùi
        if (Input.GetKey(KeyCode.K) && gear) {
            gear = false;
            MAXSPEED = 5f;
        }       
    }

    public void OnCollisionExit2D() {
        this.force = false;
    }
}
