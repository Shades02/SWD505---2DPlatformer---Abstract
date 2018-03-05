using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShoot_Enemy_Script : Shooting_Enemy_Script
{
    public int moveSpeed;
    public int shootRange;

    void Update ()
    {
        deathCheck();
        getDirection();

        curShootCD -= Time.deltaTime;

        myAnim.SetFloat("runSpeed", Mathf.Abs(myRigid.velocity.x));        //check to see if moving

        //Enemy specific, not in the parent class
        if (facingRight)           //slow shoot enemy sprite is left by default, so if facing right, flip sprite
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;        //dont flip if facing left
        }
    }

    private void FixedUpdate()
    {
        if(thisColour == player.GetComponent<Player_Script>().getColour())      //only "sees" the player if they are the same colour
        {
            float xDistance = player.transform.position.x - transform.position.x;

            if (Mathf.Abs(xDistance) <= shootRange)              //if in range, enemy starts to shoot and stops moving
            {
                shootPlayer();
            }
            else if (Mathf.Abs(xDistance) <= detectRange)        //else the enemy will move towards the player
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
