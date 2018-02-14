using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Script : Timed_Object_Script
{
    public int damage;

    private colour currentColour;

    private void Start()
    {

    }

    public void setTag(string tag)
    {
        gameObject.tag = tag;
    }

    public void setColour(colour colourToSet)
    {
        currentColour = colourToSet;
        //change visible colour
    }

    public colour getColour()
    {
        return currentColour;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("PlayerBullet") || collision.gameObject.CompareTag("EnemyBullet"))
        {
            //check colour here
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Land"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("ColourWall"))
        {
            if(collision.gameObject.GetComponent<Colour_Wall_Script>().wallColour == currentColour)
            {
                Destroy(gameObject);
            }
        }
    }
}
