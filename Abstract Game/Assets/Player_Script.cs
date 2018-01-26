using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    public int moveSpeed;
    public int jumpStrength;
    public int maxWallJumpCD;

    private float currentWallJumpCD;

    private Pickup_Script.pickupType currentColour;

    private Rigidbody2D myRigid;
    private SpriteRenderer myRenderer;

	void Start ()
    {
        myRigid = GetComponent<Rigidbody2D>();
        myRenderer = GetComponent<SpriteRenderer>();
        currentColour = Pickup_Script.pickupType.white;
	}
	
	void Update ()
    {
        currentWallJumpCD -= Time.deltaTime;
	}

    private void FixedUpdate()
    {
        //move left/right
        myRigid.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, myRigid.velocity.y);

        //jump
        if(Input.GetButtonDown("Jump"))
        {
            if (Physics2D.Linecast(transform.position,
            transform.position + new Vector3(0, -1.0f, 0),              //distance down slightly more than half the height of the sprite
            1 << LayerMask.NameToLayer("Land")))
            {
                myRigid.velocity = new Vector2(myRigid.velocity.x, 0);
                myRigid.AddForce(new Vector2(0, jumpStrength));
            }
            else if (currentWallJumpCD <= 0)            //if wall jump is not on cooldown, check for wall jump
            {
                currentWallJumpCD = maxWallJumpCD;      //reset wall jump cooldown

                if (Physics2D.Linecast(transform.position,
                transform.position + new Vector3(1.0f, 0, 0),               //right side check
                1 << LayerMask.NameToLayer("Land")))
                {
                    myRigid.velocity = new Vector2(myRigid.velocity.x, 0);
                    myRigid.AddForce(new Vector2(0, jumpStrength));
                }
                else if (Physics2D.Linecast(transform.position,
                transform.position + new Vector3(-1.0f, 0, 0),              //left side check
                1 << LayerMask.NameToLayer("Land")))
                {
                    myRigid.velocity = new Vector2(myRigid.velocity.x, 0);
                    myRigid.AddForce(new Vector2(0, jumpStrength));
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Pickup"))
        {
            switch (collision.GetComponent<Pickup_Script>().pickupColour)
            {
                case Pickup_Script.pickupType.white:
                    myRenderer.color = Color.white;
                    currentColour = Pickup_Script.pickupType.white;
                    break;
                case Pickup_Script.pickupType.blue:
                    myRenderer.color = Color.blue;
                    currentColour = Pickup_Script.pickupType.blue;
                    break;
                case Pickup_Script.pickupType.green:
                    myRenderer.color = Color.green;
                    currentColour = Pickup_Script.pickupType.green;
                    break;
                case Pickup_Script.pickupType.red:
                    myRenderer.color = Color.red;
                    currentColour = Pickup_Script.pickupType.red;
                    break;
            }

            Destroy(collision.gameObject);
        }

        if(collision.gameObject.CompareTag("ColourWall"))
        {
            if(currentColour == collision.GetComponent<Colour_Wall_Script>().wallColour)
            {
                Debug.Log("Colour Match");
                collision.GetComponent<BoxCollider2D>().isTrigger = false;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ColourWall"))          
        {
            //when you stop touching a colour wall, it becomes a trigger again
            collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }
}
