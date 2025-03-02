using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class WaypointGroup {
    public Transform nextNode; // Node đích
    public List<Transform> waypoints; // Danh sách waypoint đi qua trước khi tới nextNode
    public Transform finalNode; // Node cuối cùng (nextNode)

    public WaypointGroup(Transform nextNode, List<Transform> waypoints) {
        this.nextNode = nextNode;
        this.waypoints = waypoints;
        this.finalNode = this.nextNode; // Mặc định finalNode = nextNode
    }
}

public class TrafficNode : MonoBehaviour {
    public List<WaypointGroup> waypointGroups; // Danh sách đường đi

    public bool hasTrafficLight = false;
    public TrafficLight trafficLight;
    public bool isEndNode = false;

    public Transform GetRandomNextNode() {
        if (waypointGroups.Count == 0) return null;
        return waypointGroups[Random.Range(0, waypointGroups.Count)].nextNode;
    }

    public bool CanProceed() {
        if (!hasTrafficLight || trafficLight == null) return true;
        return trafficLight.CanGo();
    }

    public List<Transform> GetWaypointsForNode(Transform targetNode) {
        for (int i = 0; i < waypointGroups.Count; i++) {
            if (waypointGroups[i].nextNode == targetNode) {
                return waypointGroups[i].waypoints;
            }
        }
        return new List<Transform>();
    }
}
