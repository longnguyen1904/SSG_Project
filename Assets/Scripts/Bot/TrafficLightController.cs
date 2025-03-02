using UnityEngine;
using System.Collections;

public class TrafficLight : MonoBehaviour {
    public float greenTime = 5f;  // Thời gian đèn xanh
    public float redTime = 5f;    // Thời gian đèn đỏ
    private bool isGreen = true;  // Đèn đang xanh hay đỏ?

    void Start() {
        StartCoroutine(TrafficLightCycle());
    }

    IEnumerator TrafficLightCycle() {
        while (true)
        {
            isGreen = true; // Đèn xanh
            yield return new WaitForSeconds(greenTime);

            isGreen = false; // Đèn đỏ
            yield return new WaitForSeconds(redTime);
        }
    }

    public bool CanGo() {
        return isGreen;
    }
}
