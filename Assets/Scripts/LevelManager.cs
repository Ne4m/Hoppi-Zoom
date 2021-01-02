using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform player;
    public Rigidbody2D rb2D;
    private GameObject checkPoint_Prefab;
    private GameObject new_CheckPoint;
    private GameObject last_CheckPoint;

    private bool isINCP;
    private bool collided = false;

    [SerializeField]
    private int point = 0;

    private GameObject[] spawnedObjects = new GameObject[2];

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Transform>();
        rb2D = player.GetComponent<Rigidbody2D>();
        checkPoint_Prefab = Resources.Load("Checkpoint_Bar") as GameObject;
    }

    private void Awake()
    {
        isINCP = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playerUpdate(bool CP_Status)
    {
        isINCP = CP_Status;
        Debug.Log("Check Point Status: " + isINCP);
    }

    public bool playerCheck()
    {
        return isINCP;
    }

    public void playerCollided()
    {
        point++;


        

        new_CheckPoint = Instantiate(checkPoint_Prefab) as GameObject;
        new_CheckPoint.transform.position = transform.position;
        new_CheckPoint.transform.position = new Vector3(0, new_CheckPoint.transform.position.y + 5.25f, 0);



             if (spawnedObjects[0] == null)
        {
            spawnedObjects[0] = new_CheckPoint;
        }
        else if (spawnedObjects[1] == null)
        {
            spawnedObjects[1] = new_CheckPoint;
        }
        else if (spawnedObjects[0] != null)
        {
            Destroy(spawnedObjects[0]);

            spawnedObjects[1] = spawnedObjects[0];
            spawnedObjects[1] = null;
        }

    }
}
