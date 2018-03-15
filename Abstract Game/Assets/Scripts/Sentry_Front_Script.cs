using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sentry_Front_Script : MonoBehaviour
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

        if (!myEnemy.returnDirection())           //sentry enemy sprite is right by default, so if facing left, flip sprite
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;        //dont flip if facing right
        }

        if (myEnemy.health <= 0)
        {
            myAnim.SetBool("isDead", true);
        }
    }
}
