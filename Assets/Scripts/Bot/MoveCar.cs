using UnityEngine;
using System.Collections.Generic;

public class MoveCar : MonoBehaviour {
    public float speed = 5f;
    public TrafficNode currentNode;
    private List<Transform> currentWaypoints = new List<Transform>();
    public int waypointIndex = 0;
    private Transform nextNode;

    void Start() {
        if (currentNode == null) return;
        SetNextNode(currentNode.GetRandomNextNode());
    }

    void Update() {
        if (currentWaypoints.Count > 0) {
            MoveThroughWaypoints();
        } else {
            MoveToNextNode();
        }

        if (currentNode.isEndNode) {
            Destroy(gameObject);
        }
    }

    void SetNextNode(Transform newNode) {
        // if (newNode == null) return;

        nextNode = newNode;
        waypointIndex = 0;
        currentWaypoints = currentNode.GetWaypointsForNode(nextNode);
    }

    void MoveThroughWaypoints() {
        if (currentWaypoints == null || currentWaypoints.Count == 0) {
            MoveToNextNode();
            return;
        }

        if (waypointIndex >= currentWaypoints.Count) {
            MoveToNextNode();
            return;
        }

        Transform targetWaypoint = currentWaypoints[waypointIndex];

        // Di chuyển xe đến waypoint hiện tại
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        // Kiểm tra nếu đã đến waypoint thì tăng index
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f) {
            waypointIndex++;
        }
    }

    void MoveToNextNode() {
        if (nextNode == null) return;

        currentNode = nextNode.GetComponent<TrafficNode>();
        nextNode = currentNode.GetRandomNextNode();
        currentWaypoints = currentNode.GetWaypointsForNode(nextNode);
        waypointIndex = 0;
    }
}
