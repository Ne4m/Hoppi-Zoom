using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu_Animations : MonoBehaviour
{
    [SerializeField] private GameObject eyeLeft;
    [SerializeField] private GameObject eyeTopLeft;
    [SerializeField] private GameObject eyeRight;
    [SerializeField] private GameObject allEyes;

    [SerializeField] private float rotateSpeed = 25f;
    [SerializeField] private float rotateAngle = 120f;


    void Start()
    {
        InvokeRepeating("RotateEyes", 3f, 1f);
    }

    void RotateEyes()
    {


        GameObject randomObject;


        var number = UnityEngine.Random.Range(0, 3);

        if(number == 0)
        {
            randomObject = eyeLeft;
        }
        else if(number == 1)
        {
            randomObject = eyeTopLeft;
        }
        else if(number == 2)
        {
            randomObject = eyeRight;
        }
        else
        {
            randomObject = allEyes;
        }


       // Debug.Log("New leaning is about to start");

        if (!LeanTween.isTweening(randomObject))
        {
            LeanTween.rotate(randomObject, new Vector3(0, 0, rotateAngle), 0f)
            .setLoopType(LeanTweenType.pingPong)
            .setSpeed(rotateSpeed);
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
