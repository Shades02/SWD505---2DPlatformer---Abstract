using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Script : Enemy_Script
{

    public float moveSpeed,
        minX,
        maxX,
        radius,
        p1OrbitSpeed,
        p2OrbitSpeed,
        p1ShootCD,
        p2ShootCD,
        maxMeleeCD;
    public GameObject[] shapes;

    private Vector2 moveDirection;
    private Vector3 targetPosition;
    private bool patrolling = false,
        shooting = false,
        meleeAttacking = false;
    private int p2Health;
    private float meleeCD;

    private void Start()
    {
        moveDirection = new Vector2(-1, 0);
        for (int i = 0; i < shapes.Length; ++i)
        {
            health += shapes[i].GetComponent<Shape_Script>().health;
            shapes[i].GetComponent<Shape_Script>().orbitSpeed = p1OrbitSpeed;
            shapes[i].GetComponent<Shape_Script>().maxShootCD = p1ShootCD;
        }
        p2Health = health / 2;
        for (int i = 0; i < shapes.Length; i++) shapes[i].GetComponent<Shape_Script>().shoot();
    }

    private void FixedUpdate()
    {
        orbitShapes();
        if (shooting)
            for (int i = 0; i < shapes.Length; ++i) shapes[i].GetComponent<Shape_Script>().shoot();

        if (health > p2Health)
        {

        }
        else
        {
            for (int i = 0; i < shapes.Length; ++i)
            {
                shapes[i].GetComponent<Shape_Script>().orbitSpeed = p2OrbitSpeed;
                shapes[i].GetComponent<Shape_Script>().maxShootCD = p2ShootCD;
            }
            patrol();
            meleeAttack();
        }
    }

    private void patrol()       //Boss moves left & right within boundaries
    {
        if (patrolling &&
            (transform.position.x >= maxX || transform.position.x <= minX))       //If patrolling outside boundaries
        {
            moveDirection = -moveDirection;     //Reverse move direction
            restartPatrolling();        //Move in other direction
        }
    }

    private void setShapeDirectiontoPoint(Vector3 point)     //Set shape velocity to move towards a point
    {
        for (int i = 0; i < shapes.Length; ++i)
        {
            shapes[i].GetComponent<Shape_Script>().orbitDirection = point - shapes[i].transform.position;
        }
    }

    private void orbitShapes()      //Shapes orbit centre of boss
    {
        if (!shapesInRadius()) setShapeDirectiontoPoint(transform.position);
        for (int i = 0; i < shapes.Length; ++i) shapes[i].GetComponent<Shape_Script>().moveShape();
    }

    private void meleeAttack()      //Charges at player
    {
        if (meleeCD <= 0)       //If meleeCD finished counting down
        {
            shooting = false;
            patrolling = false;     //Stop patrolling & shooting

            if (!meleeAttacking)        //If not currently attacking
            {
                setShapeDirectiontoPoint(transform.position);       //Centre shapes

                if (shapesInCentre())       //When shapes centred...
                {
                    targetPosition = player.transform.position;
                    setShapeDirectiontoPoint(targetPosition);       //Charge shapes at player
                    meleeAttacking = true;
                }
            }
            else        //If currently attacking
            {
                if (shapes[0].transform.position == targetPosition) targetPosition = transform.position;        //Recentre shapes when target hit position
                else if (shapesInCentre())       //When shapes recentred...
                {
                    meleeAttacking = false;
                    meleeCD = maxMeleeCD;       //Reset meleeCD

                    shooting = true;
                    patrolling = true;
                    restartPatrolling();        //Return to patrolling & shooting
                }
            }
        }
        else
        {
            meleeCD -= Time.deltaTime;      //Countdown meleeCD
        }
    }

    private void restartPatrolling()      //Set velocity
    {
        GetComponent<Rigidbody2D>().velocity = moveDirection * moveSpeed;
    }

    private bool shapesInRadius()       //Returns true if shapes are in radius
    {
        float distX = transform.position.x - shapes[0].transform.position.x,
            distY = transform.position.y - shapes[0].transform.position.y,
            distance = Mathf.Sqrt((distX * distX) + (distY * distY));

        return (distance <= radius);
    }

    private bool shapesInCentre()       //Returns true if shapes are in the centre of boss
    {
        return (shapes[0].transform.position == transform.position);
    }
}
