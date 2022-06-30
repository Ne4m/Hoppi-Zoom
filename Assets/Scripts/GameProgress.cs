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

    [Header ("Speed Threshold Values")]
    [SerializeField] float speedChangeThreshold = 10;
    [SerializeField] float backgroundChangeThreshold = 1;

    private float speedIncrease;
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
        if(levelManager.playerControl.getPoint() >= last.BgPoint + backgroundChangeThreshold)
        {
            bgChangeCounter++;
            if (bgChangeCounter > spritesToChange.Length) return;

 
            background1.GetComponent<SpriteRenderer>().sprite = spritesToChange[bgChangeCounter];
            background2.GetComponent<SpriteRenderer>().sprite = spritesToChange[bgChangeCounter];



            last.BgPoint = levelManager.playerControl.getPoint();

            messager.startMsg($"Background changed!", 2, Vector3.zero);
        }

        if (levelManager.playerControl.getPoint() >= last.SpeedPoint + speedChangeThreshold)
        {
            last.SpeedPoint = levelManager.playerControl.getPoint();

            speedIncrease += Random.Range(0.01f, 0.1f);

            messager.startMsg($"Speed Increased {speedIncrease}!", 2, Vector3.zero);
        }

        int point = levelManager.playerControl.getPoint();
        switch (point)
        {
            case var _ when point <= (int)SpriteNames.Clouds:
                loaded.Spawner = SpawnerNames.Cloudsets.ToString();
                loaded.Sprite = SpriteNames.Clouds.ToString();
                break;
            case var _ when point <= (int)SpriteNames.Planets:
                loaded.Spawner = SpawnerNames.Cloudsets.ToString();
                loaded.Sprite = SpriteNames.Planets.ToString();
                break;

        }
    }

    public float GetSpeedIncrease()
    {
        return speedIncrease;
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
