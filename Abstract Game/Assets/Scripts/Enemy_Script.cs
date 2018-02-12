using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Script : MonoBehaviour
{
    public int health;
    public int detectRange;

    protected GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
}
