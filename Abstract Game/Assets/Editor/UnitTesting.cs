﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

public class UnitTesting : MonoBehaviour
{
    //BulletScript Tests
    //Start
    [UnityTest]
    public IEnumerator setTagBulletTest()
    {
        //Arrange
        string tag = "EnemyBullet";     //existing tag i'll use for testing
        var testBullet = new GameObject().AddComponent<Bullet_Script>();

        yield return null;

        //Act
        testBullet.setTag(tag);

        //Assert
        Assert.AreEqual(tag, testBullet.tag);
    }

    [UnityTest]
    public IEnumerator setColourBulletTest()
    {
        //Arrange
        var testObject = new GameObject();
        testObject.AddComponent<SpriteRenderer>();
        testObject.AddComponent<Bullet_Script>();
        colour testColour = colour.red;

        yield return null;

        //Act
        testObject.GetComponent<Bullet_Script>().setColour(testColour, true);

        //Assert
        Assert.AreEqual(testColour, testObject.GetComponent<Bullet_Script>().getColour());
    }

    [UnityTest]
    public IEnumerator getColourBulletTest()
    {
        //Arrange
        var testObject = new GameObject();
        testObject.AddComponent<SpriteRenderer>();
        testObject.AddComponent<Bullet_Script>();
        colour testColour = colour.red;

        yield return null;

        //Act
        testObject.GetComponent<Bullet_Script>().setColour(testColour, true);

        //Assert
        Assert.AreEqual(testColour, testObject.GetComponent<Bullet_Script>().getColour());
    }
    //End


    //ColourChangerScript Tests
    //Start
    [UnityTest]
    public IEnumerator setColourAndLayerTest()
    {
        //Arrange


        yield return null;

        //Act

        //Assert
    }

    [UnityTest]
    public IEnumerator setColourWithoutLayerTest()
    {
        //Arrange


        yield return null;

        //Act

        //Assert
    }

    [UnityTest]
    public IEnumerator RGBToColourValuesTest()
    {
        //Arrange


        yield return null;

        //Act

        //Assert
    }
    //End


    //DispenserScript Tests
    //Start
    [UnityTest]
    public IEnumerator dispenseTest()
    {
        //Arrange


        yield return null;

        //Act

        //Assert
    }
    //End


    //EnemyScript Tests
    //Start
    [UnityTest]
    public IEnumerator takeDamageTest()
    {
        //Arrange


        yield return null;

        //Act

        //Assert
    }

    [UnityTest]
    public IEnumerator returnDirectionTest()
    {
        //Arrange


        yield return null;

        //Act

        //Assert
    }
    //End


    //MainMenuScript Tests
    //Start
    [UnityTest]
    public IEnumerator startGameTest()
    {
        //Arrange


        yield return null;

        //Act

        //Assert
    }

    [UnityTest]
    public IEnumerator quitGameTest()
    {
        //Arrange


        yield return null;

        //Act

        //Assert
    }

    [UnityTest]
    public IEnumerator settingsTest()
    {
        //Arrange


        yield return null;

        //Act

        //Assert
    }
    //End


    //PauseMenuScript Tests
    //Start
    [UnityTest]
    public IEnumerator resumeTest()
    {
        //Arrange


        yield return null;

        //Act

        //Assert
    }

    [UnityTest]
    public IEnumerator quitGameFromPauseTest()
    {
        //Arrange


        yield return null;

        //Act

        //Assert
    }

    [UnityTest]
    public IEnumerator settingsFromPauseTest()
    {
        //Arrange


        yield return null;

        //Act

        //Assert
    }

    [UnityTest]
    public IEnumerator returnToPauseTest()
    {
        //Arrange


        yield return null;

        //Act

        //Assert
    }

    [UnityTest]
    public IEnumerator setFullscreenTest()
    {
        //Arrange


        yield return null;

        //Act

        //Assert
    }

    [UnityTest]
    public IEnumerator setLowQualityTest()
    {
        //Arrange


        yield return null;

        //Act

        //Assert
    }

    [UnityTest]
    public IEnumerator restartLevelTest()
    {
        //Arrange


        yield return null;

        //Act

        //Assert
    }

    [UnityTest]
    public IEnumerator returnToMenuTest()
    {
        //Arrange


        yield return null;

        //Act

        //Assert
    }
    //End


    //PlayerScript Tests
    //Start
    [UnityTest]
    public IEnumerator takeDamagePlayerTest()
    {
        //Arrange
        var testPlayer = new GameObject().AddComponent<Player_Script>();
        int testHealth = 5;
        testPlayer.maxHealth = testHealth;      //set player health to the test value
        int testDamage = 2;

        yield return null;

        //Act
        testPlayer.takeDamage(testDamage);

        //Assert
        Assert.Less(testPlayer.getHealth(), testHealth);        //if player health is less, the damage has been taken
    }

    [UnityTest]
    public IEnumerator getHealthPlayerTest()
    {
        //Arrange
        var testPlayer = new GameObject().AddComponent<Player_Script>();
        int testHealth = 5;
        testPlayer.maxHealth = testHealth;

        yield return null;

        //Act

        //Assert
        Assert.AreEqual(testHealth, testPlayer.getHealth());
    }

    [UnityTest]
    public IEnumerator getAmmoPlayerTest()
    {
        //Arrange
        var testPlayer = new GameObject().AddComponent<Player_Script>();
        int testAmmo = 20;

        yield return null;

        //Act

        //Assert
    }

    [UnityTest]
    public IEnumerator getColourPlayerTest()
    {
        //Arrange


        yield return null;

        //Act

        //Assert
    }

    [UnityTest]
    public IEnumerator getDirectionPlayerTest()
    {
        //Arrange


        yield return null;

        //Act

        //Assert
    }
    //End


    //SoundManagerScript Tests
    //Start
    [UnityTest]
    public IEnumerator playSFXTest()
    {
        //Arrange


        yield return null;

        //Act

        //Assert
        Assert.AreEqual(1, 2);
    }
    //End


    //SpikeScript Tests
    //Start
    [UnityTest]
    public IEnumerator getDamageTest()
    {
        //Arrange
        var testSpike = new GameObject().AddComponent<Spike_Script>();
        int damageValue = 5;
        testSpike.damage = damageValue;

        yield return null;

        //Act
        

        //Assert
        Assert.AreEqual(damageValue, testSpike.getDamage());
    }
    //End

}
