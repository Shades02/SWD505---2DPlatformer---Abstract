using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum colour
{
    white, black, red, blue, yellow, green, orange, purple
}

public class Player_Script : MonoBehaviour
{
    public int moveSpeed;
    public int jumpStrength;
    public int maxWallJumpCD;
    public int firePower;
    public int maxHealth;
    public int maxAmmo;
    public float maxShootCD;

    public GameObject rightShootPoint;
    public GameObject leftShootPoint;
    public GameObject bulletPrefab;

    private float currentWallJumpCD;
    private float currentShootCD;
    private int health;
    private int ammo = 0;
    private bool facingRight;

    private colour currentColour;

    private Rigidbody2D myRigid;
    private SpriteRenderer myRenderer;
    private Sound_Manager_Script soundManager;

    void Start ()
    {
        myRigid = GetComponent<Rigidbody2D>();
        myRenderer = GetComponent<SpriteRenderer>();
        currentColour = colour.white;
        setColourLayer();
        health = maxHealth;
        soundManager = GameObject.Find("SoundManager").GetComponent<Sound_Manager_Script>();

        //Temporary
        ammo = maxAmmo;
    }
	
	void Update ()
    {
        //Death check
        if(health <= 0)
        {
            //Reloads the current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        //counter updates
        //Cooldowns count down until it can be used again
        currentWallJumpCD -= Time.deltaTime;
        currentShootCD -= Time.deltaTime;

        //shoot - only check if you have ammo to shoot
        //Could use object pool here instead of instantiating?
        if(ammo > 0 && currentShootCD <= 0 && !Pause_Menu_Script.isPaused)                 //makes sure the game isnt paused else it still instantiates when paused
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (facingRight)
                {
                    GameObject go = Instantiate(bulletPrefab, rightShootPoint.transform.position, Quaternion.identity);
                    go.GetComponent<Rigidbody2D>().velocity = Vector2.right * firePower;
                    go.GetComponent<Bullet_Script>().setTag("PlayerBullet");
                    go.GetComponent<Bullet_Script>().setColour(currentColour, facingRight);
                }
                else
                {
                    GameObject go = Instantiate(bulletPrefab, leftShootPoint.transform.position, Quaternion.identity);
                    go.GetComponent<Rigidbody2D>().velocity = Vector2.left * firePower;
                    go.GetComponent<Bullet_Script>().setTag("PlayerBullet");
                    go.GetComponent<Bullet_Script>().setColour(currentColour, facingRight);
                }
                
                ammo -= 1;          //use 1 ammo per shot
                currentShootCD = maxShootCD;

                soundManager.PlaySFX("SplashTest");
            }
        }
	}

    private void FixedUpdate()
    {
        //move left/right
        myRigid.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, myRigid.velocity.y);

        //get direction
        if (myRigid.velocity.x > 0) facingRight = true;
        else if (myRigid.velocity.x < 0) facingRight = false;

        float spriteHeight = gameObject.GetComponent<BoxCollider2D>().bounds.size.y;
        Vector3 linecastStart = new Vector3(transform.position.x, transform.position.y - spriteHeight / 2 - 0.1f, transform.position.z);
        Debug.DrawLine(linecastStart, linecastStart + new Vector3(0, -0.1f, 0));

        //jump
        if (Input.GetButtonDown("Jump"))
        {
            if (Physics2D.Linecast(linecastStart,                           //off set the linecast down to avoid it finding itself
                linecastStart + new Vector3(0, -0.1f, 0),                   //distance down slightly more than half the height of the sprite
                1 << LayerMask.NameToLayer("Land")) ||
                Physics2D.Linecast(linecastStart,                           //second linecast checks if you are standing on an entity of the same colour
                linecastStart + new Vector3(0, -0.1f, 0),
                1 << LayerMask.NameToLayer(currentColour.ToString())))
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
                case Pickup_Script.pickupType.ammo:
                    ammo = maxAmmo;                                 //set to max ammo
                    break;
                case Pickup_Script.pickupType.health:
                    health = maxHealth;                             //set to max health
                    break;
            }

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Mine"))
        {
            health -= collision.gameObject.GetComponent<Mine_Script>().getDamage();
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.CompareTag("Water"))
        {
            //touching the water clears off any paint/colour
            currentColour = colour.white;
            updateColour();
        }
        else if (collision.gameObject.CompareTag("Dispenser"))      //if the player jumps onto the top of the dispenser, it will trigger a dispense
        {
            collision.transform.parent.gameObject.GetComponent<Dispenser_Script>().dispense();
        }
        else if(collision.gameObject.CompareTag("PaintDrop"))
        {
            switch (currentColour)
            {
                case colour.white:
                    switch (collision.GetComponent<PaintDrop_Script>().currentColour)
                    {
                        case colour.red:
                            currentColour = colour.red;
                            break;
                        case colour.blue:
                            currentColour = colour.blue;
                            break;
                        case colour.yellow:
                            currentColour = colour.yellow;
                            break;
                        default:
                            break;
                    }
                    break;
                case colour.red:
                    switch (collision.GetComponent<PaintDrop_Script>().currentColour)
                    {
                        case colour.blue:
                            currentColour = colour.purple;
                            break;
                        case colour.yellow:
                            currentColour = colour.orange;
                            break;
                        default:
                            break;
                    }
                    break;
                case colour.blue:
                    switch (collision.GetComponent<PaintDrop_Script>().currentColour)
                    {
                        case colour.red:
                            currentColour = colour.purple;
                            break;
                        case colour.yellow:
                            currentColour = colour.green;
                            break;
                        default:
                            break;
                    }
                    break;
                case colour.yellow:
                    switch (collision.GetComponent<PaintDrop_Script>().currentColour)
                    {
                        case colour.red:
                            currentColour = colour.orange;
                            break;
                        case colour.blue:
                            currentColour = colour.green;
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    currentColour = colour.black;
                    break;
            }

            Destroy(collision.gameObject);
            updateColour();
            setColourLayer();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Spike"))
        {
            health -= collision.gameObject.GetComponent<Spike_Script>().getDamage();
        }
    }

    public void takeDamage(int damage)
    {
        health -= damage;
    }

    internal int getHealth()
    {
        return health;
    }

    internal int getAmmo()
    {
        return ammo;
    }

    internal colour getColour()
    {
        return currentColour;
    }

    private void setColourLayer()
    {
        switch (currentColour)
        {
            case colour.white:
                gameObject.layer = 20;
                break;
            case colour.black:
                gameObject.layer = 21;
                break;
            case colour.red:
                gameObject.layer = 22;
                break;
            case colour.blue:
                gameObject.layer = 23;
                break;
            case colour.yellow:
                gameObject.layer = 24;
                break;
            case colour.green:
                gameObject.layer = 25;
                break;
            case colour.orange:
                gameObject.layer = 26;
                break;
            case colour.purple:
                gameObject.layer = 27;
                break;
        }
    }

    private void updateColour()
    {
        switch (currentColour)
        {
            case colour.white:
                myRenderer.color = Color.white;
                break;
            case colour.black:
                myRenderer.color = Color.black;
                break;
            case colour.red:
                myRenderer.color = Color.red;
                break;
            case colour.blue:
                myRenderer.color = Color.blue;
                break;
            case colour.yellow:
                myRenderer.color = Color.yellow;
                break;
            case colour.green:
                myRenderer.color = Color.green;
                break;
            case colour.purple:
                myRenderer.color = Color.magenta;
                break;
            case colour.orange:
                myRenderer.color = Color.grey;
                break;
        }
    }
}
