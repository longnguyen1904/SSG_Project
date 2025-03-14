using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    public float speed = 5f; // Tốc độ di chuyển
    private Vector2 inputVector = Vector2.zero;

    void Update()
    {
        // Lấy giá trị đầu vào từ bàn phím
        inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // Giới hạn độ dài vector trong khoảng (-1,1) để tránh tăng tốc độ khi di chuyển chéo
        inputVector = inputVector.normalized;
    }

    public Vector2 GetDirection()
    {
        return inputVector;
    }
}
