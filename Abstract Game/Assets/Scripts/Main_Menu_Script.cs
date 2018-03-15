﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main_Menu_Script : MonoBehaviour
{
    public GameObject settingsMenuCanvas;
    public GameObject[] mainMenu;

    private bool canInteract = true;
    private int selectedButton = 0;
	
    //Button functions
    public void startGame()
    {
        //currently loads scene 1, the only other scene
        SceneManager.LoadScene(1);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void settings()
    {
        settingsMenuCanvas.SetActive(true);
    }
    //End

    private void Update()
    {
        float controllerInput = (float)Input.GetAxis("Vertical");

        if(controllerInput != 0 && canInteract)
        {
            canInteract = false;    //stops multiple movements on the menu
            StartCoroutine(menuChange(controllerInput));
        }
        mainMenu[selectedButton].GetComponent<Button>().Select();
    }

    IEnumerator menuChange(float input)
    {
        if (input < 0 && selectedButton < mainMenu.Length - 1)
            selectedButton++;
        else if (input > 0 && selectedButton > 0)
            selectedButton--;

        yield return new WaitForSecondsRealtime(0.2f);
        canInteract = true;     //now you move again
        StopCoroutine(menuChange(0));
    }
}
