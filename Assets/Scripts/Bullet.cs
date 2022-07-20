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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();


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
            Debug.Log($"Hit platform : {collision.gameObject.name}");

            
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
                return;
            }

            Instantiate(particleFX, transform.position, Quaternion.identity);

            Destroy(collision.gameObject);
            Destroy(gameObject);


        }
        else if (collision.CompareTag("Chain"))
        {
            Chain.CutMe(collision.gameObject);
            Destroy(gameObject);
        }


    }

}
