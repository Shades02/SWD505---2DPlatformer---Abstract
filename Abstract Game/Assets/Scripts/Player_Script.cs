﻿using System.Collections;
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
    public float maxShootCD;

    public GameObject rightShootPoint;
    public GameObject leftShootPoint;
    public GameObject bulletPrefab;

    public Text healthText;
    public Text ammoText;
    public Text colourText;
    public Text timerText;

    private float currentWallJumpCD;
    private float currentShootCD;
    private int health;
    private int ammo = 0;
    private float levelTimer = 0;
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
        health = maxHealth;
        soundManager = GameObject.Find("SoundManager").GetComponent<Sound_Manager_Script>();

        //Temporary
        ammo = 50;
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
        levelTimer += Time.deltaTime;

        //text updates
        healthText.text = "Health: " + health.ToString("00");
        if (health < 0) healthText.text = "Health: 00";

        ammoText.text = "Ammo: " + ammo.ToString("00");
        timerText.text = "Time: " + levelTimer.ToString("0000");

        colourText.text = "Colour: " + currentColour;

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
                    go.GetComponent<Bullet_Script>().setColour(currentColour);
                }
                else
                {
                    GameObject go = Instantiate(bulletPrefab, leftShootPoint.transform.position, Quaternion.identity);
                    go.GetComponent<Rigidbody2D>().velocity = Vector2.left * firePower;
                    go.GetComponent<Bullet_Script>().setTag("PlayerBullet");
                    go.GetComponent<Bullet_Script>().setColour(currentColour);
                }

                ammo -= 1;          //use 1 ammo per shot
                ammoText.text = "Ammo: " + ammo.ToString("00");                 //do I need this line???
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

        //jump
        if (Input.GetButtonDown("Jump"))
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
                case Pickup_Script.pickupType.yellow:        
                case Pickup_Script.pickupType.blue:
                case Pickup_Script.pickupType.red:
                    switch (currentColour)
                    {
                        case colour.white:
                            switch(collision.GetComponent<Pickup_Script>().thisPickupType)
                            {
                                case Pickup_Script.pickupType.red:
                                    currentColour = colour.red;
                                    break;
                                case Pickup_Script.pickupType.blue:
                                    currentColour = colour.blue;
                                    break;
                                case Pickup_Script.pickupType.yellow:
                                    currentColour = colour.yellow;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case colour.red:
                            switch (collision.GetComponent<Pickup_Script>().thisPickupType)
                            {
                                case Pickup_Script.pickupType.blue:
                                    currentColour = colour.purple;
                                    break;
                                case Pickup_Script.pickupType.yellow:
                                    currentColour = colour.orange;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case colour.blue:
                            switch (collision.GetComponent<Pickup_Script>().thisPickupType)
                            {
                                case Pickup_Script.pickupType.red:
                                    currentColour = colour.purple;
                                    break;
                                case Pickup_Script.pickupType.yellow:
                                    currentColour = colour.green;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case colour.yellow:
                            switch (collision.GetComponent<Pickup_Script>().thisPickupType)
                            {
                                case Pickup_Script.pickupType.red:
                                    currentColour = colour.orange;
                                    break;
                                case Pickup_Script.pickupType.blue:
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
                    break;

                //non colour pickups
                case Pickup_Script.pickupType.ammo:
                    ammo += 10;
                    break;
                case Pickup_Script.pickupType.health:
                    health += 5;        //add 5 health currently
                    if (health > maxHealth) health = maxHealth;     //can't go over max health
                    break;
            }

            //update colour
            updateColour();

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

        if (collision.gameObject.CompareTag("Mine"))
        {
            health -= collision.gameObject.GetComponent<Mine_Script>().getDamage();
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.CompareTag("Water"))
        {
            //touching the water clears off any paint/colour
            currentColour = colour.white;
            updateColour();
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

        if(collision.gameObject.CompareTag("EnemyBullet"))
        {
            health -= collision.gameObject.GetComponent<Bullet_Script>().damage;
            Destroy(collision.gameObject);
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
