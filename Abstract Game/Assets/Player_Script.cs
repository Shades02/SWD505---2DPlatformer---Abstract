using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Script : MonoBehaviour
{
    public int moveSpeed;
    public int jumpStrength;
    public int maxWallJumpCD;
    public int firePower;
    public int maxHealth;

    public GameObject bulletPrefab;

    public Text healthText;
    public Text ammoText;

    private float currentWallJumpCD;
    private int health;
    private int ammo = 0;

    private Pickup_Script.pickupType currentColour;

    private Rigidbody2D myRigid;
    private SpriteRenderer myRenderer;

	void Start ()
    {
        myRigid = GetComponent<Rigidbody2D>();
        myRenderer = GetComponent<SpriteRenderer>();
        currentColour = Pickup_Script.pickupType.white;
        health = maxHealth;

        //Temporary
        ammo = 50;
    }
	
	void Update ()
    {
        //counter updates
        currentWallJumpCD -= Time.deltaTime;

        //text updates
        healthText.text = "Health: " + health.ToString("00");
        ammoText.text = "Ammo: " + ammo.ToString("00");

        //shoot - only check if you have ammo to shoot
        //Could use object pool here instead of instantiating?
        if(ammo > 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                //positive velocity is to the right
                if (myRigid.velocity.x >= 0)
                {
                    GameObject go = Instantiate(bulletPrefab, new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z), Quaternion.identity);
                    go.GetComponent<Rigidbody2D>().AddForce(Vector2.right * firePower);
                }
                //negative velocity is to the left
                else if (myRigid.velocity.x < 0)
                {
                    GameObject go = Instantiate(bulletPrefab, new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z), Quaternion.identity);
                    go.GetComponent<Rigidbody2D>().AddForce(Vector2.left * firePower);
                }

                ammo -= 1;          //use 1 ammo per shot
                ammoText.text = "Ammo: " + ammo.ToString("00");                 //do I need this line???
            }
        }
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
            switch (collision.GetComponent<Pickup_Script>().thisPickupType)
            {
                //colour pickups
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

                //non colour pickups
                case Pickup_Script.pickupType.ammo:
                    ammo += 10;
                    break;
                case Pickup_Script.pickupType.health:
                    health += 5;        //add 5 health currently
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Spike"))
        {
            health -= collision.gameObject.GetComponent<Spike_Script>().getDamage();
        }
    }
}
