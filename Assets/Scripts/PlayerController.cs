using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 10;

    private bool isInWaypoint;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isInWaypoint = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isInWaypoint)
        Movement();
    }

    void Movement()
	{
        float h = Input.GetAxis("Horizontal")*speed*Time.fixedDeltaTime;
        float v = Input.GetAxis("Vertical")*speed*Time.fixedDeltaTime;

        rb.AddForce(new Vector2(h, v));
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        isInWaypoint = true;
	}

}
