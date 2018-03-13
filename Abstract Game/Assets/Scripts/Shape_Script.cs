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

    private void Start()
    {
        startPos = transform.position;
    }

    private void FixedUpdate()
    {
        shootCD -= Time.deltaTime;
    }

    internal void moveShape()
    {
        transform.Translate(orbitDirection.normalized * orbitSpeed * Time.deltaTime);
    }

    internal void shoot()
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

    internal Vector3 getStartPos()
    {
        return startPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet") &&
            collision.GetComponent<Bullet_Script>().getColour() == thisColour)
        {
            Destroy(collision.gameObject);
            --health;
            --Boss.GetComponent<Boss_Script>().health;
        }
        else if (collision.CompareTag("Player") &&
            collision.GetComponent<Player_Script>().getColour() == thisColour)
        {
            collision.GetComponent<Player_Script>().takeDamage(1);
        }
    }
}
