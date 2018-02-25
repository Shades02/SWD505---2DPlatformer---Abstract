using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Script : MonoBehaviour
{
    public int health;
    public int detectRange;
    public colour thisColour;
    
    protected bool facingRight = true;
    protected Rigidbody2D myRigid;

    protected GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myRigid = gameObject.GetComponent<Rigidbody2D>();
        setColourLayer();
    }

    protected void getDirection()       //need to call in update
    {
        if (myRigid.velocity.x > 0) facingRight = true;
        else if (myRigid.velocity.x < 0) facingRight = false;
    }

    protected void deathCheck()       //need to call in update
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void takeDamage(int damage)
    {
        health -= damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Mine"))
        {
            health -= collision.gameObject.GetComponent<Mine_Script>().getDamage();
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            health -= collision.gameObject.GetComponent<Spike_Script>().getDamage();
        }
    }

    private void setColourLayer()
    {
        switch (thisColour)
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
}
