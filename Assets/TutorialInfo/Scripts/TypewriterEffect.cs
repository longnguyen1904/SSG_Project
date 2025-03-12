using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI; // Dùng đúng thư viện UI

public class TypewriterEffect : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float typingSpeed = 0.05f;
    private string fullText;

    
   
    

    void Start()
    {
        fullText = textMeshPro.text; // Lưu nội dung gốc
        textMeshPro.text = ""; // Xóa nội dung ban đầu
        
        StartCoroutine(TypeText());
        
       
    }

    IEnumerator TypeText()
    {
        
        foreach (char letter in fullText)
        {
            textMeshPro.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
       
        
    }
}
