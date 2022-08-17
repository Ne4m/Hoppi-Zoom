
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{


    [Header ("Bullet Variables")]
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletDamage;

    [Header("Particle Effect")]
    [SerializeField] private GameObject particleFX;


    private Transform tr;
    private Rigidbody2D rb;

    private bool canDestroy;

    private Vector3 startPosition;


    public float BulletSpeed
    {
        get => bulletSpeed;
        set
        {
            bulletSpeed = value;
            Debug.Log($"Bullet Speed is set to {bulletSpeed}");
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();


        BulletSpeed = Perks.instance.BulletSpeed;

        startPosition = tr.position;
    }

    private void Update()
    {
        if(Vector2.Distance(tr.position, startPosition) > 10) // tr.position.y > startPosition.y + 10
        {
            
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        Vector3 vectorUp = tr.TransformDirection(Vector2.up);

        rb.velocity = (vectorUp * bulletSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform") || collision.CompareTag("Wood"))
        {
            //Debug.Log($"Hit platform : {collision.gameObject.name}");

            
            if(collision.GetComponent<PlatformMovements>() != null)
            {
                canDestroy = collision.GetComponent<PlatformMovements>().GetIsDestructable();
            }
            else
            {
                canDestroy = true;
            }




            if (!canDestroy)
            {
                Destroy(gameObject);
                AudioManager.instance.Play("Invalid");
                return;
            }

            Instantiate(particleFX, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(gameObject);

            AudioManager.instance.Play("Destruction");

            if (Perks.instance.ChanceToHeal)
            {
                var rnd = Random.Range(0, 100);

                if(rnd < 25)
                {
                    var perc = Perks.instance.HealPercentage;
                    FindObjectOfType<PlayerConditions>().HealthbarShow = true;
                    LevelManager.instance.playerControl.percentageHeal(perc);
                }


            }

        }
        else if (collision.CompareTag("Chain"))
        {
            Chain.CutMe(collision.gameObject);
            Destroy(gameObject);
        }


    }



}
