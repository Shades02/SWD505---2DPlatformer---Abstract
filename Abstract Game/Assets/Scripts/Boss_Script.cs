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
    public List<GameObject> shapes;
    public GameObject tear;

    private Vector2 moveDirection;
    private Vector3 targetPosition,
        startPos;
    private bool patrolling = false,
        shooting = false,
        meleeAttacking = false,
        aggrovated = false;
    private int p2Health;
    private float meleeCD;
    private Sound_Manager_Script soundManager;

    private void Start()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<Sound_Manager_Script>();
        player = GameObject.FindGameObjectWithTag("Player");
        moveDirection = new Vector2(-1, 0);
        foreach (GameObject x in shapes)
        {
            health += x.GetComponent<Shape_Script>().health;
            x.GetComponent<Shape_Script>().orbitSpeed = p1OrbitSpeed;
            x.GetComponent<Shape_Script>().maxShootCD = p1ShootCD;
        }

        p2Health = health / 2;
        health = 2;
        startPos = transform.position;
    }

    private void FixedUpdate()
    {
        if (meleeAttacking &&
            meleeCD <= 0) pushShapesinDirection();      //Push shapes out of radius if melee attacking
        else orbitShapes();     //Orbit shapes within radius if not melee attacking

        if (aggrovated)     //If already aggrovated
        {
            //if (shooting)
            //    foreach (GameObject x in shapes) x.GetComponent<Shape_Script>().shoot();

            if (health <= p2Health)     //If health drops below phase 2 boundaries
            {
                foreach (GameObject x in shapes)        //Change orbit and shoot speed
                {
                    x.GetComponent<Shape_Script>().orbitSpeed = p2OrbitSpeed;
                    x.GetComponent<Shape_Script>().maxShootCD = p2ShootCD;
                }
                patrol();       //Boss patrols
                meleeAttack();      //Performs melee attack
            }
        }
        else if (inDetectRange(player))     //If boss sees player when unaggrovated
        {
            aggrovated = true;
            shooting = true;        //Get aggrovated and start shooting

            soundManager.PlaySFX("BossAlerted");       //Play aggrovated roar
        }

        shapeDeathCheck();
        if (health <= 0)        //If boss dies
        {
            GameObject newTear = Instantiate(tear, transform.position, Quaternion.identity);        //Creates tear

            soundManager.PlaySFX("BossDeath");       //Plays death roar
            Destroy(gameObject, 2.26f);     //Destroy game object
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
        foreach (GameObject x in shapes) x.GetComponent<Shape_Script>().orbitDirection = point - x.transform.position;
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
                    setShapeDirectiontoPoint(targetPosition);
                    pushShapesinDirection();       //Charge shapes at player
                    meleeAttacking = true;
                    soundManager.PlaySFX("BossMelee");      //Play melee attack sound
                }
            }
            else        //If currently attacking
            {
                if (shapesatPoint(targetPosition)) setShapeDirectiontoPoint(transform.position);        //Recentre shapes when target hit position
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

        foreach (GameObject x in shapes)
        {
            x.GetComponent<Shape_Script>().orbitDirection = (x.GetComponent<Shape_Script>().startPos + distanceFromStart) - x.transform.position;
        }
    }

    internal void shapeDeathCheck()        //Reorganises shapes array if one dies
    {
        foreach (GameObject x in shapes)
        {
            if (x.activeSelf &&
                x.GetComponent<Shape_Script>().health <= 0)
            {
                x.SetActive(false);
                soundManager.PlaySFX("BossAlerted");
            }
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
        return (shapes[0].transform.position.x >= point.x - 0.2 &&
            shapes[0].transform.position.y >= point.y - 0.2);
    }

    private bool inDetectRange(GameObject target)       //Returns true if target in detectRange
    {
        float distX = transform.position.x - target.transform.position.x,
            distY = transform.position.y - target.transform.position.y,
            distance = Mathf.Sqrt((distX * distX) + (distY * distY));
        
        return (distance <= detectRange);
    }
}
