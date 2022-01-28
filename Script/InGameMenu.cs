using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    
    public GameObject restartMenuUI;
    public GameObject winMenuUI;
    public GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume ()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        ((Behaviour)GameObject.Find("Main Camera").GetComponent("MouseLook")).enabled = true;
        ((Behaviour)GameObject.Find("Stamina Bar").GetComponent("Stamina")).enabled = true;
    }
    void Pause ()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        ((Behaviour)GameObject.Find("Main Camera").GetComponent("MouseLook")).enabled = false;
        ((Behaviour)GameObject.Find("Stamina Bar").GetComponent("Stamina")).enabled = false;
        Cursor.lockState = CursorLockMode.None;
    }


    public void DeathMenu()
    {
        // do something
        restartMenuUI.SetActive(true);
        Time.timeScale = 0f;
        ((Behaviour)GameObject.Find("Main Camera").GetComponent("MouseLook")).enabled = false;
        ((Behaviour)GameObject.Find("Stamina Bar").GetComponent("Stamina")).enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void WinMenu ()
    {
        winMenuUI.SetActive(true);
        Time.timeScale = 0f;
        ((Behaviour)GameObject.Find("Main Camera").GetComponent("MouseLook")).enabled = false;
        ((Behaviour)GameObject.Find("Stamina Bar").GetComponent("Stamina")).enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadMenu()

    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    // Player press "Try Again", enable the code back
    public void PlayGame()
    {
        SceneManager.LoadScene("BasicWorkingHospital");
        Time.timeScale = 1f;
        GameIsPaused = false;
        ((Behaviour)GameObject.Find("Main Camera").GetComponent("MouseLook")).enabled = true;
        ((Behaviour)GameObject.Find("Stamina Bar").GetComponent("Stamina")).enabled = true;
    }

    
}