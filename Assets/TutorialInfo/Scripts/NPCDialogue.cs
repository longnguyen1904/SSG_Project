using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPCDialogue : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public Image dialogueBox;
    public string message = "Xin chào! Bạn cần giúp gì không?";
    public float typingSpeed = 0.05f;
    private float displayTime = 2f;
    private float repeatTime = 10f;

    private Coroutine dialogueCoroutine;

    void Start()
    {
        textMesh.gameObject.SetActive(false);
        dialogueBox.gameObject.SetActive(false);

        if (dialogueCoroutine == null) // Đảm bảo chỉ chạy một lần
        {
            dialogueCoroutine = StartCoroutine(DialogueLoop());
        }
    }

    IEnumerator DialogueLoop()
    {
        while (true)
        {
            yield return StartCoroutine(ShowDialogue());
            yield return new WaitForSeconds(repeatTime);
        }
    }

    IEnumerator ShowDialogue()
    {
        dialogueBox.gameObject.SetActive(true);
        textMesh.gameObject.SetActive(true);
        textMesh.text = "";

        foreach (char letter in message)
        {
            textMesh.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(displayTime);
        textMesh.gameObject.SetActive(false);
        dialogueBox.gameObject.SetActive(false);
    }
}
