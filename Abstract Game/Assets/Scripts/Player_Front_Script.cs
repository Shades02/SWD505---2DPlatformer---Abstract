using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Front_Script : MonoBehaviour
{
    private Animator myAnim;
    private Player_Script myPlayer;

    void Start()
    {
        myAnim = gameObject.GetComponent<Animator>();
        myPlayer = transform.parent.GetComponent<Player_Script>();
    }

    void Update()
    {
        myAnim.SetFloat("runSpeed", Mathf.Abs(Input.GetAxis("Horizontal")));        //check to see if moving

        if (!myPlayer.returnDirection())           //player sprite is right by default, so if facing left, flip sprite
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;        //dont flip if facing right
        }

        if (myPlayer.getHealth() <= 0)
        {
            myAnim.SetBool("isDead", true);
        }
    }
}
