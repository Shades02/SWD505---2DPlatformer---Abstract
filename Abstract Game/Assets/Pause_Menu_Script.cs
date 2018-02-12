using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_Menu_Script : MonoBehaviour
{
    static public bool isPaused = false;

    public GameObject pauseMenuCanvas;
	
	void Update ()
    {
        //escape key to pause
		if(Input.GetKeyDown(KeyCode.Escape))        
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
}
