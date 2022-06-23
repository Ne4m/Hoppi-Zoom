using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerConditions : MonoBehaviour
{

    LevelManager levelManager;
    Rigidbody2D rb;
    Transform tr;
    SpriteRenderer sr;

    [Header("Values")]
    [SerializeField] private float gracePeriod;
    [SerializeField] private float graceSpeed;


    private bool graceEnabled = false;
    private bool routineStarted = false;

    Color srColor;

    void Start()
    {
        levelManager = this.GetComponent<LevelManager>();
        tr = this.GetComponent<Transform>();
        rb = tr.GetComponent<Rigidbody2D>();
        sr = tr.GetComponent<SpriteRenderer>();


    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Contains("Spike"))
        {
            Debug.Log("Got Hit By A Spike!! \n");
            blinkCharacter();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spike"))
        {
            Debug.Log("Got Hit By A Spike!! \n");
            blinkCharacter();
        }
    }



    private void Update()
    {
        if(!routineStarted && graceEnabled)
        {
            StartCoroutine(gracePeriodTask());
            StartCoroutine(gracePeriodCountdown());
        }
    }

    public void blinkCharacter()
    {
        if (!graceEnabled)
        {
            levelManager.playerControl.damage(100);
            graceEnabled = !graceEnabled;
            Debug.Log(graceEnabled ? "Grace Enabled" : "Grace Disabled");
            return;
        }

        Debug.LogWarning("Already on grace period!");

    }


    private IEnumerator gracePeriodCountdown()
    {
        while(graceEnabled)
        {
            routineStarted = true;
            yield return new WaitForSeconds(gracePeriod);
            graceEnabled = false;
            routineStarted = false;
        }

    }

    private IEnumerator gracePeriodTask()
    {
        while (graceEnabled)
        {
            srColor = new Color(186, 186, 186);
            sr.color = srColor;

            sr.enabled = false;

            yield return new WaitForSeconds(graceSpeed);


            srColor = new Color(255, 255, 255);
            sr.color = srColor;

            sr.enabled = true;

            yield return new WaitForSeconds(graceSpeed);

        }


    }
}
