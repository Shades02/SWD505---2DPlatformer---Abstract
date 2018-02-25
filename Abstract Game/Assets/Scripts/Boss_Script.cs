using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Script : Enemy_Script
{
    public GameObject shape1;
    public GameObject shape2;
    public GameObject shape3;
    public GameObject shape4;
    public GameObject shape5;
    public GameObject shape6;
    public GameObject shape7;
    public GameObject shape8;

    private int numberShapes = 8;
    private int phaseTransition = 4;
	
	void Update ()
    {
        getDirection();
        deathCheck();

		if(numberShapes > phaseTransition)      //Phase 1 when more than 4 shapes
        {
            //Phase 1
            //Stationary
            //Shapes fire projectiles - 0.5 seconds
        }
        else        //Phase 2 when 4 or less shapes
        {
            //Phase 2
            //50% health - 4/8 shapes left
            //Flying - 1.25% player speed
            //Shapes fire projectiles - 0.3 seconds
            //Melee attack - 3 seconds 
        }
        
	}

    internal void shapeDestroyed()      //Every time a shape is destroyed this will be called
    {
        numberShapes -= 1;
    }
}
