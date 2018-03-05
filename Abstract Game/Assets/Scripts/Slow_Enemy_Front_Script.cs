using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow_Enemy_Front_Script : MonoBehaviour
{

    private Animator myAnim;
    private Enemy_Script myEnemy;

    void Start()
    {
        myAnim = gameObject.GetComponent<Animator>();
        myEnemy = transform.parent.GetComponent<Enemy_Script>();
    }

    void Update()
    {
        myAnim.SetFloat("runSpeed", Mathf.Abs(myEnemy.gameObject.GetComponent<Rigidbody2D>().velocity.x));        //check to see if moving

        if (myEnemy.returnDirection())           //slow/shoot enemy sprite is left by default, so if facing right, flip sprite
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;        //dont flip if facing left
        }

        if (myEnemy.health <= 0)
        {
            myAnim.SetBool("isDead", true);
        }
    }
}
