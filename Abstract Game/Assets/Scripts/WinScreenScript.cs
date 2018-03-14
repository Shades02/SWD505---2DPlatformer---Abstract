using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenScript : MonoBehaviour
{
    public void returnToMenu()
    {
        SceneManager.LoadScene(0);  //scene 0 is the main menu
    }
}
