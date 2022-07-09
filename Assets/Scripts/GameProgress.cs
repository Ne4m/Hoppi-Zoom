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
    float backgroundChangeThreshold = 10;

    private float speedIncrease;
    private float rotateSpeedIncrease;
    private int bgChangeCounter = 0;
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
        Clouds = 5,
        Stars = 6,
        Planets = 10
    }

    enum BackGroundThresholds
    {
        BG_1,

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
            randomNumber = Random.Range(0, limit);
        }
        lastNumber = randomNumber;

        //if(randomNumber > 10) Debug.LogWarning($"Generated Number {randomNumber} \n");

        return randomNumber;
    }


    // Update is called once per frame
    void Update()
    {
        int point = levelManager.playerControl.getPoint();



        if (point >= last.BgPoint + backgroundChangeThreshold)
        {
            bgChangeCounter++;
            if (bgChangeCounter > spritesToChange.Length-1) return;


            background1.GetComponent<SpriteRenderer>().sprite = spritesToChange[bgChangeCounter];
            background2.GetComponent<SpriteRenderer>().sprite = spritesToChange[bgChangeCounter];



            last.BgPoint = point;

            //messager.startMsg($"Area Level Changed!", 2, Vector3.zero);
        }

        if (point >= last.SpeedPoint + speedChangeThreshold)
        {

            // PLATFORMS SPEED MANIPULATION BASED ON EARNED POINTS
            last.SpeedPoint = point;

            speedIncrease += Random.Range(0.01f, 0.1f);
            rotateSpeedIncrease += Random.Range(0.1f, 1f);

            //messager.startMsg($"Area Speed Increased!", 2, Vector3.zero);
        }


        switch (point)
        {
            case var _ when point >= 0 && point < (int)SpriteNames.Clouds:
                loaded.Spawner = SpawnerNames.Cloudsets.ToString();
                loaded.Sprite = SpriteNames.Clouds.ToString();
                break;
            case var _ when bgChangeCounter == spritesToChange.Length-1: // point >= (int)SpriteNames.Clouds &&  point <= (int)SpriteNames.Planets
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
