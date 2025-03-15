using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject characterSelectionUI; // Canvas ch?n nhân v?t
    public GameObject[] characters; // M?ng ch?a các Prefab nhân v?t
    private GameObject selectedCharacter; // Nhân v?t ???c ch?n
    public CinemachineVirtualCamera Vcam;
    public CinemachineVirtualCamera Mcam;



    void Start()
    {

        if (characterSelectionUI == null)
        {
            Debug.LogError("⚠️ Lỗi: Chưa gán Canvas vào characterSelectionUI!");
        }

        if (characters == null || characters.Length == 0)
        {
            Debug.LogError("⚠️ Lỗi: Chưa gán nhân vật vào characters[] trong Inspector!");
        }

        // Ẩn tất cả nhân vật
        foreach (GameObject character in characters)
        {
            if (character != null)
            {
                character.SetActive(false);
            }
            else
            {
                Debug.LogError("⚠️ Nhân vật trong danh sách bị NULL!");
            }
        }

        // Hiện UI chọn nhân vật
        characterSelectionUI?.SetActive(true);
    }

    public void SelectCharacter(int characterIndex)
    {
        if (characterSelectionUI != null)
        {
            characterSelectionUI.SetActive(false);
        }

        if (characters != null && characterIndex >= 0 && characterIndex < characters.Length)
        {
            if (characters[characterIndex] != null)
            {
                // Gán nhân vật được chọn
                selectedCharacter = characters[characterIndex];

                // Hiển thị nhân vật
                selectedCharacter.SetActive(true);
                Debug.Log("✅ Nhân vật được chọn: " + selectedCharacter.name);

                // Gán Virtual Camera
                if (Vcam != null)
                {
                    Vcam.Follow = selectedCharacter.transform;
                    Vcam.LookAt = selectedCharacter.transform;
                    Debug.Log("📷 Virtual Camera đã follow: " + selectedCharacter.name);
                }
                else
                {
                    Debug.LogError("⚠️ Virtual Camera chưa được gán trong Inspector!");
                }

                // Gán Virtual Camera
                if (Mcam != null)
                {
                    Mcam.Follow = selectedCharacter.transform;
                    Mcam.LookAt = selectedCharacter.transform;
                    Debug.Log("📷 Virtual Camera đã follow: " + selectedCharacter.name);
                }
                else
                {
                    Debug.LogError("⚠️ Virtual Camera chưa được gán trong Inspector!");
                }
            }
            else
            {
                Debug.LogError("⚠️ Lỗi: Nhân vật tại index " + characterIndex + " bị NULL!");
            }
        }
        else
        {
            Debug.LogError("⚠️ Index chọn nhân vật không hợp lệ!");
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
    // Gán nhân vật vào Virtual Camera để theo dõi

}
