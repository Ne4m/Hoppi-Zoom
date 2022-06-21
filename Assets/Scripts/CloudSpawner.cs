using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{

    public static GameObject lastSpawned;
    public static GameObject newSpawned;

    public GameObject[] cloudResource;
    public Sprite[] cloudSpritesArray;

    [SerializeField] private bool isSpawner;

    int randomNumber;
    int lastNumber;
    int maxAttempts = 10;

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


        if (isSpawner)
        {
            cloudResource = Resources.LoadAll<GameObject>("Cloudsets");
            SpawnCloudsets();
        }
        else
        {
            cloudSpritesArray = Resources.LoadAll<Sprite>("Cloud_Sprites");
            SpawnClouds();
        }




    }



    private void SpawnCloudsets()
    {
        int uniqueNumber = selectUniqueRandomNumber(cloudResource.Length);

        if(lastSpawned != null)
        {
            Destroy((GameObject)lastSpawned);
        }
        newSpawned = Instantiate(cloudResource[uniqueNumber], transform.position, Quaternion.identity);
        lastSpawned = newSpawned;

    }

    private void SpawnClouds()
    {
        int uniqueNumber = selectUniqueRandomNumber(cloudSpritesArray.Length);
        sr.sprite = cloudSpritesArray[uniqueNumber];

    }

}
