using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_Script : MonoBehaviour
{
    public int health;
    public float shootCooldown;
    public int shootPower;
    public int detectRange;

    public GameObject projectile;

    private float curShootCD = 0;
    private GameObject player;

	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update ()
    {
        curShootCD += Time.deltaTime;

        if(curShootCD >= shootCooldown)
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

                curShootCD = 0;
            }
        }
	}
}
