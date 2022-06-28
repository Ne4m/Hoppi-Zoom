using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgress : MonoBehaviour
{
    [Header ("Backgrounds")]
    [SerializeField] private GameObject background1;
    [SerializeField] private GameObject background2;
    [SerializeField] private Sprite[] spritesToChange;

    LevelManager levelManager;

    float lastPoint = 0;
    void Start()
    {
        levelManager = GetComponent<LevelManager>();
    }

    int randomNumber;
    int lastNumber;
    int maxAttempts = 10;
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
        if(levelManager.playerControl.getPoint() > lastPoint + 10)
        {
            int number = selectUniqueRandomNumber(spritesToChange.Length);
            background1.GetComponent<SpriteRenderer>().sprite = spritesToChange[number];
            background2.GetComponent<SpriteRenderer>().sprite = spritesToChange[number];

            lastPoint = levelManager.playerControl.getPoint();
        }
    }
}
