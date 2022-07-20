using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlayerConditions : MonoBehaviour
{

    LevelManager levelManager;
    Rigidbody2D rb;
    Transform tr;
    SpriteRenderer sr;

    [SerializeField] private GameObject playerSkins;
    [SerializeField] private GameObject healthBarContainer;
    [SerializeField] private GameObject healthBarGreenFill;
    [SerializeField] private float damageToBarTime;
    private Image healthBar;

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

        healthBar = healthBarGreenFill.GetComponent<Image>();

        healthBarContainer.gameObject.SetActive(false);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Contains("Spike"))
        {
            Debug.Log("Got Hit By A Spike!! [COLLISION] \n" + transform.tag);
            DamageCharacter();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spike"))
        {
            Debug.Log("Got Hit By A Spike!! [TRIGGER]\n" + transform.tag);
            DamageCharacter();
        }

        if (collision.CompareTag("Laser"))
        {
            Debug.Log("Got Hit By A Laser! [TRIGGER]\n" + transform.tag);
            DamageCharacter();
        }

        if (collision.CompareTag("Wood"))
        {
            Debug.Log("Got Hit By A Wood [TRIGGER]\n" + transform.tag);
            DamageCharacter();
        }
    }



    private void Update()
    {
        if(!routineStarted && graceEnabled)
        {
            StartCoroutine(gracePeriodTask());
            StartCoroutine(gracePeriodCountdown());
        }

        var currentHealthRatio = (levelManager.playerControl.getHealth() / levelManager.playerControl.getMaxHealth());
        healthBar.fillAmount = Mathf.MoveTowards(healthBar.fillAmount, currentHealthRatio, damageToBarTime * Time.deltaTime);
        if (levelManager.playerControl.isPlayerDead()) healthBar.fillAmount = 0f;

        var canvasFollow = new Vector3(transform.position.x, transform.position.y + 0.7879446f, transform.position.z);
        var canvElemCanvasPosition = GetCanvasPositionOfWorld(canvasFollow);
        healthBarContainer.GetComponent<RectTransform>().position = canvElemCanvasPosition;
    }

    //private void OnGUI()
    //{
    //    if (healthBarContainer.gameObject.activeSelf)
    //    {
    //    }


    //}

    public Vector3 GetCanvasPositionOfWorld(Vector3 coords)
    {
        var result = RectTransformUtility.WorldToScreenPoint(Camera.main, coords);

        return result;
    }

    public void DamageCharacter()
    {
        if (!graceEnabled)
        {
            healthBarContainer.gameObject.SetActive(true);

            levelManager.playerControl.damage(100);
            graceEnabled = !graceEnabled;
            //Debug.Log(graceEnabled ? "Grace Enabled" : "Grace Disabled");
            return;
        }

        //Debug.LogWarning("Already on grace period!");

    }


    private IEnumerator gracePeriodCountdown()
    {
        while(graceEnabled)
        {
            routineStarted = true;
            yield return new WaitForSeconds(gracePeriod);

            healthBarContainer.gameObject.SetActive(false);
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

            for(int i=0; i< playerSkins.transform.childCount; i++)
            {
                playerSkins.transform.GetChild(i).GetComponent<SpriteRenderer>().color = srColor;
                playerSkins.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = false;
            }

            yield return new WaitForSeconds(graceSpeed);


            srColor = new Color(255, 255, 255);
            sr.color = srColor;

            sr.enabled = true;

            for (int i = 0; i < playerSkins.transform.childCount; i++)
            {
                playerSkins.transform.GetChild(i).GetComponent<SpriteRenderer>().color = srColor;
                playerSkins.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
            }

            yield return new WaitForSeconds(graceSpeed);

        }


    }
}
