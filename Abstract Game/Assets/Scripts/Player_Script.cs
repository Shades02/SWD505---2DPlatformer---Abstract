using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_Script : MonoBehaviour
{
    public int moveSpeed;
    public int jumpStrength;
    public int firePower;
    public int maxHealth;
    public int maxAmmo;
    public float maxShootCD;
    public float ammoRegenTime;
    public float maxInvDuration;            //max invincible time

    public GameObject rightShootPoint;
    public GameObject leftShootPoint;
    public GameObject bulletPrefab;
    private GameObject gameOverScreen;

    private float currentShootCD;
    private float currentAmmoRegenTime;
    private float currentInvTime;           //counts how long you've been invincible
    private int health;
    private int ammo = 0;
    private bool facingRight;
    private bool isDead = false;            //to ensure the death sound is only played once

    private colour currentColour;

    private Rigidbody2D myRigid;
    private SpriteRenderer myRenderer;
    private Animator myAnim;
    private Sound_Manager_Script soundManager;

    private GameObject objectLastFrame = null;       //the gameobject that the player was "on top of" in the last frame
    private GameObject objectThisFrame;             //the gameobject that the layer is "on top of" this frame

    void Start ()
    {
        myRigid = GetComponent<Rigidbody2D>();
        myRenderer = GetComponent<SpriteRenderer>();
        myAnim = GetComponent<Animator>();
        Colour_Changer_Script.setColour(gameObject, currentColour);     //set colour and colour layer
        health = maxHealth;
        soundManager = GameObject.Find("SoundManager").GetComponent<Sound_Manager_Script>();
        gameOverScreen = GameObject.Find("Canvas").transform.GetChild(3).gameObject;                //Since GameOver is always at index 3 of the canvas prefab
        currentInvTime = maxInvDuration;    //so that you dont start the level invincible

        ammo = maxAmmo;     //start with max ammo
    }
	
	void Update ()
    {
        //Death check
        if(health <= 0)
        {
            //Reloads the current scene
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            if(!isDead)         //now the sound will play once the player dies, but not more than once
            {
                soundManager.PlaySFX("PlayerDeath");
                isDead = true;
            }
            myAnim.SetBool("isDead", true);   
            Invoke("killPlayer", 0.5f);             //calls the function to kill the player after a delay
        }

        //counter updates
        //Cooldowns count down until it can be used again
        currentShootCD -= Time.deltaTime;
        currentAmmoRegenTime -= Time.deltaTime;
        currentInvTime += Time.deltaTime;           //counts up asit counts how long youve been invincible

        //Invincibility display
        if (currentInvTime < maxInvDuration)        //if you are within the invincibility time
        {
            myRenderer.enabled = !myRenderer.enabled;       //toggle sprite renderer to give the flickering sprite effect
        }
        else myRenderer.enabled = true;             //if you're not invincible, the sprite is always displayed

        //Ammo Regen
        if (currentAmmoRegenTime <= 0)
        {
            if(ammo < maxAmmo)
                ammo += 1;
            currentAmmoRegenTime = ammoRegenTime;
        }

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

                soundManager.PlaySFX("PlayerShooting");
            }
        }
	}

    private void FixedUpdate()
    {
        //move left/right
        myRigid.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, myRigid.velocity.y);
        myAnim.SetFloat("runSpeed", Mathf.Abs(Input.GetAxis("Horizontal")));        //set velocity for animator to decide if the player is moving

        //check for movement for walking sound
        if(myRigid.velocity.x != 0 && myRigid.velocity.y == 0)          //only plays when there is lateral movement but not when moving vertically (i.e in the air)
        {
            if(!gameObject.GetComponent<AudioSource>().isPlaying)
                gameObject.GetComponent<AudioSource>().Play();
        }
        else
        {
            if (gameObject.GetComponent<AudioSource>().isPlaying)
                gameObject.GetComponent<AudioSource>().Stop();
        }

        //get direction
        if (myRigid.velocity.x > 0) facingRight = true;
        else if (myRigid.velocity.x < 0) facingRight = false;

        //change sprite facing
        if (!facingRight)           //player sprite is right by default, so if facing left, flip sprite
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;        //dont flip if facing right
        }

        //Prep jump linecasts
        float spriteHeight = gameObject.GetComponent<BoxCollider2D>().bounds.size.y;        
        Vector3 linecastStart = new Vector3(transform.position.x, transform.position.y - spriteHeight / 2 - 0.1f, transform.position.z);
        Debug.DrawLine(linecastStart + new Vector3(0.2f, 0,0), linecastStart + new Vector3(0.2f, -0.1f, 0));        //Remove this!!!
        Debug.DrawLine(linecastStart + new Vector3(-0.2f, 0, 0), linecastStart + new Vector3(-0.2f, -0.1f, 0));        //Remove this!!!

        //Landing sound test
        var hit = Physics2D.Linecast(linecastStart, linecastStart + new Vector3(0, -0.1f, 0));      //check what is below the player
        if(hit)     //if the player is standing on something
        {
            objectThisFrame = hit.collider.gameObject;      //collect what the player is standing on

            if(1 << objectThisFrame.layer == 1 << LayerMask.NameToLayer("Land") ||
                1 << objectThisFrame.layer == 1 << LayerMask.NameToLayer(currentColour.ToString()))      //if they are on "land" or a matching colour object
            {
                if(objectLastFrame == null)     //if the player hit something this frame, and was in air last frame, thats a landing
                {
                    soundManager.PlaySFX("PlayerLanding");
                }
            }

            objectLastFrame = objectThisFrame;      //before the test ends, set the last frame object to this ready for the next frame
        }
        else
        {
            objectLastFrame = null;     //if there is no hit, the player is in air, so set last frame to null, ready for the next frame
        }
    
        //jump
        if (Input.GetButton("Jump"))
        {
            if (Physics2D.Linecast(linecastStart + new Vector3(0.2f, 0, 0),                           //off set the linecast down to avoid it finding itself
                linecastStart + new Vector3(0.2f, -0.1f, 0),                                          //distance down slightly more than half the height of the sprite
                1 << LayerMask.NameToLayer("Land")) ||

                Physics2D.Linecast(linecastStart + new Vector3(0.2f, 0, 0),                           //second linecast checks if you are standing on an entity of the same colour
                linecastStart + new Vector3(0.2f, -0.1f, 0),
                1 << LayerMask.NameToLayer(currentColour.ToString())) ||
                
                Physics2D.Linecast(linecastStart + new Vector3(-0.2f, 0, 0),
                linecastStart + new Vector3(-0.2f, -0.1f, 0),
                1 << LayerMask.NameToLayer("Land")) ||

                Physics2D.Linecast(linecastStart + new Vector3(-0.2f, 0, 0),                          
                linecastStart + new Vector3(-0.2f, -0.1f, 0),
                1 << LayerMask.NameToLayer(currentColour.ToString()))
                )
            {
                myRigid.velocity = new Vector2(myRigid.velocity.x, 0);
                myRigid.AddForce(new Vector2(0, jumpStrength));
                soundManager.PlaySFX("PlayerJumping");
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
                    soundManager.PlaySFX("AmmoPickup");
                    ammo = maxAmmo;                                 //set to max ammo
                    break;
                case Pickup_Script.pickupType.health:
                    soundManager.PlaySFX("PickupHealth");
                    health = maxHealth;                             //set to max health
                    break;
            }

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Mine"))
        {
            takeDamage(collision.gameObject.GetComponent<Spike_Script>().damage);
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.CompareTag("Water"))
        {
            //touching the water clears off any paint/colour
            currentColour = colour.white;
            Colour_Changer_Script.setColour(gameObject, currentColour);     //updates the colour and colour layer
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
            soundManager.PlaySFX("PickupColour");
            Colour_Changer_Script.setColour(gameObject, currentColour);     //updates the colour and colour layer
        }
        else if(collision.gameObject.CompareTag("Tear"))
        {
            //end of level
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Spike"))
        {
            takeDamage(collision.gameObject.GetComponent<Spike_Script>().damage);
        }
    }

    public void takeDamage(int damage)
    {
        if(currentInvTime > maxInvDuration)         //the damage only applies if you are out of invincibility frames
        {
            health -= damage;
            soundManager.PlaySFX("PlayerHit");
            currentInvTime = 0;     //sets to 0 to start counting invincibility
        }
    }

    public int getHealth()
    {
        return health;
    }

    public int getAmmo()
    {
        return ammo;
    }

    public colour getColour()
    {
        return currentColour;
    }

    public bool returnDirection()
    {
        return facingRight;
    }

    private void killPlayer()
    {
        gameOverScreen.SetActive(true);         //put up the game over screen
        Destroy(gameObject);                    //Destroy the player
    }

}
