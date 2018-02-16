using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Script : MonoBehaviour
{
    private Player_Script player;
    private float levelTimer = 0;
    public GameObject heartContainer;

    public Text ammoText;
    public Text colourText;
    public Text timerText;

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Script>();
	}
	
	void Update ()
    {
        //timer
        levelTimer += Time.deltaTime;
        int seconds = Mathf.RoundToInt(levelTimer % 60);
        int minutes = Mathf.RoundToInt(levelTimer - seconds);
        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");

        //health
        switch(player.getHealth())
        {
            case 0:
                heartContainer.transform.GetChild(0).gameObject.SetActive(false);
                heartContainer.transform.GetChild(1).gameObject.SetActive(false);
                heartContainer.transform.GetChild(2).gameObject.SetActive(false);
                heartContainer.transform.GetChild(3).gameObject.SetActive(false);
                heartContainer.transform.GetChild(4).gameObject.SetActive(false);
                break;
            case 1:
                heartContainer.transform.GetChild(0).gameObject.SetActive(true);
                heartContainer.transform.GetChild(1).gameObject.SetActive(false);
                heartContainer.transform.GetChild(2).gameObject.SetActive(false);
                heartContainer.transform.GetChild(3).gameObject.SetActive(false);
                heartContainer.transform.GetChild(4).gameObject.SetActive(false);
                break;
            case 2:
                heartContainer.transform.GetChild(0).gameObject.SetActive(true);
                heartContainer.transform.GetChild(1).gameObject.SetActive(true);
                heartContainer.transform.GetChild(2).gameObject.SetActive(false);
                heartContainer.transform.GetChild(3).gameObject.SetActive(false);
                heartContainer.transform.GetChild(4).gameObject.SetActive(false);
                break;
            case 3:
                heartContainer.transform.GetChild(0).gameObject.SetActive(true);
                heartContainer.transform.GetChild(1).gameObject.SetActive(true);
                heartContainer.transform.GetChild(2).gameObject.SetActive(true);
                heartContainer.transform.GetChild(3).gameObject.SetActive(false);
                heartContainer.transform.GetChild(4).gameObject.SetActive(false);
                break;
            case 4:
                heartContainer.transform.GetChild(0).gameObject.SetActive(true);
                heartContainer.transform.GetChild(1).gameObject.SetActive(true);
                heartContainer.transform.GetChild(2).gameObject.SetActive(true);
                heartContainer.transform.GetChild(3).gameObject.SetActive(true);
                heartContainer.transform.GetChild(4).gameObject.SetActive(false);
                break;
            case 5:
                heartContainer.transform.GetChild(0).gameObject.SetActive(true);
                heartContainer.transform.GetChild(1).gameObject.SetActive(true);
                heartContainer.transform.GetChild(2).gameObject.SetActive(true);
                heartContainer.transform.GetChild(3).gameObject.SetActive(true);
                heartContainer.transform.GetChild(4).gameObject.SetActive(true);
                break;
            default:
                break;
        }

        //ammo
        ammoText.text = player.getAmmo().ToString("00");

        //colour
        colourText.text = "Colour: " + player.getColour();
    }
}
