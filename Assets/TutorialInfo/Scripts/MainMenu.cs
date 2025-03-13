using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update


    private void Start()
    {

    }
    public void toLV2(string ScenceName)
    {
        SceneManager.LoadScene("LV2");
    }
    public void ChangScene(string ScenceName) {
        SceneManager.LoadScene("LV1");
    }

    public void ToSetting(string name) {
        SceneManager.LoadScene("Setting");
    }

    public void Tomainmenu(string name){
        SceneManager.LoadScene("Main"); 
    }

    public void Toturtorial(string name) {
        SceneManager.LoadScene("Turtorial");  
    }
<<<<<<< HEAD
    public void ToTutorial1(string name) {
        SceneManager.LoadScene("tutorial1");  
    }
=======

>>>>>>> parent of 5996489 (Merge branch 'Long_not_main' into LOngg)

    public void OnApplicationQuit()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
