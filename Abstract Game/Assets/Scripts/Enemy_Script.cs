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
    protected bool isDead = false;      //for the death sound

    protected GameObject player;
    protected Sound_Manager_Script soundManager;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myRigid = gameObject.GetComponent<Rigidbody2D>();
        myAnim = gameObject.GetComponent<Animator>();
        soundManager = GameObject.Find("SoundManager").GetComponent<Sound_Manager_Script>();

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
            if(!isDead)
            {
                soundManager.PlaySFX("EnemyDeath");
                isDead = true;      //so that the sound only plays once
            }
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
        soundManager.PlaySFX("EnemyHit");
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
            takeDamage(collision.gameObject.GetComponent<Spike_Script>().damage);
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            takeDamage(collision.gameObject.GetComponent<Spike_Script>().damage);
        }
    }
}
