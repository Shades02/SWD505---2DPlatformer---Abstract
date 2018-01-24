using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    public int moveSpeed;
    Rigidbody2D myRigid;

	void Start ()
    {
        myRigid = GetComponent<Rigidbody2D>();
	}
	
	void Update ()
    {
		
	}

    private void FixedUpdate()
    {
        myRigid.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, myRigid.velocity.y);
    }
}
