using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlatformSpawns_Test : MonoBehaviour
{
    [SerializeField] private GameObject[] platforms;
    [SerializeField] private Button incrementBtn, decrementBtn, applyBtn;
    [SerializeField] private TMP_Text indexNumberTxt;
    [SerializeField] private TMP_InputField platformNameTxt, setIndexNumberTxt;
    private int indexNumber = 0;
    private GameObject spawnedPlatform, lastSpawnedPlatform;

    void Start()
    {
        if (incrementBtn is not null) incrementBtn.onClick.AddListener(incrementBtn_Clicked);
        if (decrementBtn is not null) decrementBtn.onClick.AddListener(decrementBtn_Clicked);
        if (applyBtn is not null) applyBtn.onClick.AddListener(applyBtn_Clicked);

        platforms = Resources.LoadAll<GameObject>("Platforms");
        SpawnPlatform(indexNumber);
    }

    private void Update()
    {


        indexNumberTxt.text = indexNumber.ToString();

        if (spawnedPlatform != null)
        {
            platformNameTxt.text = spawnedPlatform.name;
            var indexText = platformNameTxt.text;
            indexText = indexText.Remove(indexText.Length - 7);
            platformNameTxt.text = indexText;
        }
    }

    private void applyBtn_Clicked()
    {
        var tmpIndex = Convert.ToInt32(setIndexNumberTxt.text);
        if(tmpIndex >= 0 &&  tmpIndex < platforms.Length)
        {
            SpawnPlatform(tmpIndex);
        }

    }

    void incrementBtn_Clicked()
    {
        if (indexNumber < platforms.Length-1)
        {
            indexNumber++;
            SpawnPlatform(indexNumber);
        }
    }

    void decrementBtn_Clicked()
    {
        if(indexNumber > 0)
        {
            indexNumber--;
            SpawnPlatform(indexNumber);
        }
    }

    void SpawnPlatform(int index)
    {
        if(lastSpawnedPlatform != null) Destroy(lastSpawnedPlatform);

        var spawn = platforms[index];
        spawnedPlatform = Instantiate(spawn);
        spawnedPlatform.transform.position = new Vector3(0f, 0f, 0f);
        spawnedPlatform.tag = "Platform";


        lastSpawnedPlatform = spawnedPlatform;
    }
}
