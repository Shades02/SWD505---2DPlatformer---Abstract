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
    public GameObject projectile;

    internal float orbitSpeed;
    internal Vector3 orbitDirection;

    internal void moveShape()
    {
        transform.Translate(orbitDirection.normalized * orbitSpeed * Time.deltaTime);
    }

    internal void shoot(GameObject target)
    {
        GameObject rightProjectile = Instantiate(projectile, rightShootPoint.transform.position, Quaternion.identity);
        rightProjectile.GetComponent<Rigidbody2D>().velocity = Vector2.right * shootSpeed;
        rightProjectile.GetComponent<Bullet_Script>().setTag("EnemyBullet");
        rightProjectile.GetComponent<Bullet_Script>().setColour(thisColour, true);

        GameObject leftProjectile = Instantiate(projectile, leftShootPoint.transform.position, Quaternion.identity);
        leftProjectile.GetComponent<Rigidbody2D>().velocity = Vector2.left * shootSpeed;
        leftProjectile.GetComponent<Bullet_Script>().setTag("EnemyBullet");
        leftProjectile.GetComponent<Bullet_Script>().setColour(thisColour, false);
    }
}
