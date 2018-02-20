using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourUpdate_Script : MonoBehaviour
{
    private SpriteRenderer myRenderer;

    public Sprite whiteSprite;
    public Sprite blackSprite;
    public Sprite redSprite;
    public Sprite blueSprite;
    public Sprite yellowSprite;
    public Sprite orangeSprite;
    public Sprite greenSprite;
    public Sprite purpleSprite;

    void Start ()
    {
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
	}
	
    internal void updateColour(colour currentColour)
    {
        switch(currentColour)
        {
            case colour.white:
                myRenderer.sprite = whiteSprite;
                gameObject.layer = 20;
                break;
            case colour.black:
                myRenderer.sprite = blackSprite;
                gameObject.layer = 21;
                break;
            case colour.red:
                myRenderer.sprite = redSprite;
                gameObject.layer = 22;
                break;
            case colour.blue:
                myRenderer.sprite = blueSprite;
                gameObject.layer = 23;
                break;
            case colour.yellow:
                myRenderer.sprite = yellowSprite;
                gameObject.layer = 24;
                break;
            case colour.orange:
                myRenderer.sprite = orangeSprite;
                gameObject.layer = 26;
                break;
            case colour.green:
                myRenderer.sprite = greenSprite;
                gameObject.layer = 25;
                break;
            case colour.purple:
                myRenderer.sprite = purpleSprite;
                gameObject.layer = 27;
                break;
        }
    }
}
