using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings_Script : MonoBehaviour
{
    public GameObject fullscreenButton;

    void Start ()
    {
        fullscreenButton.GetComponent<Toggle>().isOn = Screen.fullScreen;
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

    public void returnToMenu()
    {
        gameObject.SetActive(false);
    }

}
