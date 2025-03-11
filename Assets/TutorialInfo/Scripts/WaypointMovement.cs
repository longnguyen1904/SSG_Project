using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaypointMovement : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 3f;
    private int currentWaypointIndex = 0;
    public float respawnTime = 2f;

    private SpriteRenderer spriteRenderer;
    private Collider2D npcCollider;

    [SerializeField] public List<Collider2D> touchedObjects = new List<Collider2D>();

    public Traffic_number_setting trafficLight; // 📌 Thêm biến để kiểm tra đèn giao thông

    private bool isStopped = false; // 📌 Kiểm tra xem NPC có đang bị dừng không

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        npcCollider = GetComponent<Collider2D>();

        if (waypoints.Length > 0)
        {
            transform.position = waypoints[0].position;
        }
        else
        {
            Debug.LogError("Waypoint list is empty! Add waypoints in the Inspector.");
        }
    }

    private void Update()
    {
        if (waypoints.Length == 0 || isStopped) return; // 📌 Nếu đang dừng thì không di chuyển

        if (currentWaypointIndex < waypoints.Length)
        {
            Transform targetWaypoint = waypoints[currentWaypointIndex];
            transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
            {
                currentWaypointIndex++;

                if (currentWaypointIndex >= waypoints.Length)
                {
                    StartCoroutine(RespawnNPC());
                }
            }
        }
    }

    private IEnumerator RespawnNPC()
    {
        spriteRenderer.enabled = false;
        npcCollider.enabled = false;

        yield return new WaitForSeconds(respawnTime);

        transform.position = waypoints[0].position;
        currentWaypointIndex = 1;

        spriteRenderer.enabled = true;
        npcCollider.enabled = true;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Chạm vào: " + other.gameObject.name);

        if (!touchedObjects.Contains(other))
        {
            touchedObjects.Add(other);
            other.isTrigger = true;
        }

        if (other.CompareTag("Tilemap")) // 📌 Nếu chạm vào tilemap kiểm tra đèn giao thông
        {
            if (trafficLight != null && !trafficLight.IsGreenLight())
            {
                isStopped = true; // Dừng NPC nếu đèn đỏ
            }
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Rời khỏi: " + other.gameObject.name);

        if (touchedObjects.Contains(other))
        {
            touchedObjects.Remove(other);
            other.isTrigger = false;
        }

        if (other.CompareTag("Tilemap")) // 📌 Nếu rời khỏi tilemap hoặc đèn xanh thì tiếp tục di chuyển
        {
            if (trafficLight != null && trafficLight.IsGreenLight())
            {
                isStopped = false; // Tiếp tục di chuyển nếu đèn xanh
            }
        }
    }
}

