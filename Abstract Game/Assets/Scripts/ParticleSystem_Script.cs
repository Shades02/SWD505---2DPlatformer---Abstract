using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystem_Script : MonoBehaviour
{
    private Player_Script player;

	void Start ()
    {
        player = GameObject.Find("Player").GetComponent<Player_Script>();
	}

	void Update ()
    {
        Colour_Changer_Script.setParticleColour(gameObject, player.getColour());
	}
}
