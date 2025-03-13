using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public CanvasGroup fadeCanvas; // Kéo Image vào đây
    public float fadeDuration = 1f; // Thời gian chuyển cảnh

    void Start()
    {
        StartCoroutine(FadeIn()); // Hiệu ứng xuất hiện khi vào Scene
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName)); // Chuyển cảnh khi gọi hàm này
    }

    IEnumerator FadeIn()
    {
        fadeCanvas.alpha = 1;
        while (fadeCanvas.alpha > 0)
        {
            fadeCanvas.alpha -= Time.deltaTime / fadeDuration;
            yield return null;
        }
    }

    IEnumerator FadeOut(string sceneName)
    {
        fadeCanvas.alpha = 0;
        while (fadeCanvas.alpha < 1)
        {
            fadeCanvas.alpha += Time.deltaTime / fadeDuration;
            yield return null;
        }
        SceneManager.LoadScene(sceneName);
        fadeCanvas.gameObject.SetActive(false);

    }
}
