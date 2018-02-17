using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShoot_Enemy_Script : Shooting_Enemy_Script
{
    public int moveSpeed;


    void Update ()
    {
        deathCheck();
        getDirection();

        curShootCD -= Time.deltaTime;

        float xDistance = player.transform.position.x - transform.position.x;

        
    }
}
