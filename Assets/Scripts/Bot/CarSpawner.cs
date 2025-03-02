using UnityEngine;

public class CarSpawner : MonoBehaviour {
    public GameObject carPrefab;
    public TrafficNode startNode;
    public float spawnInterval = 3f;

    private void Start() {
        InvokeRepeating(nameof(SpawnCar), 0f, spawnInterval);
    }

    void SpawnCar() {
        GameObject car = Instantiate(carPrefab, startNode.transform.position, Quaternion.identity);
        MoveCar moveCar = car.GetComponent<MoveCar>();
        if (moveCar != null) {
            moveCar.currentNode = startNode;
        }
    }
}
