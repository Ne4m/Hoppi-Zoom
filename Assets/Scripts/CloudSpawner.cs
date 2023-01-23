using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{

    public static GameObject lastSpawned;
    public static GameObject newSpawned;

    public GameObject[] spawnerResource;
    public Sprite[] spritesArray;

    [SerializeField] private bool isSpawner;

    int randomNumber;
    int lastNumber;
    int maxAttempts = 15;

    GameProgress gameProgress;
    SpriteRenderer sr;



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


    private void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();

        gameProgress = GameProgress.instance;



        //loadedSpawner = gameProgress.GetSpawnSets();
        //loadedSprite = gameProgress.GetSkySprites();

        //int point = levelManager.playerControl.getPoint();



        if (isSpawner)
        {
            if(gameProgress.GetSpawnSets() != null)
            {
                spawnerResource = Resources.LoadAll<GameObject>($"ObjectSpawning/Sets/{gameProgress.GetSpawnSets()}");
                SpawnSpawner();
            }

        }
        else
        {
            if(gameProgress.GetSkySprites() != null)
            {
                spritesArray = Resources.LoadAll<Sprite>($"ObjectSpawning/Sprites/{gameProgress.GetSkySprites()}");

                //Debug.Log($"Loaded Sprite : {loadedSprite} and Point is {point}");
                SpawnSprites();
            }

        }



    }



    private void SpawnSpawner()
    {
        int uniqueNumber = selectUniqueRandomNumber(spawnerResource.Length);

        if(lastSpawned != null)
        {
            Destroy((GameObject)lastSpawned);
        }
        newSpawned = Instantiate(spawnerResource[uniqueNumber], transform.position, Quaternion.identity);
        lastSpawned = newSpawned;

    }

    private void SpawnSprites()
    {
        int uniqueNumber = selectUniqueRandomNumber(spritesArray.Length);
        sr.sprite = spritesArray[uniqueNumber];

    }

}
