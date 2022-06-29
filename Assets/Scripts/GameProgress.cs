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

    [Header ("Speed Threshold Values")]
    [SerializeField] float speedChangeThreshold = 2;
    [SerializeField] float backgroundChangeThreshold = 5;

    private float speedIncrease;

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
            int number = selectUniqueRandomNumber(spritesToChange.Length);
            background1.GetComponent<SpriteRenderer>().sprite = spritesToChange[number];
            background2.GetComponent<SpriteRenderer>().sprite = spritesToChange[number];

            last.BgPoint = levelManager.playerControl.getPoint();

            messager.startMsg($"Background changed!", 2, Vector3.zero);
        }

        if (levelManager.playerControl.getPoint() >= last.SpeedPoint + speedChangeThreshold)
        {
            last.SpeedPoint = levelManager.playerControl.getPoint();

            speedIncrease += Random.Range(0.01f, 0.1f);

            messager.startMsg($"Speed Increased {speedIncrease}!", 2, Vector3.zero);
        }
    }

    public float GetSpeedIncrease()
    {
        return speedIncrease;
    }
}
