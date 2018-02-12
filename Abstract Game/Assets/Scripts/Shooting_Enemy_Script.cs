using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting_Enemy_Script : Enemy_Script
{
    public float maxShootCD;
    public int shootPower;

    public GameObject projectile;

    private float curShootCD;

    void Update()
    {
        curShootCD -= Time.deltaTime;

        shoot();
    }

    void shoot()
    {
        if (curShootCD <= 0)
        {
            float xDistance = player.transform.position.x - transform.position.x;
            //check for player range
            if (Mathf.Abs(xDistance) <= detectRange)
            {
                if (player.transform.position.x > transform.position.x)
                {
                    GameObject go = Instantiate(projectile, new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z), Quaternion.identity);
                    go.GetComponent<Rigidbody2D>().AddForce(Vector2.right * shootPower);
                }
                else if (player.transform.position.x < transform.position.x)
                {
                    GameObject go = Instantiate(projectile, new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z), Quaternion.identity);
                    go.GetComponent<Rigidbody2D>().AddForce(Vector2.left * shootPower);
                }

                curShootCD = maxShootCD;
            }
        }
    }
}
