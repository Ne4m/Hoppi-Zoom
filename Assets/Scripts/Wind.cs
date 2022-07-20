using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    private AreaEffector2D _effector;
    [SerializeField] private ParticleSystem windParticle;
    [SerializeField] private float minForce = -1f, maxForce = 1f;


    [SerializeField] private float windSpeed;
    void Start()
    {
        _effector = GetComponent<AreaEffector2D>();

        InvokeRepeating("RandomWind", 2f, 1f);
    }

    [System.Obsolete]
    void RandomWind()
    {
        //windspeed = random.range(-5f, 100f);

        windSpeed = Random.Range(minForce, maxForce);

        //windParticle.startSpeed = windSpeed;

        _effector.forceMagnitude = windSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
