using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Script : MonoBehaviour
{
    private GameObject player;
    private Vector2 origin;
    public float maxX;

    void Start ()
    {
        origin = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update ()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
	}

    private void LateUpdate()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, origin.x, maxX),              //left point = origin   right point = maxX
            Mathf.Clamp(transform.position.y, origin.y, 99),                //bottom point = origin     top point = 99
            -10);                                                           //-10
    }
}
