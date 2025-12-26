using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Endgame : MonoBehaviour
{
    public void OnPlayAgianBt()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void OnQuitBtn() 
    {
        Debug.Log("Quiting game......");
        Application.Quit();
    }

}
