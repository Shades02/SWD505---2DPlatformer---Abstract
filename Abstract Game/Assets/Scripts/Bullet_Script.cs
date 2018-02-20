using System.Collections;
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

    public void setColour(colour colourToSet)
    {
        currentColour = colourToSet;
        setColourLayer();
        //change visible colour
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

    private void setColourLayer()
    {
        switch(currentColour)
        {
            case colour.white:
                gameObject.layer = 20;
                break;
            case colour.black:
                gameObject.layer = 21;
                break;
            case colour.red:
                gameObject.layer = 22;
                break;
            case colour.blue:
                gameObject.layer = 23;
                break;
            case colour.yellow:
                gameObject.layer = 24;
                break;
            case colour.green:
                gameObject.layer = 25;
                break;
            case colour.orange:
                gameObject.layer = 26;
                break;
            case colour.purple:
                gameObject.layer = 27;
                break;
        }
    }

    private void updateColour()
    {
        switch (currentColour)
        {
            case colour.white:
                myRenderer.color = Color.white;
                break;
            case colour.black:
                myRenderer.color = Color.black;
                break;
            case colour.red:
                myRenderer.color = Color.red;
                break;
            case colour.blue:
                myRenderer.color = Color.blue;
                break;
            case colour.yellow:
                myRenderer.color = Color.yellow;
                break;
            case colour.green:
                myRenderer.color = Color.green;
                break;
            case colour.purple:
                myRenderer.color = Color.magenta;
                break;
            case colour.orange:
                myRenderer.color = Color.grey;
                break;
        }
    }
}
