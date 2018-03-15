using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum colour
{
    white, black, red, blue, yellow, green, orange, purple
}

public class Colour_Changer_Script : MonoBehaviour
{
    static public void setColour(GameObject thisObject, colour currentColour)
    {
        switch(currentColour)
        {
            case colour.white:
                thisObject.GetComponent<SpriteRenderer>().color = RGBValuesToColorValues(new Vector4(241, 241, 241, 255));   //White
                thisObject.layer = 20;
                break;
            case colour.black:
                thisObject.GetComponent<SpriteRenderer>().color = RGBValuesToColorValues(new Vector4(51, 51, 51, 255));      //Black;
                thisObject.layer = 21;
                break;
            case colour.red:
                thisObject.GetComponent<SpriteRenderer>().color = RGBValuesToColorValues(new Vector4(205, 11, 11, 255));     //Red ;
                thisObject.layer = 22;
                break;
            case colour.blue:
                thisObject.GetComponent<SpriteRenderer>().color = RGBValuesToColorValues(new Vector4(15, 40, 229, 255));     //Blue;
                thisObject.layer = 23;
                break;
            case colour.yellow:
                thisObject.GetComponent<SpriteRenderer>().color = RGBValuesToColorValues(new Vector4(196, 204, 46, 255));    //Yellow;
                thisObject.layer = 24;
                break;
            case colour.green:
                thisObject.GetComponent<SpriteRenderer>().color = RGBValuesToColorValues(new Vector4(46, 196, 46, 255));     //Green;
                thisObject.layer = 25;
                break;
            case colour.orange:
                thisObject.GetComponent<SpriteRenderer>().color = RGBValuesToColorValues(new Vector4(206, 91, 22, 255));     //Orange;
                thisObject.layer = 26;
                break;
            case colour.purple:
                thisObject.GetComponent<SpriteRenderer>().color = RGBValuesToColorValues(new Vector4(159, 10, 229, 255));    //Purple;
                thisObject.layer = 27;
                break;
        }
    }

    static public void setColourWithoutLayer(GameObject thisObject, colour currentColour)
    {
        switch (currentColour)
        {
            case colour.white:
                thisObject.GetComponent<SpriteRenderer>().color = RGBValuesToColorValues(new Vector4(241, 241, 241, 255));   //White
                break;
            case colour.black:
                thisObject.GetComponent<SpriteRenderer>().color = RGBValuesToColorValues(new Vector4(51, 51, 51, 255));      //Black;
                break;
            case colour.red:
                thisObject.GetComponent<SpriteRenderer>().color = RGBValuesToColorValues(new Vector4(205, 11, 11, 255));     //Red ;
                break;
            case colour.blue:
                thisObject.GetComponent<SpriteRenderer>().color = RGBValuesToColorValues(new Vector4(15, 40, 229, 255));     //Blue;
                break;
            case colour.yellow:
                thisObject.GetComponent<SpriteRenderer>().color = RGBValuesToColorValues(new Vector4(196, 204, 46, 255));    //Yellow;
                break;
            case colour.green:
                thisObject.GetComponent<SpriteRenderer>().color = RGBValuesToColorValues(new Vector4(46, 196, 46, 255));     //Green;
                break;
            case colour.orange:
                thisObject.GetComponent<SpriteRenderer>().color = RGBValuesToColorValues(new Vector4(206, 91, 22, 255));     //Orange;
                break;
            case colour.purple:
                thisObject.GetComponent<SpriteRenderer>().color = RGBValuesToColorValues(new Vector4(159, 10, 229, 255));    //Purple;
                break;
        }
    }

    static private Vector4 RGBValuesToColorValues(Vector4 values) // helpful website https://www.rapidtables.com/web/color/RGB_Color.html
    {
        values.x /= 255;
        values.y /= 255;
        values.z /= 255;
        values.w /= 255;

        return values;
    }

}
