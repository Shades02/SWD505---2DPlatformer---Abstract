using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Script : MonoBehaviour
{
    public enum pickupType
    {
        white, red, blue, green
    }

    public pickupType pickupColour;

    private SpriteRenderer myRenderer;

	void Start ()
    {
        myRenderer = GetComponent<SpriteRenderer>();

        switch (pickupColour)
        {
            case Pickup_Script.pickupType.white:
                myRenderer.color = Color.white;
                break;
            case Pickup_Script.pickupType.blue:
                myRenderer.color = Color.blue;
                break;
            case Pickup_Script.pickupType.green:
                myRenderer.color = Color.green;
                break;
            case Pickup_Script.pickupType.red:
                myRenderer.color = Color.red;
                break;
        }
    }
	
	void Update ()
    {
		
	}
}
