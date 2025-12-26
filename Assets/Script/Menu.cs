using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [Header("All Menu's")]
    public GameObject PauseMenuUI;
    public GameObject EndGameMenuUI;
    public GameObject ObjectiveMenuUI;

    public static bool GameIsStopped = false;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsStopped)
            {
                Resume();
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Pause();
                Cursor.lockState = CursorLockMode.None;
            }
        }
        else if(Input.GetKeyDown("m"))
        {
            if(GameIsStopped)
            {
                removeObjective();
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                showObjective();
                Cursor.lockState = CursorLockMode.None;
            }
        }
        
    }

    public void showObjective()
    {
        ObjectiveMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsStopped = true;
    }

    public void removeObjective()
    {
        ObjectiveMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        GameIsStopped = false;
    }
    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        GameIsStopped = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu"); 
    }

    public void QuitGame()
    {
        Debug.Log("OUT OUT OUT OUT OUT OUT........");
        Application.Quit();
    }
    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsStopped = true;
    }
}
