using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public List<GameObject> platformList;
    public GameObject[] platformListArr;

    private Transform tr;

    [SerializeField]
    private float spawnDistance = 5f;

    private GameObject platformToSpawn;
    private GameObject newPlatform;
    private GameObject[] platformsSpawnCap = new GameObject[5];

    private int initiatedCount = 0;    
    public GameObject lastSpawnedPlatform;


    void Start()
    {
        tr = GetComponent<Transform>();

        platformListArr = Resources.LoadAll<GameObject>("Platforms");
        platformList = platformListArr.ToList();

        //for(int i=0; i < platformListArr.Length; i++)
        //{
        //    Debug.Log($"Loaded platform No {i+1}: {platformListArr[i].name}\n");
        //}

        //Debug.Log($"Total Platform Count {platformList.Count}");
       // StartCoroutine(ExampleCoroutine());
    }


    void Update()
    {
        
    }

    IEnumerator ExampleCoroutine()
    {
        while (true)
        {
            spawnRandomPlatform();
            yield return new WaitForSeconds(5);
        }

    }

    public void initiateSpawn()
    {
       
        if(initiatedCount != 4)
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
           
            platformToSpawn = platformList[selectUniqueRandomNumber(platformList.Count)];

            newPlatform = Instantiate(platformToSpawn) as GameObject;
            lastSpawnedPlatform = newPlatform;

            newPlatform.transform.position = new Vector3(0, tr.position.y + spawnDistance, 0);
            newPlatform.tag = "Platform";
            spawnDistance += 10;


            platformsSpawnCap[i] = newPlatform;


        }

    }
}
