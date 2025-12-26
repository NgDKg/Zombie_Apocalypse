using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject selectcharacter;
    public GameObject mainmenu;

    public void OnPlayerBt()
    {
        selectcharacter.SetActive(true);
        mainmenu.SetActive(false);
    }

    //public void OnPlayerBtn()
    //{
    //    SceneManager.LoadScene("ZombieLand");
    //}
    
    public void OnQuitBtn() 
    {
        Debug.Log("Quiting game......");
        Application.Quit();
    }

}
