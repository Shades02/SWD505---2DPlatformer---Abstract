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

    public GameObject[] pauseMenu;
    public GameObject[] settingsMenu;
    public GameObject[] gameOverMenu;

    private bool playerIsDead = false;

    private bool canInteract = true;
    private int selectedButton = 0;

    private void Start()
    {
        fullscreenButton.GetComponent<Toggle>().isOn = Screen.fullScreen;
    }

    void Update ()
    {
        //check for player death
        if(!GameObject.Find("Player"))          //if the player is dead, hes been destroyed
        {
            //This means the game over screen is up
            //We need to use that for the controller selecting buttons

            playerIsDead = true;
        }

        //escape key to pause
		if(Input.GetButtonDown("Pause") && !inSettingsMenu)             //if in game, pauses, if already paused, resumes game
        {
            if (isPaused) resume();
            else
            {
                selectedButton = 0;
                pause();            
            }
        }
        else if(Input.GetButtonDown("Pause") && inSettingsMenu)         //causes pressing pause (esc) from settings to jump back to puase
        {
            returnToPause();
        }

        if(isPaused || playerIsDead)        //if in a menu, check for controller for buttons
        {
            //Controller/Keyboard support
            float controllerInput = (float)Input.GetAxis("Vertical");

            if (controllerInput != 0 && canInteract)
            {
                canInteract = false;    //stops multiple movements on the menu
                StartCoroutine(menuChange(controllerInput));
            }

            if(playerIsDead)                //is player is dead, its on the game over screen
            {
                gameOverMenu[selectedButton].GetComponent<Button>().Select();
            }
            else if (!inSettingsMenu)       //if not in settings, must be in normal pause menu
            {
                pauseMenu[selectedButton].GetComponent<Button>().Select();
            }          
            else if(inSettingsMenu)         // in the settings menu
            {
                if (selectedButton == 0 || selectedButton == 1)
                {
                    settingsMenu[selectedButton].GetComponent<Toggle>().Select();
                }
                else
                {
                    settingsMenu[selectedButton].GetComponent<Button>().Select();
                }
            } 
        }
    }

    IEnumerator menuChange(float input)
    {
        if(!inSettingsMenu)             //pause
        {
            if (input < 0 && selectedButton < pauseMenu.Length - 1)
                selectedButton++;
            else if (input > 0 && selectedButton > 0)
                selectedButton--;
        }
        else if (inSettingsMenu)        //settings
        {   
            if (input < 0 && selectedButton < settingsMenu.Length - 1)
                selectedButton++;
            else if (input > 0 && selectedButton > 0)
                selectedButton--;
        }
        else if (playerIsDead)          //game over
        {
            if (input < 0 && selectedButton < gameOverMenu.Length - 1)
                selectedButton++;
            else if (input > 0 && selectedButton > 0)
                selectedButton--;
        }

        yield return new WaitForSecondsRealtime(0.2f);
        canInteract = true;     //now you move again
        StopCoroutine(menuChange(0));
    }

    //Button functions
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
    //End
}
