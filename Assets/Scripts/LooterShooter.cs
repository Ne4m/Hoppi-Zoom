using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LooterShooter : MonoBehaviour
{
   // [SerializeField]
    public float rotateSpeed = 100;


    public Vector3 rot;

    public bool atMid = true;
    public bool goingLeft = true;
    public bool goingRight = false;

    [SerializeField]
    private Transform tr;

    [SerializeField]
    private Rigidbody2D rb2D;


    public float ejectForce = 1;

    public Vector3 kekVec = new Vector3(0, 0.46f, 0);



    // Start is called before the first frame update
    void Start()
    {
 
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

    void getCast()
    {

        Vector3 upVector = tr.TransformDirection(Vector2.up) * 10;
        Debug.DrawRay(tr.position, upVector, Color.green);

        if (Input.GetKey(KeyCode.Space))
        {
            upVector = upVector - kekVec;
            rb2D.AddForce(upVector * ejectForce * Time.fixedDeltaTime);
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
}
