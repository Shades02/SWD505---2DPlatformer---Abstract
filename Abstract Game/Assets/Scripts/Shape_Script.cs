using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape_Script : MonoBehaviour
{
    public int health;
    public float shootSpeed;
    public GameObject leftShootPoint,
        rightShootPoint;
    public colour thisColour;
    public GameObject projectile,
        Boss;

    internal float orbitSpeed,
        maxShootCD;
    internal Vector3 orbitDirection,
        startPos;

    private float shootCD;

    public GameObject lootDrop;
    public Vector2 lootPoint1;
    public Vector2 lootPoint2;

    private void Start()
    {
        startPos = transform.position;
    }

    private void FixedUpdate()
    {
        if (isDead())
        {
            GameObject go1 = Instantiate(lootDrop, lootPoint1, Quaternion.identity);
            go1.GetComponent<Pickup_Script>().thisPickupType = Pickup_Script.pickupType.ammo;

            GameObject go2 = Instantiate(lootDrop, lootPoint2, Quaternion.identity);
            go2.GetComponent<Pickup_Script>().thisPickupType = Pickup_Script.pickupType.health;

            gameObject.SetActive(false);
        }
        else shootCD -= Time.deltaTime;
    }

    private bool isDead()       //Returns true if health is 0
    {
        return (health <= 0);
    }

    internal void moveShape()       //Move shape in orbit direction
    {
        transform.Translate(orbitDirection.normalized * orbitSpeed * Time.deltaTime);
    }

    internal void shoot()       //Fire projectiles from both shoot points
    {
        if (shootCD <= 0)
        {
            GameObject rightProjectile = Instantiate(projectile, rightShootPoint.transform.position, Quaternion.identity);
            rightProjectile.GetComponent<Rigidbody2D>().velocity = Vector2.right * shootSpeed;
            rightProjectile.GetComponent<Bullet_Script>().setTag("EnemyBullet");
            rightProjectile.GetComponent<Bullet_Script>().setColour(thisColour, true);

            GameObject leftProjectile = Instantiate(projectile, leftShootPoint.transform.position, Quaternion.identity);
            leftProjectile.GetComponent<Rigidbody2D>().velocity = Vector2.left * shootSpeed;
            leftProjectile.GetComponent<Bullet_Script>().setTag("EnemyBullet");
            leftProjectile.GetComponent<Bullet_Script>().setColour(thisColour, false);

            shootCD = maxShootCD;
        }
    }

    internal Vector3 getStartPos()      //Returns start position vector
    {
        return startPos;
    }

    public void takeDamage(int damage)      //Deal damage to shape
    {
        health -= damage;
        Boss.GetComponent<Boss_Script>().health -= damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") &&
            collision.GetComponent<Player_Script>().getColour() == thisColour)
        {
            collision.GetComponent<Player_Script>().takeDamage(1);
        }
    }
}
