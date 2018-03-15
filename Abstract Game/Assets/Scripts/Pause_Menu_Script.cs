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
		if(Input.GetButtonDown("Pause") && !inSettingsMenu)             //if in game, pauses, if already paused, resumes game
        {
            if (isPaused) resume();
            else pause();
        }
        else if(Input.GetButtonDown("Pause") && inSettingsMenu)         //causes pressing pause (esc) from settings to jump back to puase
        {
            returnToPause();
        }
	}

    public void resume()            //resume game from pause menu
    {
        pauseMenuCanvas.SetActive(false);
        inGameUI.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }

    private void pause()            //pause from in game
    {
        pauseMenuCanvas.SetActive(true);
        inGameUI.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }
            
    public void quitGame()              //quit the application
    {
        Application.Quit();
    }

    public void settings()              //access the settings menu, from pause menu
    {
        inSettingsMenu = true;
        settingsMenuCanvas.SetActive(true);
        pauseMenuCanvas.SetActive(false);
    }

    public void returnToPause()         //return to pause menu, from settings menu
    {
        inSettingsMenu = false;
        settingsMenuCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(true);
    }

    public void setFullscreen(bool toggle)          //toggle fullscreen mode
    {
        Screen.fullScreen = toggle;
    }

    public void setLowQuality(bool toggle)          //toggle performance mode
    {
        if (toggle) QualitySettings.SetQualityLevel(0);
        else QualitySettings.SetQualityLevel(5);
    }

    public void restartLevel()      //restarts the current level
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);       //reload current scene
    }

    public void returnToMenu()      //returns you to the main menu
    {
        SceneManager.LoadScene(0);  //scene 0 is the main menu
    }
}
