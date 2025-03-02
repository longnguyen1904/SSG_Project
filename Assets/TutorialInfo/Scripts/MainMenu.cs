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


    public void OnApplicationQuit()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
