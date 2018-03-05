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
            health += shapes[i].GetComponent<Enemy_Script>().health;
            shapes[i].GetComponent<Shape_Script>().orbitSpeed = orbitSpeed;
        }
        p2Health = health / 2;
    }

    private void FixedUpdate()
    {
        patrol();
        orbitShapes();
    }

    private void patrol()       //Boss moves left and right within boundaries
    {
        if (!patrolling) GetComponent<Rigidbody2D>().velocity = moveDirection * moveSpeed;
        else if (transform.position.x >= maxX || transform.position.x <= minX)
        {
            moveDirection = -moveDirection;
            patrolling = false;
        }
    }

    private void setShapeVelocitytoPoint(Vector3 point)     //Set shape velocity to move towards a point
    {
        for (int i = 0; i < shapes.Length; ++i)
        {
            shapes[i].GetComponent<Shape_Script>().orbitDirection = point - shapes[i].transform.position;
            shapes[i].GetComponent<Shape_Script>().orbitDirection.x /= Mathf.Abs(shapes[i].GetComponent<Shape_Script>().orbitDirection.x);
            shapes[i].GetComponent<Shape_Script>().orbitDirection.y /= Mathf.Abs(shapes[i].GetComponent<Shape_Script>().orbitDirection.y);
        }
    }

    private void orbitShapes()      //Shapes orbit centre of boss
    {
        if (!shapesInRadius()) setShapeVelocitytoPoint(transform.position);
        else
        {
            for (int i = 0; i < shapes.Length; ++i) shapes[i].GetComponent<Shape_Script>().moveShape();
        }
    }

    private bool shapesInRadius()       //Returns true if shapes are in radius
    {
        float distX = transform.position.x - shapes[0].transform.position.x,
            distY = transform.position.y - shapes[0].transform.position.y,
            distance = Mathf.Sqrt((distX * distX) + (distY * distY));

        return (distance <= radius);
    }
}
