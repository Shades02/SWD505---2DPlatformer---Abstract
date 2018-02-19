using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintDrop_Script : Timed_Object_Script
{
    public colour currentColour;

    private SpriteRenderer myRenderer;

	void Start ()
    {
        myRenderer = gameObject.GetComponent<SpriteRenderer>();

		switch(currentColour)
        {
            case colour.red:
                myRenderer.color = Color.red;
                break;
            case colour.blue:
                myRenderer.color = Color.blue;
                break;
            case colour.yellow:
                myRenderer.color = Color.yellow;
                break;
            default:
                break;
        }
	}
	
}
