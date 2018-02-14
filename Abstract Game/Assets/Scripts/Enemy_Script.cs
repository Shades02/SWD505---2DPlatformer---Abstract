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

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("PlayerBullet") || collision.gameObject.CompareTag("EnemyBullet"))
        {
            if(collision.gameObject.GetComponent<Bullet_Script>().getColour() == thisColour)
            {
                health -= collision.gameObject.GetComponent<Bullet_Script>().damage;
                Destroy(collision.gameObject);
            }
        }
    }
}
