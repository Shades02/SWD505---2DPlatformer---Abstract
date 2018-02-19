using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser_Script : MonoBehaviour
{
    public colour colourToDrop;
    public GameObject paintDrop;

	void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    public void dispense()
    {
        Vector3 dispensePoint = new Vector3(transform.position.x, transform.position.y + 3.0f, transform.position.z);

        //Dispense pickup
        GameObject go = Instantiate(paintDrop, dispensePoint, Quaternion.identity);
        go.GetComponent<PaintDrop_Script>().currentColour = colourToDrop;
    }
}
