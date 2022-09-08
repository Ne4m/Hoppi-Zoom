using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GiftSpawner : MonoBehaviour
{
    public GameObject pointer;
    public TMP_Text debugtext;
    public GameObject spawnedGift;
    private float lastUpdateCheck;


    private float randomNumber;
    private float lastNumber;
    private int maxAttempts = 5;

    public static GiftSpawner instance;

    private Vector3 spawnPos;
    private GameObject spawnedObject;

    private int giftSpawnThreshold = 2;

    public int GiftSpawnThreshold
    {
        get
        {
            giftSpawnThreshold = UnityEngine.Random.Range(5, 25);
            return giftSpawnThreshold;
        }
        private set { }
    }

    [SerializeField] private Camera mainCam;

    public enum Pickables
    {
        AMMO,
        HEALTH,
        GOLD
    }

    private Pickables chosenPickable;

    public Pickables ChosenPickable
    {
        get => chosenPickable;
        set => chosenPickable = value;
    }

    private void Awake()
    {

       instance = this;

    }

    void Start()
    {

        //debugtext.gameObject.SetActive(true);
    }

    void Update()
    {

        //WaitForSeconds(0.25f, () =>
        //{

        //    SpawnObject();

        //});

    }

    private void SpawnObject()
    {
        var number = UnityEngine.Random.Range(0, 3);

        switch (number)
        {
            case 0:
                ChosenPickable = Pickables.AMMO;
                break;
            case 1:
                ChosenPickable = Pickables.HEALTH;
                break;
            case 2:
                ChosenPickable = Pickables.GOLD;
                break;
        }

        // Debug.Log($"Chosen Pickable: {ChosenPickable} Number: {number}");


        spawnedObject = Instantiate(spawnedGift, SetSpawnPosition(), Quaternion.identity);
        spawnedObject.tag = "Pickable";
    }

    private Vector3 SetSpawnPosition()
    {
        float height = 2f * Camera.main.orthographicSize;
        float width = height * Camera.main.aspect;

        var _screen = Camera.main.ViewportToWorldPoint(new Vector2(UnityEngine.Random.value, UnityEngine.Random.value)); // new Vector2(Screen.width, Screen.height)
        //var _spawnX = selectUniqueRandomNumber(_screen.x * -1, _screen.x);
        //var _spawnY = selectUniqueRandomNumber(_screen.y * -1, _screen.y);
        var spawnPoint = new Vector3(_screen.x, _screen.y, 0f);

        return spawnPoint;

    }

    public void Spawn()
    {
        SpawnObject();
    }


    public float selectUniqueRandomNumber(float upperLimit, float floorLimit)
    {

        for (int i = 0; randomNumber == lastNumber && i < maxAttempts; i++)
        {
            randomNumber = UnityEngine.Random.Range(floorLimit, upperLimit);
        }
        lastNumber = randomNumber;

        //if(randomNumber > 10) Debug.LogWarning($"Generated Number {randomNumber} \n");

        return randomNumber;
    }

    private void WaitForSeconds(float interval, Action action)
    {

        if (Time.time > interval + lastUpdateCheck)
        {

            action();

            lastUpdateCheck = Time.time;
        }
    }
}
