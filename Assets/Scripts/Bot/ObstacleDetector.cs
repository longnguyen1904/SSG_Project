using UnityEngine;

public class ObstacleDetector : MonoBehaviour
{
    public bool isBlocked = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car") || other.CompareTag("Pedestrian"))
        {
            isBlocked = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Car") || other.CompareTag("Pedestrian"))
        {
            isBlocked = false;
        }
    }
}
