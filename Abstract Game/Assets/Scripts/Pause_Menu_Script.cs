using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_Menu_Script : MonoBehaviour
{
    static public bool isPaused = false;

    public GameObject pauseMenuCanvas;
    public GameObject settingsMenuCanvas;
	
	void Update ()
    {
        //escape key to pause
		if(Input.GetButtonDown("Pause"))        
        {
            if (isPaused) resume();
            else pause();
        }
	}

    public void resume()
    {
        pauseMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void pause()
    {
        pauseMenuCanvas.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void settings()
    {
        settingsMenuCanvas.SetActive(true);
    }
}
