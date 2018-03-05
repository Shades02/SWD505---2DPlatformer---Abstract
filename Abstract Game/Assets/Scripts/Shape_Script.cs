using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape_Script : Enemy_Script
{
    internal float orbitSpeed;
    internal Vector3 orbitDirection;

    internal void moveShape()
    {
        transform.Translate(orbitDirection.normalized * orbitSpeed * Time.deltaTime);
    }
}
