using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;

public class PlayerController : MonoBehaviour
{
    public int inputMode = 0;


    [SerializeField] private GameObject shootingArea;
    private SpriteRenderer shootingAreaSpriteRenderer;
    [SerializeField] private GameObject crosshair;

    [SerializeField] private float shootingRotVal;



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

    Shooting shooting;

    public float arrowTouchRotateOffset = -90f;
    public float inputModeOffset;

    (int, string) wasIsDast;

    private void Awake()
	{
        _rb2D = GetComponent<Rigidbody2D>();

        backgrounds = GetComponent<Transform>();

        inputMode = PlayerPrefs.GetInt("ControlInput", 0);
    }

	void Start()
    {
        //LM = gameObject.AddComponent(typeof(LevelManager)) as LevelManager;
        _levelManager = gameObject.GetComponent<LevelManager>();
        shooting = gameObject.GetComponent<Shooting>();
        shootingAreaSpriteRenderer = shootingArea.GetComponent<SpriteRenderer>();
        //AdjustStartingPoint();
        //LM.playerUpdate(true);
        _levelManager.playerControl.setPlayerCheckPoint(true);

        if (inputMode == 0)
        {
            tr.gameObject.SetActive(true);
            crosshair.gameObject.SetActive(false);
        }
        else
        {
            ejector.position = new Vector3(ejector.position.x, ejector.position.y + inputModeOffset, ejector.position.z);
            tr.gameObject.SetActive(false);
            crosshair.gameObject.SetActive(false);

            shooting.SetShootingPoint(crosshair.transform);
        }


        StartCoroutine(checkVariables());

    }

    void Update()
    {
        // Debug.Log("Z: " + transform.rotation.z +"\n");

        //if (tr.rotation.z == -0.0008726012) atMid = true;
        

        if (Input.GetKey(KeyCode.R) || Input.touchCount == 3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        isPlayerDead = _levelManager.playerControl.isPlayerDead();

 

        if (Application.platform == RuntimePlatform.Android)
        {

            if (Input.GetKeyDown(KeyCode.Escape))
            {

                BackButtonPressed();
                
            }
        }

        CheckJumpInput();
    }

    [SerializeField] private float jumpRate = 1.0f;
    [SerializeField] private float lastJump = 0.0f;
    private bool holdingShoot;
    private void CheckJumpInput()
    {

        if ((Input.GetKey(KeyCode.A)) && _levelManager.playerControl.IsPlayerInCheckPoint() && !isPlayerDead)
        {
            if (Time.time > jumpRate + lastJump) JumpPlayer();
        }
        else if (Input.touchCount == 1 && _levelManager.playerControl.IsPlayerInCheckPoint() && !isPlayerDead)
        {
            Touch touch = Input.touches[0];
            bool touchEnded = touch.phase == TouchPhase.Ended;

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero);
            Vector2 direction = Camera.main.ScreenToWorldPoint(touch.position) - crosshair.transform.position; // 

            //var tempPos = direction;
            //tempPos = new Vector3(Mathf.Abs(tempPos.x), Mathf.Abs(tempPos.y));
            direction.Normalize();

            if (hit.collider != null && hit.collider.tag == "NoTouch")
            {

                //Vector2 pos = Camera.main.ScreenToWorldPoint(touch.position);

                //crosshair.transform.position = new Vector3(pos.x, pos.y + 3, crosshair.transform.position.z);
                //crosshair.gameObject.SetActive(true);

                if(inputMode == 1)
                {
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    //crosshair.transform.rotation = Quaternion.Euler(Vector3.forward * (angle + arrowTouchRotateOffset * -1)); // tr  * Time.fixedDeltaTime * 50f

                    var tempRot = Quaternion.Slerp(crosshair.transform.rotation, Quaternion.Euler(Vector3.forward * (angle + arrowTouchRotateOffset * -1)), Time.deltaTime * shootingRotVal);
                    crosshair.transform.rotation = tempRot;

                    //crosshair.transform.Rotate(Vector3.forward, (angle + arrowTouchRotateOffset * -1) * Time.fixedDeltaTime * shootingRotVal);

                    //var newRot = Quaternion.Slerp(crosshair.transform.rotation, tr.rotation, Time.fixedDeltaTime);
                    //crosshair.transform.rotation = newRot;

                    //tr.gameObject.SetActive(true);


                    


                    //debugtxt.text = tr.rotation.ToString();
                    Debug.Log("Target name: " + hit.collider.name + " " + hit.collider.tag);
                    if (!touchEnded)
                    {
                        if (!crosshair.gameObject.activeSelf)
                        {
                            crosshair.gameObject.SetActive(true);
                            shootingAreaSpriteRenderer.color = new Color32(140, 140, 140, 143);
                            Debug.Log("I'm here!!");

                        }

                        holdingShoot = true;
                    }
                    else
                    {
                        //if (tr.rotation.w >= 0.75f)
                        //{

                        //}


                        tr.gameObject.SetActive(false);
                        crosshair.gameObject.SetActive(false);
                        shootingAreaSpriteRenderer.color = new Color32(140, 140, 140, 0);
                        holdingShoot = false;

                        shooting.SendShoot();
                    }
                }
                else
                {
                    if (touchEnded)
                    {
                        if (tr.rotation.w >= 0.75f)
                        {
                            shooting.SendShoot();
                        }

                        holdingShoot = false;
                    }
                }



                return;
            }
            else
            {
                if (touchEnded)
                {
                    shootingAreaSpriteRenderer.color = new Color32(140, 140, 140, 0);
                    holdingShoot = false;
                    tr.gameObject.SetActive(false);
                    crosshair.gameObject.SetActive(false);
                    return;
                }
            }

            
            if (inputMode == 1 && !holdingShoot)
            {
               
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                tr.rotation = Quaternion.Euler(Vector3.forward * (angle + arrowTouchRotateOffset * -1));


                if (Time.time > jumpRate + lastJump && tr.rotation.w >= 0.75f)
                {
                    tr.gameObject.SetActive(false);
                    crosshair.gameObject.SetActive(false);
                    shootingAreaSpriteRenderer.color = new Color32(140,140,140, 0);

                    JumpPlayer();
                }
                //Debug.Log($"Touched @ {tr.rotation}");
            }

            if (inputMode == 0 && !holdingShoot)
            {
                if (Time.time > jumpRate + lastJump) JumpPlayer();
            }



        }
    }

    private void FixedUpdate()
    {
        if(inputMode == 0 ) MoveArrow();

        
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

    public void JumpPlayer()
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

    private void BackButtonPressed()
    {
        //Application.Quit();

        SceneManager.LoadScene("Main Menu");
    }
}
