using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Script : MonoBehaviour {

    public float moveSpeed,
        minX,
        maxX,
        radius;
    public GameObject[] shapes;

    private Vector2 moveDirection;
    private bool patrolling = false;

    private void Start()
    {
        moveDirection = new Vector2(-1, 0);
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
            Vector2 distance = point - shapes[i].transform.position;
            shapes[i].GetComponent<Rigidbody2D>().velocity = distance.normalized * moveSpeed;
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
