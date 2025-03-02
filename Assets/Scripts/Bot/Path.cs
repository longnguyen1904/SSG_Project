using UnityEngine;
using System.Collections.Generic;

public class Path : MonoBehaviour {
    public List<Transform> waypoints;

    public Transform GetClosestWaypoint(Vector2 position) {
        Transform closest = null;
        float minDist = Mathf.Infinity;

        foreach (Transform wp in waypoints) {
            float dist = Vector2.Distance(position, wp.position);
            if (dist < minDist) {
                minDist = dist;
                closest = wp;
            }
        }
        return closest;
    }
}
