using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Front_Script : MonoBehaviour
{
    private Animator myAnim;
    private Enemy_Script myEnemy;

	void Start ()
    {
        myAnim = gameObject.GetComponent<Animator>();
        myEnemy = transform.parent.GetComponent<Enemy_Script>();
	}
	
	void Update ()
    {
        myAnim.SetFloat("runSpeed", Mathf.Abs(myEnemy.gameObject.GetComponent<Rigidbody2D>().velocity.x));        //check to see if moving

        if (!myEnemy.returnDirection())           //melee enemy sprite is right by default, so if facing left, flip sprite
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;        //dont flip if facing right
        }

        if(myEnemy.health <= 0)         
        {
            myAnim.SetBool("isDead", true);
        }
    }
}
