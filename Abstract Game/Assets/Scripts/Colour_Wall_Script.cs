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
                gameObject.layer = 20;
                break;
            case colour.blue:
                myRenderer.color = Color.blue;
                gameObject.layer = 23;
                break;
            case colour.yellow:
                myRenderer.color = Color.yellow;
                gameObject.layer = 24;
                break;
            case colour.red:
                myRenderer.color = Color.red;
                gameObject.layer = 22;
                break;
            case colour.green:
                myRenderer.color = Color.green;
                gameObject.layer = 25;
                break;
            case colour.purple:
                myRenderer.color = Color.magenta;
                gameObject.layer = 27;
                break;
            case colour.orange:
                myRenderer.color = Color.grey;
                gameObject.layer = 26;
                break;
            case colour.black:
                myRenderer.color = Color.black;
                gameObject.layer = 21;
                break;
        }

    }

}
