using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    bool isInWaypoint = true;

    Rigidbody2D rb2D;


    public float rotateSpeed = 100;


    public bool atMid = true;
    public bool goingLeft = true;
    public bool goingRight = false;

    [SerializeField]
    private Transform tr;

    [SerializeField]
    private Transform ejector;

    [SerializeField]
    private GameObject arrowiks;

    Vector3 kekVec = new Vector3(0, 0.46f, 0);

    #region Speed Variables
    [SerializeField]
    private float ejectForce = 1;
    [SerializeField]
    private float playerRotSpeed=10;
	#endregion

	private void Awake()
	{
        rb2D = GetComponent<Rigidbody2D>();
    }

	void Start()
    {
        AdjustStartingPoint();
        isInWaypoint = true;
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


        if ((Input.GetKey(KeyCode.Space) || Input.touchCount == 1) && isInWaypoint)
        {
            isInWaypoint = !isInWaypoint;
            arrowiks.SetActive(false);

            rb2D.constraints = RigidbodyConstraints2D.None;
            upVector = upVector - kekVec;
            rb2D.AddForce(upVector * ejectForce * Time.fixedDeltaTime);

            slowRot = Quaternion.Slerp(transform.rotation, tr.rotation, Time.fixedDeltaTime * playerRotSpeed);
            rb2D.MoveRotation(slowRot);

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


    private void OnTriggerEnter2D(Collider2D collision)
	{
        //collision.GetComponent<GameObject>().SetActive(false);
        arrowiks.SetActive(true);
        rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
        AdjustStartingPoint();
        isInWaypoint = true;
	}

	private void OnCollisionEnter(Collision collision)
	{
		//vector3 reflect = vector3.reflect(vector3.right, collision.contacts[0].normal);
		//float rot = 90 - mathf.atan2(reflect.z, reflect.x) * mathf.rad2deg;
		//transform.eulerangles = new vector3(0, 0, rot);

	}

    void GetRotAccordingtoVelocity()
	{
		if (!isInWaypoint)
		{
            Vector2 velocity = rb2D.velocity;
            float desiredAngle = (Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg);
            Quaternion desiredRot = Quaternion.AngleAxis(desiredAngle, Vector3.forward);

            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRot, rotateSpeed * Time.fixedDeltaTime);

        }
        else if (isInWaypoint)
		{
            Quaternion stopRot = Quaternion.Euler(0, 0,90);
            transform.rotation = Quaternion.Lerp(transform.rotation, stopRot, playerRotSpeed*2*Time.fixedDeltaTime);
            //Debug.Log("Accessed");
		}

        //Debug.Log("Is In Waypoint" +isInWaypoint);

    }
}
