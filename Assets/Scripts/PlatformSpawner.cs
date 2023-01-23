﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class PlatformSpawner : MonoBehaviour
{
    public List<GameObject> platformList;
    public GameObject[] platformListArr;

    public (GameObject[] easy, GameObject[] normal, GameObject[] hard, GameObject[] uber) platformArray;

    private Transform tr;

    [SerializeField]
    private float spawnDistance = 5f;

    private GameObject platformToSpawn;
    private GameObject newPlatform;
    private GameObject[] platformsSpawnCap = new GameObject[3];

    private int initiatedCount = 0;    
    public GameObject lastSpawnedPlatform;

    [Header("Platform Sprites")]
    [SerializeField] private Sprite platformSprites;

    LevelManager lm;

    void Start()
    {
        tr = GetComponent<Transform>();
        lm = LevelManager.instance;


        platformArray.easy = Resources.LoadAll<GameObject>("Platforms/1_Easy");
        platformArray.normal = Resources.LoadAll<GameObject>("Platforms/2_Normal");
        platformArray.hard = Resources.LoadAll<GameObject>("Platforms/3_Hard");

        var easyNormal = platformArray.easy.Union(platformArray.normal).ToArray();
        var easyNormalHard = easyNormal.Union(platformArray.hard).ToArray();
        platformArray.uber = easyNormalHard;

        platformListArr = platformArray.easy;
 
    }



    void Update()
    {
        if(lm.playerControl.getPoint() >= 50 && lm.playerControl.getPoint() < 125)
        {
            if(platformListArr != platformArray.normal)
            {
                platformListArr = platformArray.normal;
                Debug.Log("Loaded Normal Platforms");
            }

        }
        else if (lm.playerControl.getPoint() >= 125 && lm.playerControl.getPoint() < 250)
        {
            if(platformListArr != platformArray.hard)
            {
                platformListArr = platformArray.hard;
                Debug.Log("Loaded Hard Platform");
            }

        }
        else if (lm.playerControl.getPoint() >= 250)
        {
            if (platformListArr != platformArray.uber)
            {
                platformListArr = platformArray.uber;
                Debug.Log("Loaded Uber Platform");
            }

        }


    }

    public void initiateSpawn()
    {

        if (initiatedCount != platformsSpawnCap.Length - 1)
        {
            if (initiatedCount < 1)
            {
                spawnDistance = 5;
                spawnRandomPlatform();
            }

            initiatedCount++;
        }
        else initiatedCount = 0;


        //Debug.Log($"Initiated count {initiatedCount}\n");
    }

    int randomNumber;
    int lastNumber;
    int maxAttempts = 5;
    System.Random rnd = new System.Random();
    public int selectUniqueRandomNumber(int limit)
    {

        for (int i = 0; randomNumber == lastNumber && i < maxAttempts; i++)
        {

            randomNumber = rnd.Next(0, limit);
        }

        lastNumber = randomNumber;

        //if(randomNumber > 10) Debug.LogWarning($"Generated Number {randomNumber} \n");

        return randomNumber;
    }

    public void spawnRandomPlatform()
    {
        for (int i = 0; i < platformsSpawnCap.Length; i++)
        {
            if (platformsSpawnCap[i] != null)
            {
                Destroy((GameObject)platformsSpawnCap[i]);
            }
        }


        for (int i=0; i < platformsSpawnCap.Length; i++)
        {
            var rndNumber = selectUniqueRandomNumber(platformListArr.Length);
            //Debug.Log($"Spawn Random Number is: {rndNumber}");
            platformToSpawn = platformListArr[rndNumber];

            newPlatform = Instantiate(platformToSpawn) as GameObject;
            lastSpawnedPlatform = newPlatform;

            newPlatform.transform.position = new Vector3(0, tr.position.y + spawnDistance, 0);
            newPlatform.tag = "Platform";
            newPlatform.transform.localScale = newPlatform.transform.localScale * Sprite_AutoRes.instance.scaleRatio;
            spawnDistance += 10;


            // PLATFORM SPRITE CHANGE
            //if(newPlatform.transform.childCount > 0)
            //{
            //    for(int j=0; j< newPlatform.transform.childCount; j++)
            //    {
            //        if(newPlatform.transform.GetChild(j).tag == "Platform")
            //        {
            //            newPlatform.transform.GetChild(j).GetComponent<SpriteRenderer>().sprite = platformSprites;
            //        }
            //    }
            //}
            //else
            //{
            //        if(newPlatform.transform.tag == "Platform")
            //        {
            //          newPlatform.transform.GetComponent<SpriteRenderer>().sprite = platformSprites;
            //      }
            //}


            platformsSpawnCap[i] = newPlatform;


        }

    }
}
