using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Script : MonoBehaviour
{
    public float maxLifetime;
    private float currentLifetime = 0;
	
	void Update ()
    {
        currentLifetime += Time.deltaTime;

        if (currentLifetime >= maxLifetime)
            Destroy(gameObject);
	}
}
