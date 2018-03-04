using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause_Menu_Script : MonoBehaviour
{
    static public bool isPaused = false;
    static public bool inSettingsMenu = false;

    public GameObject inGameUI;
    public GameObject pauseMenuCanvas;
    public GameObject settingsMenuCanvas;

    public GameObject fullscreenButton;

    private void Start()
    {
        fullscreenButton.GetComponent<Toggle>().isOn = Screen.fullScreen;
    }

    void Update ()
    {
        //escape key to pause
		if(Input.GetButtonDown("Pause") && !inSettingsMenu)        
        {
            if (isPaused) resume();
            else pause();
        }
        else if(Input.GetButtonDown("Pause") && inSettingsMenu)
        {
            returnToPause();
        }
	}

    public void resume()
    {
        pauseMenuCanvas.SetActive(false);
        inGameUI.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void pause()
    {
        pauseMenuCanvas.SetActive(true);
        inGameUI.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void settings()
    {
        inSettingsMenu = true;
        settingsMenuCanvas.SetActive(true);
        pauseMenuCanvas.SetActive(false);
    }

    public void returnToPause()
    {
        inSettingsMenu = false;
        settingsMenuCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(true);
    }

    public void setFullscreen(bool toggle)
    {
        Screen.fullScreen = toggle;
    }

    public void setLowQuality(bool toggle)
    {
        if (toggle) QualitySettings.SetQualityLevel(0);
        else QualitySettings.SetQualityLevel(5);
    }

    public void restartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);       //reload current scene
    }

    public void returnToMenu()
    {
        SceneManager.LoadScene(0);  //scene 0 is the main menu
    }
}
