using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick_handle : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public RectTransform joystickBackground;  // Nền joystick
    public RectTransform joystickHandle;      // Nút joystick
    private Vector2 inputVector = Vector2.zero; // Vector điều hướng
    private Vector2 startPosition;             // Vị trí trung tâm của joystick

    private float moveRange; // Giới hạn joystick di chuyển (bán kính của background)

    private void Start()
    {
        startPosition = joystickBackground.localPosition; // Lấy vị trí trung tâm của joystickBackground
        moveRange = joystickBackground.sizeDelta.x / 2f; // Lấy bán kính của background
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData); // Gọi OnDrag ngay khi chạm vào
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBackground.parent as RectTransform, eventData.position, eventData.pressEventCamera, out position);

        Vector2 offset = position - startPosition; // Khoảng cách từ joystick đến trung tâm
        float distance = offset.magnitude;

        if (distance > moveRange)  // Nếu joystickHandle ra ngoài background, giới hạn lại
        {
            offset = offset.normalized * moveRange;
        }

        inputVector = offset / moveRange; // Chuẩn hóa giá trị (-1 đến 1)
        joystickHandle.localPosition = offset; // Đặt vị trí joystickHandle bên trong background
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        joystickHandle.localPosition = Vector2.zero; // Reset joystick về trung tâm
    }

    public Vector2 GetDirection()
    {
        return inputVector;
    }
}
