﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad_Script : MonoBehaviour
{
    public int id;

    private GameObject linkedDoor;

	void Start ()
    {
        string doorName = "Door" + id.ToString();
        linkedDoor = GameObject.Find(doorName);
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(linkedDoor);
        }
    }
}
