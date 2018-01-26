using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colour_Wall_Script : MonoBehaviour
{
    public Pickup_Script.pickupType wallColour;

    private SpriteRenderer myRenderer;

    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();

        switch (wallColour)
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
