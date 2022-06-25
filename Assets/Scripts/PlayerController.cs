using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;

public class PlayerController : MonoBehaviour
{
    public Transform backgrounds;


    Rigidbody2D _rb2D;

    private bool isAtMid;
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


    [SerializeField] private float playerRotSpeed = 10;

    [Header ("Speed Variables")]
    [SerializeField]
    private float ejectForce = 1;
    [SerializeField]
    private float rotateSpeed = 100;

    public TMP_Text debugtxt;



    private void Awake()
	{
        _rb2D = GetComponent<Rigidbody2D>();

        backgrounds = GetComponent<Transform>();
        
    }

	void Start()
    {
        //LM = gameObject.AddComponent(typeof(LevelManager)) as LevelManager;
        _levelManager = gameObject.GetComponent<LevelManager>();
        //AdjustStartingPoint();
        //LM.playerUpdate(true);
        _levelManager.playerControl.setPlayerCheckPoint(true);

        StartCoroutine(checkVariables());
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("Z: " + transform.rotation.z +"\n");

        //if (tr.rotation.z == -0.0008726012) atMid = true;
        

        if (Input.GetKey(KeyCode.R) || Input.touchCount == 3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        isPlayerDead = _levelManager.playerControl.isPlayerDead();

        CheckJumpInput();
    }

    [SerializeField] private float jumpRate = 1.0f;
    [SerializeField] private float lastJump = 0.0f;
    private void CheckJumpInput()
    {
        #region touch to move broken
        //if(Input.GetKey(KeyCode.Mouse0) && _levelManager.playerControl.IsPlayerInCheckPoint() && !isPlayerDead)
        //{



        //    _levelManager.playerControl.setPlayerCheckPoint(false);
        //    arrowiks.SetActive(false);


        //    //upVector = upVector - _kekVec;
        //    //_rb2D.AddForce(upVector * (ejectForce * Time.fixedDeltaTime));

        //    _slowRot = Quaternion.Slerp(transform.rotation, tr.rotation, Time.fixedDeltaTime * playerRotSpeed);
        //    _rb2D.MoveRotation(_slowRot);

        //    //Vector3 vectorUp = tr.TransformDirection(Vector2.up);
        //    _rb2D.velocity = (Camera.main.ScreenToWorldPoint(Input.mousePosition) * 1 * Time.deltaTime);
        //}
        #endregion

        if ((Input.GetKey(KeyCode.A)) && _levelManager.playerControl.IsPlayerInCheckPoint() && !isPlayerDead)
        {
            if (Time.time > jumpRate + lastJump) JumpPlayer();
        }
        else if (Input.touchCount == 1 && _levelManager.playerControl.IsPlayerInCheckPoint() && !isPlayerDead)
        {

 
            Touch touch = Input.touches[0];
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero);

            if (hit.transform != null && hit.collider.tag == "Untouchable")
            {
                Debug.Log("Target name: " + hit.collider.name + " " + hit.collider.tag);
                return;
            }

            if (Time.time > jumpRate + lastJump) JumpPlayer();
        }
    }

    private void FixedUpdate()
    {
        MoveArrow();
        GetCast();
        GetRotAccordingtoVelocity();
    }

    Quaternion _slowRot;
    RaycastHit2D hit;
    void GetCast()
    {
        Vector3 upVector = tr.TransformDirection(Vector2.up) * 20;
        Debug.DrawRay(tr.position, upVector, Color.green);
    }

    private void JumpPlayer()
    {

        Vector3 vectorUp = tr.TransformDirection(Vector2.up);

        _levelManager.playerControl.setPlayerCheckPoint(false);
        arrowiks.SetActive(false);



        //upVector = upVector - _kekVec;
        _rb2D.AddForce(vectorUp * ejectForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
        _slowRot = Quaternion.Slerp(transform.rotation, tr.rotation, Time.fixedDeltaTime * playerRotSpeed);
        _rb2D.MoveRotation(_slowRot);

        lastJump = Time.time;
 

        //_rb2D.velocity = (vectorUp * ejectForce * Time.deltaTime);
    }


    void MoveArrow()
    {
        if (_levelManager.playerControl.IsPlayerInCheckPoint())
        {
            if (goingLeft && !goingRight)
            {

                if (tr.rotation.z >= 0.5007555)
                {
                    goingLeft = !goingLeft;
                    goingRight = !goingRight;
                }
                else tr.Rotate(0, 0, rotateSpeed * Time.fixedDeltaTime); ;

            }

            if (goingRight && !goingLeft)
            {

                if (tr.rotation.z <= -0.5007555)
                {
                    goingRight = !goingRight;
                    goingLeft = !goingLeft;
                }
                else tr.Rotate(0, 0, rotateSpeed * Time.fixedDeltaTime * -1);
            }

            var displacement = Quaternion.Angle(tr.rotation, transform.rotation);

            if (displacement == 0)
            {
                isAtMid = true;
            }
            else
            {
                isAtMid = false;
            }
        }
        

    }



    void AdjustStartingPoint()
    {
        //var position = _rb2D.transform.position;
        //ejector.transform.position = new Vector3(position.x, position.y + 0.19f, 0);
        //transform.rotation = Quaternion.identity; //new Quaternion(0,0,0,0);

        //Vector3 pos = transform.position;
        //pos.y += 0.19f;
        //ejector.position = pos;
        
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

            //Debug.Log("Collision: " + collision.gameObject.tag);

            _rb2D.velocity = Vector2.zero;
            _rb2D.angularVelocity = 0f;

            Vector3 kek = new Vector3(_rb2D.transform.position.x, collision.transform.position.y, 0);
            _rb2D.transform.SetPositionAndRotation(kek, Quaternion.identity);



            _levelManager.playerControl.setPlayerCheckPoint(true);
            arrowiks.SetActive(true);


            //transform.rotation = Quaternion.identity;


        }


        // if (collision.CompareTag("Background"))
        // {
        //     for (int i = 0; i < _count; i++)
        //     {
        //         Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), backgrounds.GetChild(i).GetComponent<Collider2D>());
        //     }
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

    }

    public void setEjectForce(float force)
    {
        this.ejectForce = force;
    }

    public void setArrowRotationSpeed(float speed)
    {
        this.rotateSpeed = speed;
    }

    private IEnumerator checkVariables()
    {
        while (!isPlayerDead)
        {
            setEjectForce(_levelManager.playerControl.getPlayerSpeed());
            setArrowRotationSpeed(_levelManager.playerControl.getPlayerPointerSpeed());

            yield return new WaitForSeconds(1f);
        }
    }
}
