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

    // Start is called before the first frame update
    void Start()
    {

        debugtext.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        var _screen2 = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));



        var _screen = pointer.transform.position;
        debugtext.text = ($"{_screen.x}, {_screen.y}\n" +
                          $"{_screen2.x}, {_screen2.y}");


        WaitForSeconds(3f, () =>
        {
            var _spawnX = selectUniqueRandomNumber(_screen2.x * -1, _screen2.x);
            var _spawnY = selectUniqueRandomNumber(_screen2.y * -1, _screen2.y);

            Debug.Log("Spawning new gift..");
            spawnedGift.gameObject.transform.position = new Vector3(_spawnX, _spawnY, 0f);
            Debug.Log($"Spawned new gift! in pos : {_spawnX}, {_spawnY}");

        });

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
