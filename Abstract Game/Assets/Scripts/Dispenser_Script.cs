using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser_Script : MonoBehaviour
{
    public colour colourToDrop;
    public GameObject paintDrop;
    public bool facingRight;

	void Start ()
    {
        if (facingRight)        //asset for dispenser faces left by default
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }

        Colour_Changer_Script.setColourWithoutLayer(gameObject, colourToDrop);      //sets the colour, but not the layer so it can still be interacted with
    }

    public void dispense()
    {
        Vector3 dispensePoint; 

        if (facingRight)
        {
            dispensePoint = new Vector3(transform.position.x + 1.0f, transform.position.y + 0.5f, transform.position.z);
        }
        else
        {
            dispensePoint = new Vector3(transform.position.x - 1.0f, transform.position.y + 0.5f, transform.position.z);
        }
        

        //Dispense pickup
        GameObject go = Instantiate(paintDrop, dispensePoint, Quaternion.identity);
        go.GetComponent<PaintDrop_Script>().currentColour = colourToDrop;
    }
}
