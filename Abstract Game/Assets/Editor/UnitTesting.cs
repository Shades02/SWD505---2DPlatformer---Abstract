﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using UnityEngine.SceneManagement;

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
        Assert.AreEqual(testColour, testObject.GetComponent<Bullet_Script>().getColour());             //need to "getColour" from bullet
    }
    //End


    //ColourChangerScript Tests
    //Start
    [UnityTest]
    public IEnumerator setColourAndLayerTest()
    {
        //Arrange
        var testObject = new GameObject();
        testObject.AddComponent<SpriteRenderer>();
        testObject.GetComponent<SpriteRenderer>().color = Color.red;           //set an initial colour
        colour testColour = colour.blue;        //prepare a test colour

        yield return null;

        //Act
        Colour_Changer_Script.setColour(testObject, testColour);

        //Assert
        Assert.AreNotEqual(Color.red, testObject.GetComponent<SpriteRenderer>().color);         //if not equal the colour has been changed
    }

    [UnityTest]
    public IEnumerator setColourWithoutLayerTest()
    {
        //Arrange
        var testObject = new GameObject();
        testObject.AddComponent<SpriteRenderer>();
        testObject.GetComponent<SpriteRenderer>().color = Color.red;           //set an initial colour
        colour testColour = colour.blue;        //prepare a test colour

        yield return null;

        //Act
        Colour_Changer_Script.setColour(testObject, testColour);

        //Assert
        Assert.AreNotEqual(Color.red, testObject.GetComponent<SpriteRenderer>().color);         //if not equal the colour has been changed
    }
    //End

    //DispenserScript Tests
    //Start
    [UnityTest]
    public IEnumerator dispenseTest()
    {
        //Arrange
        var testDispenser = new GameObject();
        testDispenser.AddComponent<Dispenser_Script>();
        var testPrefab = new GameObject();            //create test prefab
        testPrefab.tag = "PaintDrop";            //set tag so i can find it again
        testDispenser.GetComponent<Dispenser_Script>().paintDrop = testPrefab;          //set as prefab for dispenser

        yield return null;

        //Act
        testDispenser.GetComponent<Dispenser_Script>().dispense();
        var spawnedPrefab = GameObject.FindWithTag("PaintDrop");         //find spawned prefab

        //Assert
        Assert.AreEqual(testPrefab, spawnedPrefab);      //check prefabs are the same
    }
    //End


    //EnemyScript Tests
    //Start
    [UnityTest]
    public IEnumerator takeDamageEnemyTest()
    {
        //Arrange
        var testEnemy = new GameObject();
        testEnemy.AddComponent<Enemy_Script>();
        int testHealth = 5;
        testEnemy.GetComponent<Enemy_Script>().health = testHealth;      //set health
        int testDamage = 2;

        yield return null;

        //Act
        testEnemy.GetComponent<Enemy_Script>().takeDamage(testDamage);

        //Assert
        Assert.AreEqual((testHealth - testDamage), testEnemy.GetComponent<Enemy_Script>().health);
    }

    [UnityTest]
    public IEnumerator returnDirectionEnemyTest()
    {
        //Arrange
        var testEnemy = new GameObject();
        testEnemy.AddComponent<Enemy_Script>();
        bool testDirection = false;
        testEnemy.GetComponent<Enemy_Script>().setDirection(testDirection);

        yield return null;

        //Act

        //Assert
        Assert.AreEqual(testDirection, testEnemy.GetComponent<Enemy_Script>().returnDirection());
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

        //This method uses scene manager to load a different scene, so it is unable to be properly tested
    }

    [UnityTest]
    public IEnumerator quitGameTest()
    {
        //Arrange


        yield return null;

        //Act

        //Assert

        //This can't be tested since it calls application.quit
    }

    [UnityTest]
    public IEnumerator settingsTest()
    {
        //Arrange
        var testObject = new GameObject();
        var testSettingsCanvas = new GameObject();
        testObject.AddComponent<Main_Menu_Script>();
        testObject.GetComponent<Main_Menu_Script>().settingsMenuCanvas = testSettingsCanvas;

        yield return null;

        //Act
        testSettingsCanvas.SetActive(false);        //intially false
        testObject.GetComponent<Main_Menu_Script>().settings();         //should set to true

        //Assert
        Assert.AreEqual(true, testSettingsCanvas.activeSelf);
    }
    //End


    /*

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

        //This can't be tested since it calls application.quit
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

        //This can't be tested since fullscreen mode doesn't function in the editor
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

        //This method uses scene manager to load a different scene, so it is unable to be properly tested
    }

    [UnityTest]
    public IEnumerator returnToMenuTest()
    {
        //Arrange


        yield return null;

        //Act

        //Assert

        //This method uses scene manager to load a different scene, so it is unable to be properly tested
    }
    //End

    */

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
        testPlayer.setHealth(testHealth);

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
        testPlayer.setAmmo(testAmmo);

        yield return null;

        //Act

        //Assert
        Assert.AreEqual(testAmmo, testPlayer.getAmmo());
    }

    [UnityTest]
    public IEnumerator getColourPlayerTest()
    {
        //Arrange
        var testPlayer = new GameObject().AddComponent<Player_Script>();
        colour testColour = colour.red;
        testPlayer.setColour(testColour);

        yield return null;

        //Act

        //Assert
        Assert.AreEqual(testColour, testPlayer.getColour());
    }

    [UnityTest]
    public IEnumerator getDirectionPlayerTest()
    {
        //Arrange
        var testPlayer = new GameObject().AddComponent<Player_Script>();
        bool testDirection = false;      //true = facing right, false = facing left
        testPlayer.setDirection(testDirection);

        yield return null;

        //Act

        //Assert
        Assert.AreEqual(testDirection, testPlayer.returnDirection());
    }
    //End


    //SoundManagerScript Tests
    //Start
    [UnityTest]
    public IEnumerator playSFXTest()
    {
        //Arrange
        var testSoundManager = new GameObject();
        testSoundManager.AddComponent<Sound_Manager_Script>();
        var testPrefab = new GameObject();            //create test prefab
        testPrefab.tag = "Tear";                 //set tag so i can find it again
        testSoundManager.GetComponent<Sound_Manager_Script>().SFXPrefab = testPrefab;          //set as prefab for dispenser
        testSoundManager.GetComponent<Sound_Manager_Script>().SFXNames.Add("Test");             //Add the name of the sound clip for testing

        yield return null;

        //Act
        testSoundManager.GetComponent<Sound_Manager_Script>().PlaySFX("Test");
        var spawnedPrefab = GameObject.FindWithTag("Tear");         //find spawned prefab

        //Assert
        Assert.AreEqual(testPrefab, spawnedPrefab);      //check prefabs are the same
    }
    //End

}
