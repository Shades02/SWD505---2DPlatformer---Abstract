using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Script : MonoBehaviour
{
    public enum pickupType
    {
        ammo, health
    }

    public pickupType thisPickupType;

    private SpriteRenderer myRenderer;

	void Start ()
    {
        myRenderer = GetComponent<SpriteRenderer>();

        switch (thisPickupType)
        {
            case pickupType.ammo:
                myRenderer.color = Color.black;         //black = ammo currently
                break;
            case pickupType.health:
                myRenderer.color = Color.magenta;       //magenta = health currently
                break;
        }
    }
}
