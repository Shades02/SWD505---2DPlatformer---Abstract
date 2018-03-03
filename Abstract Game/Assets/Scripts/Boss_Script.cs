using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Script : MonoBehaviour {

    public float moveSpeed,
        minX,
        maxX;
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

    private void setShapeVelocitytoPoint(Vector2 point)
    {
        for (int i = 0; i < shapes.Length; ++i)
        {

        }
    }
}
