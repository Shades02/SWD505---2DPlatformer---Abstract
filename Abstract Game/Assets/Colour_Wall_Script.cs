using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colour_Wall_Script : MonoBehaviour
{
    public colour wallColour;

    private SpriteRenderer myRenderer;

    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();

        switch (wallColour)
        {
            case colour.white:
                myRenderer.color = Color.white;
                break;
            case colour.blue:
                myRenderer.color = Color.blue;
                break;
            case colour.yellow:
                myRenderer.color = Color.yellow;
                break;
            case colour.red:
                myRenderer.color = Color.red;
                break;
            case colour.green:
                myRenderer.color = Color.green;
                break;
            case colour.purple:
                myRenderer.color = Color.magenta;
                break;
            case colour.orange:
                myRenderer.color = Color.grey;
                break;
            case colour.black:
                myRenderer.color = Color.black;
                break;
        }
    }

    void Update ()
    {
		
	}
}
