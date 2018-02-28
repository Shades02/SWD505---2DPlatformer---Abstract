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
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
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
