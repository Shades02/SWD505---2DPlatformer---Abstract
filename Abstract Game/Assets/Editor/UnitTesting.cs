using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class UnitTesting : MonoBehaviour
{
    //BulletScript Tests
    //Start
    [Test]
    public void setTagBulletTest()
    {
        //Arrange
        string tag = "Red";
        var testObject = new GameObject();
        //GameObject go = Instantiate()  ???
        testObject.AddComponent<Bullet_Script>();

        //Act
        testObject.GetComponent<Bullet_Script>().setTag(tag);

        //Assert
        Assert.That(testObject.tag == tag);
    }

    [Test]
    public void setColourBulletTest()
    {
        //Arrange
        var testObject = new GameObject();
        testObject.AddComponent<SpriteRenderer>();
        testObject.AddComponent<Bullet_Script>();
        colour testColour = colour.red;

        //Act
        testObject.GetComponent<Bullet_Script>().setColour(testColour, true);

        //Assert
        Assert.That(testObject.GetComponent<Bullet_Script>().getColour() == testColour);
    }

    [Test]
    public void getColourBulletTest()
    {
        //Arrange
        var testObject = new GameObject();
        testObject.AddComponent<SpriteRenderer>();
        testObject.AddComponent<Bullet_Script>();
        colour testColour = colour.red;

        //Act
        testObject.GetComponent<Bullet_Script>().setColour(testColour, true);

        //Assert
        Assert.That(testObject.GetComponent<Bullet_Script>().getColour() == testColour);
    }
    //End


    //ColourChangerScript Tests
    //Start
    [Test]
    public void setColourAndLayerTest()
    {

    }

    [Test]
    public void setColourWithoutLayerTest()
    {

    }

    [Test]
    public void RGBToColourValuesTest()
    {

    }
    //End


    //DispenserScript Tests
    //Start
    [Test]
    public void dispenseTest()
    {

    }
    //End


    //EnemyScript Tests
    //Start
    [Test]
    public void takeDamageTest()
    {

    }

    [Test]
    public void returnDirectionTest()
    {

    }
    //End


    //MainMenuScript Tests
    //Start
    [Test]
    public void startGameTest()
    {

    }

    [Test]
    public void quitGameTest()
    {

    }

    [Test]
    public void settingsTest()
    {

    }
    //End


    //PauseMenuScript Tests
    //Start
    [Test]
    public void resumeTest()
    {

    }

    [Test]
    public void quitGameFromPauseTest()
    {

    }

    [Test]
    public void settingsFromPauseTest()
    {

    }

    [Test]
    public void returnToPauseTest()
    {

    }

    [Test]
    public void setFullscreenTest()
    {

    }

    [Test]
    public void setLowQualityTest()
    {

    }

    [Test]
    public void restartLevelTest()
    {

    }

    [Test]
    public void returnToMenuTest()
    {

    }
    //End


    //PlayerScript Tests
    //Start
    [Test]
    public void takeDamagePlayerTest()
    {

    }

    [Test]
    public void getHealthPlayerTest()
    {

    }

    [Test]
    public void getAmmoPlayerTest()
    {

    }

    [Test]
    public void getColourPlayerTest()
    {

    }

    [Test]
    public void getDirectionPlayerTest()
    {

    }
    //End


    //SoundManagerScript Tests
    //Start
    [Test]
    public void playSFXTest()
    {

    }
    //End


    //SpikeScript Tests
    //Start
    [Test]
    public void getDamageTest()
    {
        //Arrange
        var testObject = new GameObject();
        testObject.AddComponent<Spike_Script>();

        //Act
        

        //Assert
        Assert.That(testObject.tag == tag);
    }
    //End

}
