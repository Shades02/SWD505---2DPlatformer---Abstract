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
        }
    }

    private void patrol()       //Boss moves left and right within boundaries
    {
        if (transform.position.x >= maxX || transform.position.x <= minX)
        {
            moveDirection = -moveDirection;
            patrolling = false;
        }

        if (!patrolling) GetComponent<Rigidbody2D>().velocity = moveDirection * moveSpeed;
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
        if (meleeCD <= 0)
        {
            shooting = false;
            patrolling = false;

            if (!meleeAttacking)
            {
                setShapeDirectiontoPoint(transform.position);

                if (shapesInCentre())
                {
                    targetPosition = player.transform.position;
                    setShapeDirectiontoPoint(targetPosition);
                    meleeAttacking = true;
                }
            }
            else
            {
                if (shapes[0].transform.position == targetPosition) targetPosition = transform.position;
                else if (shapesInCentre())
                {
                    meleeAttacking = false;
                    meleeCD = maxMeleeCD;

                    shooting = true;
                    patrolling = true;
                }
            }
        }
        else
        {
            meleeCD -= Time.deltaTime;
        }
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
