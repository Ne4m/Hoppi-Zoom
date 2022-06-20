using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{


    [Header ("Bullet Variables")]
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletDamage;


    private Transform tr;

    private Rigidbody2D rb;

    private Vector3 startPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();

        startPosition = tr.position;
    }

    private void Update()
    {
        if(tr.position.y > startPosition.y + 10)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        Vector3 vectorUp = tr.TransformDirection(Vector2.up);

        rb.velocity = (vectorUp * bulletSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            Debug.Log($"Hit platform : {collision.gameObject.name}");

            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

}
