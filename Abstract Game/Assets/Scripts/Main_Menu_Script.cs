using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu_Script : MonoBehaviour
{
    private Sound_Manager_Script soundManager;

	void Start ()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<Sound_Manager_Script>();
	}
	
    public void startGame()
    {
        //currently loads scene 1, the onyl scene
        SceneManager.LoadScene(1);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
