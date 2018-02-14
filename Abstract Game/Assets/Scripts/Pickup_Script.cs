using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Script : MonoBehaviour
{
    public enum pickupType
    {
        red, blue, yellow, ammo, health
    }

    public pickupType thisPickupType;

    private SpriteRenderer myRenderer;

	void Start ()
    {
        myRenderer = GetComponent<SpriteRenderer>();

        switch (thisPickupType)
        {
            case pickupType.blue:
                myRenderer.color = Color.blue;
                break;
            case pickupType.red:
                myRenderer.color = Color.red;
                break;
            case pickupType.yellow:
                myRenderer.color = Color.yellow;
                break;
            case pickupType.ammo:
                myRenderer.color = Color.black;         //black = ammo currently
                break;
            case pickupType.health:
                myRenderer.color = Color.magenta;       //magenta = health currently
                break;
        }
    }
	
	void Update ()
    {
		
	}
}
