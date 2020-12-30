using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    [SerializeField]

    private bool isInWaypoint;

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


    public float ejectForce = 1;
    Vector3 kekVec = new Vector3(0, 0.46f, 0);

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        isInWaypoint = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("Z: " + transform.rotation.z +"\n");

        if (tr.rotation.z == -0.0008726012) atMid = true;
        moveArrow();

        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void FixedUpdate()
    {
        getCast();
    }

    Quaternion slowRot;
    void getCast()
    {

        Vector3 upVector = tr.TransformDirection(Vector2.up) * 10;
        Debug.DrawRay(tr.position, upVector, Color.green);


        if (Input.GetKey(KeyCode.Space))
        {
            arrowiks.SetActive(false);

            rb2D.constraints = RigidbodyConstraints2D.None;
            upVector = upVector - kekVec;
            rb2D.AddForce(upVector * ejectForce * Time.fixedDeltaTime);

            slowRot = Quaternion.Slerp(transform.rotation, tr.rotation, Time.fixedDeltaTime * 10);
            rb2D.MoveRotation(slowRot);
        }

    }

    void moveArrow()
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

        Debug.Log("Going Left: " + goingLeft + " Going Right " + goingRight);

    }


    void readJustStartingPoint()
    {
        ejector.transform.position = new Vector3(rb2D.transform.position.x, rb2D.transform.position.y + 0.19f, 0);
        transform.rotation = new Quaternion(0,0,0,0);
    }


    private void OnTriggerEnter2D(Collider2D collision)
	{
        arrowiks.SetActive(true);
        rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
        readJustStartingPoint();
        isInWaypoint = true;
	}

}
