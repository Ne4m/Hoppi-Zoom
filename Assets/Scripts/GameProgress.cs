using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameProgress : MonoBehaviour
{
    [Header ("Backgrounds")]
    [SerializeField] private GameObject background1;
    [SerializeField] private GameObject background2;
    [SerializeField] private Sprite[] spritesToChange;

    public LevelManager levelManager;
    UIMessager messager;

    public static GameProgress instance;

    (float BgPoint, float SpeedPoint) last;
    (string Sprite, string Spawner) loaded;


    float speedChangeThreshold = 5;

    private float speedIncrease;
    private float rotateSpeedIncrease;

    //string loadedSprite = string.Empty;
    //string loadedSpawner = string.Empty;

    enum SpawnerNames
    {
        Cloudsets,
        Starsets,
        Planetsets

    }
    enum SpriteNames
    {
        Clouds = 4,
        Stars = 123,
        Planets = 10
    }

    enum BackGroundThresholds
    {


        //p2 = 25,
        //p3 = 50,
        //p4 = 75,
        //p5 = 100,
        //p6 = 125,
        //p7 = 150,
        //p8 = 250,
        //p9 = 500,
        //p10 = 1000,
        //p11 = 1500,
        //p12 = 2000


        p2 = 2,
        p3 = 3,
        p4 = 4,
        p5 = 5,
        p6 = 6,
        p7 = 7,
        p8 = 8,
        p9 = 9,
        p10 = 10,
        p11 = 11,
        p12 = 12

    }

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        levelManager = GetComponent<LevelManager>();
        messager = GetComponent<UIMessager>();


    }

    int randomNumber;
    int lastNumber;
    int maxAttempts = 5;
    public int selectUniqueRandomNumber(int limit)
    {

        for (int i = 0; randomNumber == lastNumber && i < maxAttempts; i++)
        {
            randomNumber = UnityEngine.Random.Range(0, limit);
        }
        lastNumber = randomNumber;

        //if(randomNumber > 10) Debug.LogWarning($"Generated Number {randomNumber} \n");

        return randomNumber;
    }


    // Update is called once per frame
    void Update()
    {
        int point = levelManager.playerControl.getPoint();


        // MANIPULATING BACKGROUNDS BASED ON BG ENUM THRESHOLDS
        foreach (var bg in Enum.GetValues(typeof(BackGroundThresholds)))
        {

            if (point == (int)bg)
            {
                var number = Convert.ToInt32(bg.ToString().Substring(1));
                background1.GetComponent<SpriteRenderer>().sprite = spritesToChange[number - 2];
                background2.GetComponent<SpriteRenderer>().sprite = spritesToChange[number - 2];

            }
        }



        if (point >= last.SpeedPoint + speedChangeThreshold)
        {

            // PLATFORMS SPEED MANIPULATION BASED ON EARNED POINTS
            last.SpeedPoint = point;

            speedIncrease += UnityEngine.Random.Range(0.01f, 0.1f);
            rotateSpeedIncrease += UnityEngine.Random.Range(0.1f, 1f);

            //messager.startMsg($"Area Speed Increased!", 2, Vector3.zero);
        }


        switch (point)
        {
            case var _ when point >= 0 && point < (int)SpriteNames.Clouds:
                loaded.Spawner = SpawnerNames.Cloudsets.ToString();
                loaded.Sprite = SpriteNames.Clouds.ToString();
                break;
            case var _ when point >= (int)SpriteNames.Planets: //bgChangeCounter == spritesToChange.Length-1: // point >= (int)SpriteNames.Clouds &&  point <= (int)SpriteNames.Planets
                loaded.Spawner = SpawnerNames.Cloudsets.ToString();
                loaded.Sprite = SpriteNames.Planets.ToString();
                break;

        }
    }

    public float GetSpeedIncrease()
    {
        return speedIncrease;
    }

    public float GetRotateSpeedIncrease()
    {
        return rotateSpeedIncrease;
    }

    public string GetSpawnSets()
    {

        return loaded.Spawner;
    }

    public string GetSkySprites()
    {
        return loaded.Sprite;
    }
}
