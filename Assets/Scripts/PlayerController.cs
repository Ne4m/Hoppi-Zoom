﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Transform backgrounds;
    int count;


    Rigidbody2D rb2D;


    public float rotateSpeed = 100;


    public bool atMid = true;
    public bool goingLeft = true;
    public bool goingRight = false;
    public bool isPlayerDead;

    public float desiredAngle;

    [SerializeField]
    private Transform tr;

    [SerializeField]
    private Transform ejector;

    [SerializeField]
    private GameObject arrowiks;

    Vector3 kekVec = new Vector3(0, 0.46f, 0);
    LevelManager LM;

    #region Speed Variables
    [SerializeField]
    private float ejectForce = 1;
    [SerializeField]
    private float playerRotSpeed=10;
	#endregion

	private void Awake()
	{
        rb2D = GetComponent<Rigidbody2D>();

        backgrounds = GetComponent<Transform>();
        count = backgrounds.childCount;
    }

	void Start()
    {
        //LM = gameObject.AddComponent(typeof(LevelManager)) as LevelManager;
        LM = gameObject.GetComponent<LevelManager>();
        AdjustStartingPoint();
        //LM.playerUpdate(true);
        LM.playerControl.setPlayerCheckPoint(true);
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("Z: " + transform.rotation.z +"\n");

        if (tr.rotation.z == -0.0008726012) atMid = true;
        

        if (Input.GetKey(KeyCode.R) || Input.touchCount == 2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        isPlayerDead = LM.playerControl.isPlayerDead();
    }

    private void FixedUpdate()
    {
        MoveArrow();
        GetCast();
        GetRotAccordingtoVelocity();
    }

    Quaternion slowRot;
    void GetCast()
    {

        Vector3 upVector = tr.TransformDirection(Vector2.up) * 10;
        Debug.DrawRay(tr.position, upVector, Color.green);


        if ((Input.GetKey(KeyCode.A) || Input.touchCount == 1) && LM.playerControl.IsPlayerInCheckPoint() && !isPlayerDead) // isInWaypoint LM.playerCheck()
        {
            LM.playerControl.setPlayerCheckPoint(false);
            arrowiks.SetActive(false);


            upVector = upVector - kekVec;
            rb2D.AddForce(upVector * ejectForce * Time.fixedDeltaTime);

            slowRot = Quaternion.Slerp(transform.rotation, tr.rotation, Time.fixedDeltaTime * playerRotSpeed);
            rb2D.MoveRotation(slowRot);

           //rb2D.constraints = RigidbodyConstraints2D.None;
        }

    }

    void MoveArrow()
    {
        if (goingLeft && !goingRight)
        {

            if (tr.rotation.z >= 0.4992441)
            {
                goingLeft = !goingLeft;
                goingRight = !goingRight;
            }
            else tr.Rotate(0, 0, rotateSpeed * Time.deltaTime); ;

        }

        if (goingRight && !goingLeft)
        {

            if (tr.rotation.z <= -0.5007555)
            {
                goingRight = !goingRight;
                goingLeft = !goingLeft;
            }
            else tr.Rotate(0, 0, rotateSpeed * Time.deltaTime * -1);
        }

        //Debug.Log("Going Left: " + goingLeft + " Going Right " + goingRight);

    }


    void AdjustStartingPoint()
    {
        ejector.transform.position = new Vector3(rb2D.transform.position.x, rb2D.transform.position.y + 0.19f, 0);
        transform.rotation = new Quaternion(0,0,0,0);
    }

    Collider2D collider;
    private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.tag == "Checkpoint")
        {
            
            arrowiks.SetActive(true);
            //rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
            AdjustStartingPoint();

            //rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
            rb2D.velocity = Vector2.zero;
            rb2D.angularVelocity = 0f;
            //rb2D.constraints = RigidbodyConstraints2D.FreezePositionX;
            //rb2D.constraints = RigidbodyConstraints2D.FreezePositionY;
            Vector3 kek = new Vector3(rb2D.transform.position.x, collision.transform.position.y, 0);
            rb2D.transform.SetPositionAndRotation(kek, transform.rotation);
            //rb2D.constraints = RigidbodyConstraints2D.None;


            collider = collision.GetComponent<Collider2D>();
            collider.enabled = false;

            LM.playerControl.setPlayerCheckPoint(true);
            LM.player_EnteredCheckpoint();

        }


        if (collision.tag == "Background")
        {
            for (int i = 0; i < count; i++)
            {
                Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), backgrounds.GetChild(i).GetComponent<Collider2D>());
            }
        }
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
        //vector3 reflect = vector3.reflect(vector3.right, collision.contacts[0].normal);
        //float rot = 90 - mathf.atan2(reflect.z, reflect.x) * mathf.rad2deg;
        //transform.eulerangles = new vector3(0, 0, rot);

        //Debug.Log("Collision: " + collision.gameObject.tag);

        //if (collision.collider.tag.Contains("Untagged"))

        //{
        //    if (collider.enabled == false)
        //    {
        //        collider.enabled = true;
        //    }

        //}

    }

    void GetRotAccordingtoVelocity()
	{
		if (!LM.playerControl.IsPlayerInCheckPoint())
        {
            Vector2 velocity = rb2D.velocity;
            desiredAngle = (Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg);
            Quaternion desiredRot = Quaternion.AngleAxis(desiredAngle, Vector3.forward);

            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRot, playerRotSpeed * Time.fixedDeltaTime);

        }
        else if (LM.playerControl.IsPlayerInCheckPoint())
		{
            //Quaternion stopRot = Quaternion.Euler(0, 0, 90);
            //transform.rotation = Quaternion.Lerp(transform.rotation, stopRot, playerRotSpeed*100 * Time.fixedDeltaTime);
            //Debug.Log("Accessed");
		}

        //Debug.Log("Is In Waypoint" +isInWaypoint);

    }
}
