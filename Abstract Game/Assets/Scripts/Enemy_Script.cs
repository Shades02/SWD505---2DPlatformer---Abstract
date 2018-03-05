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
    protected Animator myAnim;

    protected GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myRigid = gameObject.GetComponent<Rigidbody2D>();
        myAnim = gameObject.GetComponent<Animator>();

        Colour_Changer_Script.setColour(gameObject, thisColour);        //set colour layer as well
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
            myAnim.SetBool("isDead", true);     //when dead, changes animation
            Invoke("destroyEnemy", 0.5f);       //delay may need editing to allow for more the animation
        }
    }

    private void destroyEnemy()
    {
        Destroy(gameObject);
    }

    public void takeDamage(int damage)
    {
        health -= damage;
    }

    public bool returnDirection()
    {
        return facingRight;
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

}
