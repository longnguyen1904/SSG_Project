using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public GameObject[] characters; // Danh sách nhân vật trong Hierarchy
    public GameObject selectionCanvas; // Canvas chứa các button chọn nhân vật

    public void SelectCharacter(int index)
    {
        if (index >= 0 && index < characters.Length)
        {
            characters[index].SetActive(true); // Bật nhân vật được chọn
            selectionCanvas.SetActive(false); // Ẩn Canvas chứa các button
        }
    }
}
