using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colour_Wall_Script : MonoBehaviour
{
    public colour wallColour;
    public bool vertical;

    private SpriteRenderer myRenderer;

    void Start()
    {
        Colour_Changer_Script.setColour(gameObject, wallColour);

        if(vertical)
        {
            transform.Rotate(0, 0, 90);
        }
    }

}
