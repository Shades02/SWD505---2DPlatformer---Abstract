using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Script : MonoBehaviour
{
    private GameObject player;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update ()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
	}

    private void LateUpdate()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minX, maxX),                  //left point = minX       right point = maxX
            Mathf.Clamp(transform.position.y, minY, maxY),                  //bottom point = minY     top point = maxY
            -10);                                                           //-10
    }
}
