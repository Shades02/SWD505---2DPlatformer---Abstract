﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_Enemy_Script : Enemy_Script
{
    public int moveSpeed;
    public float maxAttackCD;
    public int damage;
    public float maxRetreatDuration;
    public int chargeRange;
    public float maxChargeSoundTimer;

    private float currentAttackCD = 0;
    private float currentRetreatTime = 999;     //high so that it starts the enemy not retreating
    private float currentChargeSoundTimer;
	
	void Update ()
    {
        deathCheck();
        currentAttackCD -= Time.deltaTime;
        currentRetreatTime += Time.deltaTime;
        currentChargeSoundTimer -= Time.deltaTime;

        myAnim.SetFloat("runSpeed", Mathf.Abs(myRigid.velocity.x));        //check to see if moving

        getDirection();

        //Enemy specific, not in the parent class
        if (!facingRight)           //melee enemy sprite is right by default, so if facing left, flip sprite
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;        //dont flip if facing right
        }

    }

    private void FixedUpdate()
    {
        if (thisColour == player.GetComponent<Player_Script>().getColour())     //only "sees" the player if they are the same colour
        {
            float xDistance = player.transform.position.x - transform.position.x;

            if (currentRetreatTime < maxRetreatDuration)
            {
                //retreating
                if (xDistance > 0)       //player is to the right, retreat to the left
                {
                    myRigid.velocity = new Vector2(-moveSpeed * 2, myRigid.velocity.y);
                }
                else
                {
                    myRigid.velocity = new Vector2(moveSpeed * 2, myRigid.velocity.y);
                }
            }
            else if (Mathf.Abs(xDistance) <= chargeRange &&     //check if in charge range
                player.transform.position.y < transform.position.y + 1 && player.transform.position.y > transform.position.y - 1)       //check if on same y level +- 1
            {
                //Charge at player
                
                if(currentChargeSoundTimer <= 0)        //play charge sound
                {
                    soundManager.PlaySFX("EnemyCharge");
                    currentChargeSoundTimer = maxChargeSoundTimer;      //now it counts down until it can be played again
                }

                if (xDistance > 0)       //charge at player to the right
                {
                    myRigid.velocity = new Vector2(moveSpeed * 2, myRigid.velocity.y);
                }
                else                    //charge at player to the left
                {
                    myRigid.velocity = new Vector2(-moveSpeed * 2, myRigid.velocity.y);
                }
            }
            else
            {
                //Tracking Player
                if (Mathf.Abs(xDistance) <= detectRange)        //if player is within detect range
                {
                    if (xDistance > 0)  //move right
                    {
                        myRigid.velocity = new Vector2(moveSpeed, myRigid.velocity.y);
                    }
                    else                //move left
                    {
                        myRigid.velocity = new Vector2(-moveSpeed, myRigid.velocity.y);
                    }
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(currentAttackCD <= 0)
            {
                currentAttackCD = maxAttackCD;
                collision.gameObject.GetComponent<Player_Script>().takeDamage(damage);
                currentRetreatTime = 0;
            }
        }
        else if(collision.gameObject.CompareTag("Spike"))
        {
            health -= collision.gameObject.GetComponent<Spike_Script>().damage;
        }
    }
}
