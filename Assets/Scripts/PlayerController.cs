using System.Collections;
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

    Collider2D collider_CP;
    private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.tag == "Checkpoint")
        {
            
            arrowiks.SetActive(true);
            AdjustStartingPoint();

            rb2D.velocity = Vector2.zero;
            rb2D.angularVelocity = 0f;

            Vector3 kek = new Vector3(rb2D.transform.position.x, collision.transform.position.y, 0);
            rb2D.transform.SetPositionAndRotation(kek, transform.rotation);



            collider_CP = collision.GetComponent<Collider2D>();
            collider_CP.enabled = false;

            LM.playerControl.setPlayerCheckPoint(true);
            LM.player_EnteredCheckpoint();

            Debug.Log("Instance ID: " + collider_CP.GetInstanceID());
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

        //Debug.Log("Collision: " + collision.gameObject.tag);

        if (collision.collider.tag.Contains("Untagged"))

        {
            if (collider_CP.enabled == false)
            {
                collider_CP.enabled = true;
            }

        }

    }

    void GetRotAccordingtoVelocity()
	{
		if (!LM.playerControl.IsPlayerInCheckPoint())
        {     

            Vector2 velocity = rb2D.velocity;
            desiredAngle = (Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg);
            Quaternion desiredRot = Quaternion.AngleAxis(desiredAngle - 90, Vector3.forward);
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRot, playerRotSpeed * Time.fixedDeltaTime);

        }
        else if (LM.playerControl.IsPlayerInCheckPoint())
		{
            
            
        }


    }
}
