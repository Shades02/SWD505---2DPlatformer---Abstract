using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_Enemy_Script : Enemy_Script
{
    public int moveSpeed;
    public float maxAttackCD;
    public int damage;
    public float maxRetreatDuration;
    public int chargeRange;

    private float currentAttackCD = 0;
    private float currentRetreatTime = 999;     //high so that it starts the enemy not retreating
	
	void Update ()
    {
        deathCheck();
        currentAttackCD -= Time.deltaTime;
        currentRetreatTime += Time.deltaTime;

        getDirection();

        float xDistance = player.transform.position.x - transform.position.x;

        if (currentRetreatTime < maxRetreatDuration)
        {
            //retreating
            if(xDistance > 0)       //player is to the right, retreat to the left
            {
                myRigid.velocity = new Vector2(-moveSpeed * 2, myRigid.velocity.y);
            }
            else
            {
                myRigid.velocity = new Vector2(moveSpeed * 2, myRigid.velocity.y);
            }
        }
        else if (Mathf.Abs(xDistance) <= chargeRange)
        {
            //Charge at player
            if(xDistance > 0)       //charge at player to the right
            {
                myRigid.velocity = new Vector2(moveSpeed * 2, myRigid.velocity.y);
            }
            else
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
    }
}
