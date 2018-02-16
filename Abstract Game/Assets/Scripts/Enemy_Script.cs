using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Script : MonoBehaviour
{
    public int health;
    public int detectRange;
    public colour thisColour;
    
    private bool facingRight;
    private Rigidbody2D myRigid;

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

    //protected void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if(collision.gameObject.CompareTag("PlayerBullet") || collision.gameObject.CompareTag("EnemyBullet"))
    //    {
    //        if(collision.gameObject.GetComponent<Bullet_Script>().getColour() == thisColour)
    //        {
    //            health -= collision.gameObject.GetComponent<Bullet_Script>().damage;
    //            Destroy(collision.gameObject);
    //        }
    //        else
    //        {
    //            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    //        }
    //    }
    //}

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
