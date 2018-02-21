using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting_Enemy_Script : Enemy_Script
{
    public float maxShootCD;
    public int shootPower;

    public GameObject projectile;
    public GameObject rightShotPoint;
    public GameObject leftShotPoint;

    protected float curShootCD = 0;

    void Update()
    {
        deathCheck();
        getDirection();

        curShootCD -= Time.deltaTime;

        shootPlayer();
    }

    protected void shootPlayer()
    {
        if (curShootCD <= 0)        //can only shoot if its not on cooldown
        {
            if (thisColour == player.GetComponent<Player_Script>().getColour())     //only "sees" the player if they are the same colour
            {
                float xDistance = player.transform.position.x - transform.position.x;
                //check for player range
                if (Mathf.Abs(xDistance) <= detectRange)
                {
                    if (player.transform.position.x > transform.position.x)         //shoot right
                    {
                        GameObject go = Instantiate(projectile, rightShotPoint.transform.position, Quaternion.identity);
                        go.GetComponent<Rigidbody2D>().velocity = Vector2.right * shootPower;
                        go.GetComponent<Bullet_Script>().setTag("EnemyBullet");
                        facingRight = true;
                        go.GetComponent<Bullet_Script>().setColour(thisColour, facingRight);
                    }
                    else if (player.transform.position.x < transform.position.x)    //shoot left
                    {
                        GameObject go = Instantiate(projectile, leftShotPoint.transform.position, Quaternion.identity);
                        go.GetComponent<Rigidbody2D>().velocity = Vector2.left * shootPower;
                        go.GetComponent<Bullet_Script>().setTag("EnemyBullet");
                        facingRight = false;
                        go.GetComponent<Bullet_Script>().setColour(thisColour, facingRight);
                    }

                    curShootCD = maxShootCD;
                }
            }
        }
    }
}
