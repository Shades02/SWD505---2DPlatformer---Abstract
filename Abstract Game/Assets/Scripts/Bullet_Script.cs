﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Script : Timed_Object_Script
{
    public int damage;

    private colour currentColour;
    private SpriteRenderer myRenderer;

    private void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
    }

    public void setTag(string tag)
    {
        gameObject.tag = tag;
    }

    public void setColour(colour colourToSet, bool facingRight)
    {
        currentColour = colourToSet;
        GetComponent<ColourUpdate_Script>().updateColour(currentColour);

        //This is wrong VV
        if(!facingRight)
        {
            transform.localScale *= -1;
        }
    }

    public colour getColour()
    {
        return currentColour;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Land") || collision.gameObject.CompareTag("ColourWall") || collision.gameObject.CompareTag("Dispenser"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            if(gameObject.CompareTag("PlayerBullet"))       //only hurts the enemies if this is a player bullet
            {
                collision.gameObject.GetComponent<Enemy_Script>().takeDamage(damage);
                Destroy(gameObject);
            }
        }
        else if(collision.gameObject.CompareTag("Player"))
        {
            if(gameObject.CompareTag("EnemyBullet"))        //only hurts the player if this is an enemy bullet
            {
                collision.gameObject.GetComponent<Player_Script>().takeDamage(damage);
                Destroy(gameObject);
            }
        }
        else if (collision.gameObject.CompareTag("PlayerBullet") || collision.gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

}
