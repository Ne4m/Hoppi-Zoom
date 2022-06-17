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

    GameObject platformToSpawn;
    GameObject newPlatform;
    private GameObject[] platformsSpawnCap = new GameObject[5];

    private int initiatedCount = 0;


    void Start()
    {
        tr = GetComponent<Transform>();

        platformListArr = Resources.LoadAll<GameObject>("Platforms");
        platformList = platformListArr.ToList();

        for(int i=0; i < platformListArr.Length; i++)
        {
            Debug.Log($"Loaded platform No {i+1}: {platformListArr[i].name}\n");
        }

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

        Debug.Log($"Initiated count {initiatedCount}\n");
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
            platformToSpawn = platformList[Random.Range(0, platformList.Count)];

            newPlatform = Instantiate(platformToSpawn) as GameObject;

            newPlatform.transform.position = new Vector3(0, tr.position.y + spawnDistance, 0);
            newPlatform.tag = "Platform";
            spawnDistance += 10;


            platformsSpawnCap[i] = newPlatform;
        }

    }
}
