using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Script : MonoBehaviour
{
    public enum pickupType
    {
        ammo, health
    }

    public pickupType thisPickupType;
    public Sprite healthSprite;
    public Sprite ammoSprite;

    private SpriteRenderer myRenderer;

	void Start ()
    {
        myRenderer = GetComponent<SpriteRenderer>();

        switch (thisPickupType)
        {
            case pickupType.ammo:
                myRenderer.sprite = ammoSprite;
                break;
            case pickupType.health:
                myRenderer.sprite = healthSprite;
                break;
        }
    }
}
