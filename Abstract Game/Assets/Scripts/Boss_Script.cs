﻿using System.Collections;
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
    private Vector3 targetPosition,
        startPos;
    private bool patrolling = false,
        shooting = false,
        meleeAttacking = false,
        aggrovated = false;
    private int p2Health;
    private float meleeCD;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        moveDirection = new Vector2(-1, 0);
        for (int i = 0; i < shapes.Length; ++i)
        {
            health += shapes[i].GetComponent<Shape_Script>().health;
            shapes[i].GetComponent<Shape_Script>().orbitSpeed = p1OrbitSpeed;
            shapes[i].GetComponent<Shape_Script>().maxShootCD = p1ShootCD;
        }
        p2Health = health / 2;
        health = p2Health;
        startPos = transform.position;
    }

    private void FixedUpdate()
    {
        if (meleeAttacking &&
            meleeCD <= 0) pushShapesinDirection();
        else orbitShapes();

        if (aggrovated)
        {
            if (shooting)
                for (int i = 0; i < shapes.Length; ++i) shapes[i].GetComponent<Shape_Script>().shoot();

            if (health <= p2Health)
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
        else if (inDetectRange(player))
        {
            aggrovated = true;
            shooting = true;
        }
    }

    private void patrol()       //Boss moves left & right within boundaries
    {
        if (patrolling)
        {
            if (transform.position.x >= maxX || transform.position.x <= minX)       //If patrolling outside boundaries
            {
                moveDirection = -moveDirection;     //Reverse move direction
                restartPatrolling();        //Move in other direction
            }
        }
    }

    private void setShapeDirectiontoPoint(Vector3 point)     //Set shape velocity to move towards point
    {
        for (int i = 0; i < shapes.Length; ++i)
        {
            shapes[i].GetComponent<Shape_Script>().orbitDirection = point - shapes[i].transform.position;
        }
    }

    private void orbitShapes()      //Shapes orbit centre of boss
    {
        if (!shapesInRadius()) setShapeDirectiontoPoint(transform.position);
        pushShapesinDirection();
    }

    private void pushShapesinDirection()        //Set shape velocity to move direction
    {
        for (int i = 0; i < shapes.Length; ++i) shapes[i].GetComponent<Shape_Script>().moveShape();
    }

    private void meleeAttack()      //Charges at player
    {
        if (meleeCD <= 0)       //If meleeCD finished counting down
        {
            shooting = false;
            patrolling = false;
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;     //Stop patrolling & shooting

            if (!meleeAttacking)        //If not currently attacking
            {
                Debug.Log("MELEE START");
                setShapeDirectiontoPoint(transform.position);       //Centre shapes

                if (shapesatPoint(transform.position))       //When shapes centred...
                {
                    targetPosition = player.transform.position;
                    setShapeDirectiontoPoint(targetPosition);       //Charge shapes at player
                    meleeAttacking = true;
                }
            }
            else        //If currently attacking
            {
                if (shapesatPoint(targetPosition)) targetPosition = transform.position;        //Recentre shapes when target hit position
                else if (shapesatPoint(transform.position))       //When shapes recentred...
                {
                    Debug.Log("MELEE END");
                    meleeAttacking = false;
                    meleeCD = maxMeleeCD;       //Reset meleeCD

                    shooting = true;
                    sendShapestoStartPos();     //Send shapes back to edge
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
        patrolling = true;
        GetComponent<Rigidbody2D>().velocity = moveDirection * moveSpeed;
    }

    private void sendShapestoStartPos()     //Set shape velocity to move towards a edge
    {
        Vector3 distanceFromStart = transform.position - startPos;

        for (int i = 0; i < shapes.Length; ++i)
        {
            shapes[i].GetComponent<Shape_Script>().orbitDirection = (shapes[i].GetComponent<Shape_Script>().startPos + distanceFromStart) - shapes[i].transform.position;
        }
    }

    private bool shapesInRadius()       //Returns true if shapes in radius
    {
        float distX = transform.position.x - shapes[0].transform.position.x,
            distY = transform.position.y - shapes[0].transform.position.y,
            distance = Mathf.Sqrt((distX * distX) + (distY * distY));

        return (distance <= radius);
    }

    private bool shapesatPoint(Vector3 point)       //Returns true if shapes are at the point
    {
        return (shapes[0].transform.position.x >= point.x - 0.1 &&
            shapes[0].transform.position.y >= point.y - 0.1);
    }

    private bool inDetectRange(GameObject target)       //Returns true if target in detectRange
    {
        float distX = transform.position.x - target.transform.position.x,
            distY = transform.position.y - target.transform.position.y,
            distance = Mathf.Sqrt((distX * distX) + (distY * distY));
        
        return (distance <= detectRange);
    }
}