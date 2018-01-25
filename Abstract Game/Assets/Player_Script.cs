using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    public int moveSpeed;
    public int jumpStrength;
    public int maxWallJumpCD;

    private float currentWallJumpCD;

    Rigidbody2D myRigid;

	void Start ()
    {
        myRigid = GetComponent<Rigidbody2D>();
	}
	
	void Update ()
    {
        currentWallJumpCD -= Time.deltaTime;

        Debug.Log(myRigid.velocity);
	}

    private void FixedUpdate()
    {
        //move left/right
        myRigid.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, myRigid.velocity.y);

        //jump
        if(Input.GetButtonDown("Jump"))
        {
            if (Physics2D.Linecast(transform.position,
            transform.position + new Vector3(0, -0.5f, 0),              //distance down slightly more than half the height of the sprite
            1 << LayerMask.NameToLayer("Land")))
            {
                myRigid.velocity = new Vector2(myRigid.velocity.x, 0);
                myRigid.AddForce(new Vector2(0, jumpStrength));
            }
            else if (currentWallJumpCD <= 0)            //if wall jump is not on cooldown, check for wall jump
            {
                currentWallJumpCD = maxWallJumpCD;      //reset wall jump cooldown

                if (Physics2D.Linecast(transform.position,
                transform.position + new Vector3(0.5f, 0, 0),               //right side check
                1 << LayerMask.NameToLayer("Land")))
                {
                    myRigid.velocity = new Vector2(myRigid.velocity.x, 0);
                    myRigid.AddForce(new Vector2(0, jumpStrength));
                }
                else if (Physics2D.Linecast(transform.position,
                transform.position + new Vector3(-0.5f, 0, 0),              //left side check
                1 << LayerMask.NameToLayer("Land")))
                {
                    myRigid.velocity = new Vector2(myRigid.velocity.x, 0);
                    myRigid.AddForce(new Vector2(0, jumpStrength));
                }
            }
        }
    }
}
