using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public AudioSource audioSource; // Chứa âm thanh
    public Button button; // Nút bấm

    void Start()
    {
        if (button == null) button = GetComponent<Button>();
        if (audioSource == null) audioSource = GetComponent<AudioSource>();

        button.onClick.AddListener(PlaySound);
    }

    void PlaySound()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
