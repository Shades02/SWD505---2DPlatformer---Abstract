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
            case colour.green:
                myRenderer.color = Color.green;
                break;
            case colour.red:
                myRenderer.color = Color.red;
                break;
        }
    }

    void Update ()
    {
		
	}
}
