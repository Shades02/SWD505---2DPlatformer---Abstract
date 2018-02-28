using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class SpriteColorChanger : MonoBehaviour {

    public Color mainColor;
    public Color shadingColor;
    public Sprite grayScaleSprite;
    public AnimationClip hi;

    private Color[] colorArray;
    private int t;
    private int colornum;

    private bool isFirstTextureMade;

	// Use this for initialization
	void Start () {
        colorArray = new Color[2] { mainColor, shadingColor };
        t = 0;
        isFirstTextureMade = false;
        colornum = 1;
	}

    void Update()
    {
        t++;
        if(t == 20)
        {
            t = 0;
            GetComponent<SpriteRenderer>().sprite = ChangeColour(colornum);
            colornum++;
        }

        if(colornum > 8)
        {
            colornum = 1;
        }
    }

    Color[] GetColors(int color)
    {
        Color[] outputColor = new Color[2];
        switch (color)
        {
            case 1:// blue
                outputColor[0] = RGBValuesToColorValues(new Vector4(15,40,229,255));
                outputColor[1] = RGBValuesToColorValues(new Vector4(63, 59, 235, 255));
                break;
            case 2://red
                outputColor[0] = RGBValuesToColorValues(new Vector4(205, 11, 11, 255));
                outputColor[1] = RGBValuesToColorValues(new Vector4(215, 60, 60, 255));
                break;
            case 3://yellow
                outputColor[0] = RGBValuesToColorValues(new Vector4(196, 204, 46, 255));
                outputColor[1] = RGBValuesToColorValues(new Vector4(208, 214, 88, 255));
                break;
            case 4://green
                outputColor[0] = RGBValuesToColorValues(new Vector4(46, 196, 46, 255));
                outputColor[1] = RGBValuesToColorValues(new Vector4(88, 208, 88, 255));
                break;
            case 5://orange
                outputColor[0] = RGBValuesToColorValues(new Vector4(206, 91, 22, 255));
                outputColor[1] = RGBValuesToColorValues(new Vector4(216, 124, 69, 255));
                break;
            case 6://purple
                outputColor[0] = RGBValuesToColorValues(new Vector4(159, 10, 229, 255));
                outputColor[1] = RGBValuesToColorValues(new Vector4(178, 59, 234, 255));
                break;
            case 7://Black
                outputColor[0] = RGBValuesToColorValues(new Vector4(51, 51, 51, 255));
                outputColor[1] = RGBValuesToColorValues(new Vector4(92, 92, 92, 255));
                break;
            case 8://White
                outputColor[0] = RGBValuesToColorValues(new Vector4(241, 241, 241, 255));
                outputColor[1] = RGBValuesToColorValues(new Vector4(221, 221, 221, 255));
                break;
        }

        return outputColor;
    }

    Vector4 RGBValuesToColorValues(Vector4 values) // helpful website https://www.rapidtables.com/web/color/RGB_Color.html
    {
        values.x /= 255;
        values.y /= 255;
        values.z /= 255;
        values.w /= 255;

        return values;
    }

    Sprite ChangeColour(int color)
    {
        Color[] newColorArray = GetColors(color);

        if (newColorArray != null)
        {
            Texture2D grayScaleTexture = grayScaleSprite.texture;
            Texture2D texture;
            if (isFirstTextureMade)
            {
                texture = GetComponent<SpriteRenderer>().sprite.texture;
            }
            else
            {
                isFirstTextureMade = true;
                texture = new Texture2D(grayScaleTexture.width, grayScaleTexture.height);
            }

            for (int y = 0; y < texture.height; ++y)
            {
                for (int x = 0; x < texture.width; ++x)
                {
                    texture.SetPixel(x, y, grayScaleTexture.GetPixel(x, y));

                    for (int curPos = 0; curPos < colorArray.Length; ++curPos)
                    {
                        if (grayScaleTexture.GetPixel(x, y) == colorArray[curPos])
                        {
                            texture.SetPixel(x, y, newColorArray[curPos]);
                        }
                    }

                }
            }

            texture.Apply();

            texture.wrapMode = grayScaleTexture.wrapMode;
            texture.filterMode = grayScaleTexture.filterMode;
            Vector2 pivotPoint = GetComponent<SpriteRenderer>().sprite.pivot;
            Debug.Log(pivotPoint.x / grayScaleSprite.pixelsPerUnit);
            Sprite output = Sprite.Create(texture, grayScaleSprite.rect,new Vector2(pivotPoint.x/ grayScaleSprite.rect.width , pivotPoint.y/ grayScaleSprite.rect.height), grayScaleSprite.pixelsPerUnit);

            return output;
        }


        return null;
    }
}
