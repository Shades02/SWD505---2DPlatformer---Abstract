using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Script : Enemy_Script
{

    public float moveSpeed,
        minX,
        maxX,
        radius,
        orbitSpeed;
    public GameObject[] shapes;

    private Vector2 moveDirection;
    private bool patrolling = false;
    private int p2Health;

    private void Start()
    {
        moveDirection = new Vector2(-1, 0);
        for (int i = 0; i < shapes.Length; ++i)
        {
            health += shapes[i].GetComponent<Shape_Script>().health;
            shapes[i].GetComponent<Shape_Script>().orbitSpeed = orbitSpeed;
        }
        p2Health = health / 2;
        for (int i = 0; i < shapes.Length; i++) shapes[i].GetComponent<Shape_Script>().shoot(player);
    }

    private void FixedUpdate()
    {
        orbitShapes();
        

        if (health > p2Health)
        {

        }
        else
        {
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

    private bool shapesInRadius()       //Returns true if shapes are in radius
    {
        float distX = transform.position.x - shapes[0].transform.position.x,
            distY = transform.position.y - shapes[0].transform.position.y,
            distance = Mathf.Sqrt((distX * distX) + (distY * distY));

        return (distance <= radius);
    }
}
