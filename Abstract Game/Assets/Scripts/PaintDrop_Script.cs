using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintDrop_Script : Timed_Object_Script
{
    public colour currentColour;                    //this contains all the colours, however the drop will only be used as red/blue/yellow

	void Start ()
    {
        Colour_Changer_Script.setColourWithoutLayer(gameObject, currentColour);
    }
	
}
