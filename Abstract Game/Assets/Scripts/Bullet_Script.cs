using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Script : Timed_Object_Script
{
    public int damage;

    private colour currentColour;
    private Sound_Manager_Script soundManager;

    private void Start()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<Sound_Manager_Script>();
    }

    public void setTag(string tag)
    {
        gameObject.tag = tag;
    }

    public void setColour(colour colourToSet, bool facingRight)
    {
        Colour_Changer_Script.setColour(gameObject, colourToSet);

        if (!facingRight)           //bullet sprite is right by default, so if facing left, flip sprite
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;        //dont flip if facing right
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Land") || collision.gameObject.CompareTag("ColourWall") || collision.gameObject.CompareTag("Dispenser"))
        {
            soundManager.PlaySFX("ProjectileImpact");
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            if(gameObject.CompareTag("PlayerBullet"))       //only hurts the enemies if this is a player bullet
            {
                collision.gameObject.GetComponent<Enemy_Script>().takeDamage(damage);
                soundManager.PlaySFX("ProjectileImpact");
                Destroy(gameObject);
            }
        }
        else if(collision.gameObject.CompareTag("Player"))
        {
            if(gameObject.CompareTag("EnemyBullet"))        //only hurts the player if this is an enemy bullet
            {
                collision.gameObject.GetComponent<Player_Script>().takeDamage(damage);
                soundManager.PlaySFX("ProjectileImpact");
                Destroy(gameObject);
            }
        }
        else if (collision.gameObject.CompareTag("PlayerBullet") || collision.gameObject.CompareTag("EnemyBullet"))
        {
            soundManager.PlaySFX("ProjectileImpact");
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("BossShape") && gameObject.gameObject.CompareTag("PlayerBullet"))
        {
            soundManager.PlaySFX("ProjectileImpact");
            Destroy(gameObject);
            collision.gameObject.GetComponent<Shape_Script>().takeDamage(damage);
        }
    }
}
