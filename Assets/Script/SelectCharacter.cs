using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectCharacter : MonoBehaviour
{

    public GameObject selectcharacter;
    public GameObject mainmenu;
    public void OnCharacter1()
    {
        SceneManager.LoadScene("ZombieLand");
    }

    public void OnCharacter2()
    {
        SceneManager.LoadScene("ZombieLand1");
    }

    public void OnCharacter3()
    {
        SceneManager.LoadScene("ZombieLand2");
    }

    public void OnBackBtn()
    {
        selectcharacter.SetActive(false);
        mainmenu.SetActive(true);   
    }
}
