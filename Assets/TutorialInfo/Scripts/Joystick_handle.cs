using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick_handle : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public RectTransform joystickBackground;  // Nền joystick
    private RectTransform joystickHandle;    // Nút joystick
    private Vector2 inputVector = Vector2.zero; // Vector điều hướng
    private Vector2 startPosition;           // Vị trí gốc của joystick

    public float moveRange = 2f; // Giới hạn joystick di chuyển

    private void Start()
    {
        joystickHandle = GetComponent<RectTransform>();
        startPosition = joystickHandle.anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData); // Kéo ngay khi chạm vào
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBackground, eventData.position, eventData.pressEventCamera, out position);

        Vector2 offset = position - (Vector2)joystickBackground.anchoredPosition;
        float distance = offset.magnitude;

        if (distance > moveRange)
            offset = offset.normalized * moveRange;

        inputVector = offset / moveRange;
        joystickHandle.anchoredPosition = (Vector2)joystickBackground.anchoredPosition + offset;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        joystickHandle.anchoredPosition = startPosition; // Reset vị trí
    }

    public Vector2 GetDirection()
    {
        return inputVector;
    }
}
