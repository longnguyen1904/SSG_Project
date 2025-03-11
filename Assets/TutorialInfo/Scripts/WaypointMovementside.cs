using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMovementside : MonoBehaviour
{
    public Transform[] waypoints; // Các điểm di chuyển
    public float maxSpeed = 5f; // Tốc độ tối đa
    public float acceleration = 2f; // Gia tốc tăng tốc
    public float deceleration = 3f; // Gia tốc giảm tốc
    public float turnSpeed = 3f; // Tốc độ quay đầu

    private int currentWaypointIndex = 0;
    private float currentSpeed = 0f; // Tốc độ hiện tại

    public Traffic_number_setting trafficLight;
    public GameObject tilemap;
    private bool isTouchingTilemap = false; // Xe có đang chạm Tilemap không?

    void Start()
    {
        if (waypoints.Length > 0)
        {
            transform.position = waypoints[0].position;
        }
    }

    void Update()
    {
        if (waypoints.Length == 0) return;

        bool shouldStop = isTouchingTilemap && trafficLight != null && trafficLight.isRedLight;

        if (shouldStop)
        {
            currentSpeed -= deceleration * Time.deltaTime;
            if (currentSpeed < 0) currentSpeed = 0;
        }
        else
        {
            currentSpeed += acceleration * Time.deltaTime;
            if (currentSpeed > maxSpeed) currentSpeed = maxSpeed;
        }

        if (currentWaypointIndex < waypoints.Length)
        {
            Transform targetWaypoint = waypoints[currentWaypointIndex];

            // 🚗 **Tính hướng di chuyển**
            Vector2 direction = (targetWaypoint.position - transform.position).normalized;

            // 🔄 **Tính góc quay đầu xe**
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // 🎯 **Quay từ từ về phía waypoint**
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

            // 🚦 **Chỉ di chuyển khi không bị dừng**
            if (!shouldStop)
            {
                transform.position += transform.right * currentSpeed * Time.deltaTime;
            }

            if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.5f)
            {
                currentWaypointIndex++;
            }
        }
        else
        {
            currentWaypointIndex = 0;
            transform.position = waypoints[0].position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (tilemap != null && other.gameObject == tilemap.gameObject)
        {
            isTouchingTilemap = true;
            Debug.Log("🚗 Chạm tilemap!");

            if (trafficLight != null && trafficLight.isRedLight)
            {
                Debug.Log("🚦 Đèn đỏ! Xe dừng lại.");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (tilemap != null && other.gameObject == tilemap.gameObject)
        {
            isTouchingTilemap = false;
            Debug.Log("🚗 Rời khỏi tilemap!");
        }
    }
}