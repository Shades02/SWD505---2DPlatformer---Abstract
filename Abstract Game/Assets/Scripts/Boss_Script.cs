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
    public List<GameObject> shapes;
    public GameObject tear;

    private Vector2 moveDirection;
    private Vector3 targetPosition,
        startPos;
    private bool patrolling = false,
        shooting = false,
        meleeAttacking = false,
        aggrovated = false;
    private int p2Health,
        meleePhase;
    private float meleeCD;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");        //Set player as player object
        moveDirection = new Vector2(-1, 0);     //Set starting move direction to left

        //Set health
        health = 0;
        foreach (GameObject x in shapes)
        {
            health += x.GetComponent<Shape_Script>().health;
            x.GetComponent<Shape_Script>().orbitSpeed = p1OrbitSpeed;
            x.GetComponent<Shape_Script>().maxShootCD = p1ShootCD;
        }

        meleePhase = 0;     //Set melee phase to start
        p2Health = health / 2;      //Set phase 2 health
        startPos = transform.position;      //Set start pos as starting pos
    }

    private void FixedUpdate()
    {
        //Death check
        if (health <= 0)        //If boss dies
        {
            GameObject newTear = Instantiate(tear, transform.position, Quaternion.identity);        //Creates tear

            soundManager.PlaySFX("BossDeath");       //Plays death roar
            Destroy(gameObject, 2.26f);     //Destroy game object
        }

        //AI
        //Shape Control
        if (meleeAttacking) pushShapesinDirection();      //Push shapes out of radius if melee attacking
        else orbitShapes();     //Orbit shapes within radius if not melee attacking

        //Main Control
        if (aggrovated)     //If already aggrovated
        {
            if (shooting)
               foreach (GameObject x in shapes) x.GetComponent<Shape_Script>().shoot();

            if (health <= p2Health)     //If health drops below phase 2 boundaries
            {
                foreach (GameObject x in shapes)        //Change orbit and shoot speed
                {
                    x.GetComponent<Shape_Script>().orbitSpeed = p2OrbitSpeed;
                    x.GetComponent<Shape_Script>().maxShootCD = p2ShootCD;
                }
                patrol();           //Boss patrols
                meleeAttack();      //Performs melee attack
            }
        }
        else if (inDetectRange(player))     //If boss sees player when unaggrovated
        {
            aggrovated = true;
            shooting = true;        //Get aggrovated and start shooting

            //soundManager.PlaySFX("BossAlerted");       //Play aggrovated roar
        }
        //If neither, boss is dormant
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
        targetPosition = point;     //Set target position to point
        foreach (GameObject x in shapes) x.GetComponent<Shape_Script>().orbitDirection = point - x.transform.position;      //Set each shape orbit direction towards point
    }

    private void orbitShapes()      //Shapes orbit centre of boss
    {
        if (!shapesInRadius()) setShapeDirectiontoPoint(transform.position);
        pushShapesinDirection();
    }

    private void pushShapesinDirection()        //Set shape velocity to move direction
    {
        foreach (GameObject x in shapes) x.GetComponent<Shape_Script>().moveShape();
    }

    private void meleeAttack()      //Charges at player
    {
        if (meleeAttacking)       //If meleeCD finished counting down
        {
            switch (meleePhase)
            {
                case 0:
                    Debug.Log("MELEE PHASE 0");

                    shooting = false;
                    patrolling = false;
                    GetComponent<Rigidbody2D>().velocity = Vector3.zero;     //Stop patrolling & shooting

                    setShapeDirectiontoPoint(transform.position);       //Centre shapes

                    ++meleePhase;       //Proceed to next phase
                    break;
                case 1:
                    if (shapesatPoint(targetPosition))       //When shapes centred...
                    {
                        Debug.Log("MELEE PHASE 1");
                        
                        setShapeDirectiontoPoint(player.transform.position);
                        pushShapesinDirection();       //Charge shapes at player
                        //soundManager.PlaySFX("BossMelee");      //Play melee attack sound

                        ++meleePhase;       //Proceed to next phase
                    }
                    break;
                case 2:
                    if (shapesatPoint(targetPosition))      //When shapes hit attack position...
                    {
                        Debug.Log("MELEE PHASE 2");

                        setShapeDirectiontoPoint(transform.position);        //Recentre shapes when target hit position

                        ++meleePhase;       //Proceed to next phase
                    }
                    break;
                case 3:
                    if (shapesatPoint(targetPosition))       //When shapes recentred...
                    {
                        Debug.Log("MELEE PHASE 3");
                        meleeAttacking = false;
                        meleeCD = maxMeleeCD;       //Reset meleeCD

                        shooting = true;
                        sendShapestoStartPos();     //Send shapes back to edge
                        restartPatrolling();        //Return to patrolling & shooting

                        meleePhase = 0;     //Reset melee phase for next time
                    }
                    break;
                default:
                    break;
            }
        }
        else
        {
            meleeCD -= Time.deltaTime;      //Countdown meleeCD

            if (meleeCD <= 0) meleeAttacking = true;
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

        foreach (GameObject x in shapes)
        {
            x.GetComponent<Shape_Script>().orbitDirection = (x.GetComponent<Shape_Script>().startPos + distanceFromStart) - x.transform.position;
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
        if (shapes[0].transform.position.x >= point.x - 0.1 &&
            shapes[0].transform.position.x <= point.x + 0.1 &&
            shapes[0].transform.position.y >= point.y - 0.1 &&
            shapes[0].transform.position.y <= point.y + 0.1)
        {
            return true;
        }
        else return false;
    }

    private bool inDetectRange(GameObject target)       //Returns true if target in detectRange
    {
        float distX = transform.position.x - target.transform.position.x,
            distY = transform.position.y - target.transform.position.y,
            distance = Mathf.Sqrt((distX * distX) + (distY * distY));
        
        return (distance <= detectRange);
    }
}
