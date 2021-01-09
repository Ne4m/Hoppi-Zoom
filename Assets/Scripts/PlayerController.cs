using System.Collections;
using System.Collections.Generic;
using Platforms;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Transform backgrounds;


    Rigidbody2D _rb2D;
    
    //private bool atMid = true;
    private bool goingLeft = true;
    private bool goingRight = false;
    private bool isPlayerDead;

    public float desiredAngle;

    [SerializeField]
    private Transform tr;

    [SerializeField]
    private Transform ejector;

    [SerializeField]
    private GameObject arrowiks;

    readonly Vector3 _kekVec = new Vector3(0, 0.46f, 0);
    LevelManager _levelManager;
    

    #region Speed Variables
    [SerializeField]
    private float ejectForce = 1;
    [SerializeField]
    private float playerRotSpeed=10;
    [SerializeField]
    private float rotateSpeed = 100;
	#endregion
    
    
	private void Awake()
	{
        _rb2D = GetComponent<Rigidbody2D>();

        backgrounds = GetComponent<Transform>();
        
    }

	void Start()
    {
        //LM = gameObject.AddComponent(typeof(LevelManager)) as LevelManager;
        _levelManager = gameObject.GetComponent<LevelManager>();
        AdjustStartingPoint();
        //LM.playerUpdate(true);
        _levelManager.playerControl.setPlayerCheckPoint(true);
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("Z: " + transform.rotation.z +"\n");

        //if (tr.rotation.z == -0.0008726012) atMid = true;
        

        if (Input.GetKey(KeyCode.R) || Input.touchCount == 2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        isPlayerDead = _levelManager.playerControl.isPlayerDead();
    }

    private void FixedUpdate()
    {
        MoveArrow();
        GetCast();
        GetRotAccordingtoVelocity();
    }

    Quaternion _slowRot;
    void GetCast()
    {

        Vector3 upVector = tr.TransformDirection(Vector2.up) * 10;
        //Debug.DrawRay(tr.position, upVector, Color.green);


        if ((Input.GetKey(KeyCode.A) || Input.touchCount == 1) && _levelManager.playerControl.IsPlayerInCheckPoint() && !isPlayerDead)
        {
            _levelManager.playerControl.setPlayerCheckPoint(false);
            arrowiks.SetActive(false);


            upVector = upVector - _kekVec;
            _rb2D.AddForce(upVector * (ejectForce * Time.fixedDeltaTime));

            _slowRot = Quaternion.Slerp(transform.rotation, tr.rotation, Time.fixedDeltaTime * playerRotSpeed);
            _rb2D.MoveRotation(_slowRot);
            
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

    }


    void AdjustStartingPoint()
    {
        var position = _rb2D.transform.position;
        ejector.transform.position = new Vector3(position.x, position.y + 0.19f, 0);
        transform.rotation = new Quaternion(0,0,0,0);
    }

    Collider2D _colliderCp;
    int[] cPInstanceID = new int[2];
    private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.CompareTag("Checkpoint"))
        {
            _colliderCp = collision.GetComponent<Collider2D>();
            if(cPInstanceID[0] == 0)
            {
                cPInstanceID[0] = _colliderCp.GetInstanceID();
                _levelManager.player_EnteredCheckpoint();
            }
            else if(cPInstanceID[1] == 0)
            {
                cPInstanceID[1] = _colliderCp.GetInstanceID();
                if (cPInstanceID[0] != cPInstanceID[1])
                {
                    _levelManager.player_EnteredCheckpoint();
                }
            }
            else if (cPInstanceID[0] != 0)
            {
                cPInstanceID[0] = cPInstanceID[1];
                cPInstanceID[1] = _colliderCp.GetInstanceID();
                if (cPInstanceID[0] != cPInstanceID[1])
                {
                    _levelManager.player_EnteredCheckpoint();
                }
            }
            
            

            _rb2D.velocity = Vector2.zero;
            _rb2D.angularVelocity = 0f;

            Vector3 kek = new Vector3(_rb2D.transform.position.x, collision.transform.position.y, 0);
            _rb2D.transform.SetPositionAndRotation(kek, transform.rotation);


           // _colliderCp.enabled = false;
            _levelManager.playerControl.setPlayerCheckPoint(true);
            
            
            arrowiks.SetActive(true);
            AdjustStartingPoint();
            

        }


        // if (collision.CompareTag("Background"))
        // {
        //     for (int i = 0; i < _count; i++)
        //     {
        //         Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), backgrounds.GetChild(i).GetComponent<Collider2D>());
        //     }
        // }
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{

        //Debug.Log("Collision: " + collision.gameObject.tag);

        // if (collision.collider.tag.Contains("Untagged") && _colliderCp != null)
        //
        // {
        //     if (_colliderCp.enabled == false)
        //     {
        //         _colliderCp.enabled = true;
        //     }
        //
        // }

    }

    void GetRotAccordingtoVelocity()
	{
		if (!_levelManager.playerControl.IsPlayerInCheckPoint())
        {     

            Vector2 velocity = _rb2D.velocity;
            desiredAngle = (Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg);
            Quaternion desiredRot = Quaternion.AngleAxis(desiredAngle - 90, Vector3.forward);
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRot, playerRotSpeed * Time.fixedDeltaTime);

        }
        else if (_levelManager.playerControl.IsPlayerInCheckPoint())
		{
            
            
        }


    }
}
