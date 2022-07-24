using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public int inputMode = 0;


    [SerializeField] private GameObject shootingArea;
    private SpriteRenderer shootingAreaSpriteRenderer;
    [SerializeField] private GameObject crosshair;
    [SerializeField] private float shootingRotVal;



    private Rigidbody2D _rb2D;

   // private bool isAtMid;
    private bool goingLeft = true;
    private bool goingRight = false;
    private bool isPlayerDead;

    public float desiredAngle;

    [SerializeField]private Transform tr;
    [SerializeField]private Transform ejector;
    [SerializeField]private GameObject arrowiks;

    LevelManager _levelManager;



    [Header ("Speed Variables")]
    [SerializeField] private float ejectForce = 1f;
    [SerializeField] private float rotateSpeed = 100;
    [SerializeField] private float fallSpeed = 1.5f;
    [SerializeField] private float playerRotSpeed = 10;

    public TMP_Text debugtxt;

    public TMP_Dropdown comboBox, comboBox2;

    Shooting shooting;

    [SerializeField] private float checkPointPlaceOffset;
    [SerializeField] private float arrowTouchRotateOffset = -90f;
    [SerializeField] private float inputModeOffset;

    private void Awake()
	{
        inputMode = SPrefs.GetInt("ControlInput", 0);
    }

    private int scaleID_1, scaleID_2;
    private Vector3 initialScale;
    public LeanTweenType _type, _type2;


    void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();

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

            if(inputMode == 1) shooting.SetShootingPoint(crosshair.transform);
           // else shooting.SetShootingPoint(tr);
        }



        initialScale = _rb2D.gameObject.transform.localScale;

        //StartCoroutine(checkVariables());

        setEjectForce(_levelManager.playerControl.getPlayerSpeed());
        setArrowRotationSpeed(_levelManager.playerControl.getPlayerPointerSpeed());



        comboBox.ClearOptions();
        comboBox.AddOptions(LeanTween.GetTweenTypes());

        comboBox2.ClearOptions();
        comboBox2.AddOptions(LeanTween.GetTweenTypes());
    }

    void Update()
    {
        

        if (Input.GetKey(KeyCode.R) || Input.touchCount == 2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        isPlayerDead = _levelManager.playerControl.isPlayerDead();

 

        //if (Application.platform == RuntimePlatform.Android)
        //{

        //}

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            BackButtonPressed();
                
        }


        CheckJumpInput();


        if (_rb2D.velocity.y < 0 && !_levelManager.playerControl.IsPlayerInCheckPoint())
        {
            //_rb2D.velocity = new Vector2(_rb2D.velocity.x, (fallSpeed));
            SetGravity.On(_rb2D, fallSpeed);

        }
        else
        {
            SetGravity.Off(_rb2D);
        }
    }

    void EjectTweenStart()
    {
        _rb2D.gameObject.LeanScale(new Vector3(initialScale.x, initialScale.y - 0.4f, initialScale.z), 0.25f)
            .setEase(LeanTweenType.easeInOutQuart)
            .setOnComplete(EjectTweenStop);

        //if (!LeanTween.isTweening(scaleID_1))
        //{
        //    LeanTween.resume(scaleID_1);
        //}


       // Debug.Log($"Pivot is : {gameObject.GetComponent<SpriteRenderer>().sprite.pivot}");
    }

    void EjectTweenStop()
    {
        scaleID_2 = _rb2D.gameObject.LeanScale(new Vector3(initialScale.x, initialScale.y, initialScale.z), 0.10f).setEase(LeanTweenType.easeInOutQuart).id;
    }
    
    public class SetGravity
    {

        public static void On(Rigidbody2D rigidBody, float initialValue)
        {
            SetGravityScale(initialValue, rigidBody);
        }

        public static void Off(Rigidbody2D rigidBody)
        {
            SetGravityScale(0, rigidBody);
        }

        private static void SetGravityScale(float val, Rigidbody2D rigidBody)
        {
            rigidBody.gravityScale = val;
        }
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

            direction.Normalize();

            if (hit.collider != null && hit.collider.tag == "NoTouch")
            {

                if(inputMode == 1)
                {
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    var tempRot = Quaternion.Slerp(crosshair.transform.rotation, Quaternion.Euler(Vector3.forward * (angle + arrowTouchRotateOffset * -1)), Time.deltaTime * shootingRotVal);
                    crosshair.transform.rotation = tempRot;

                    if (!touchEnded)
                    {
                        if (!crosshair.gameObject.activeSelf)
                        {
                            crosshair.gameObject.SetActive(true);
                            shootingAreaSpriteRenderer.color = new Color32(140, 140, 140, 143);

                        }

                        holdingShoot = true;
                    }
                    else
                    {
                        crosshair.gameObject.SetActive(false);
                        shootingAreaSpriteRenderer.color = new Color32(140, 140, 140, 0);
                        holdingShoot = false;

                        shooting.SendShoot();
                    }
                }
                else if(inputMode == 2)
                {
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    var tempRot = Quaternion.Slerp(crosshair.transform.rotation, Quaternion.Euler(Vector3.forward * (angle + arrowTouchRotateOffset * -1)), Time.deltaTime * shootingRotVal);
                    crosshair.transform.rotation = tempRot;


                    //debugtxt.gameObject.SetActive(true);
                    //debugtxt.text = crosshair.transform.rotation.ToString();

                    if (!touchEnded)
                    {
                        if (!crosshair.gameObject.activeSelf)
                        {
                            crosshair.gameObject.SetActive(true);
                            shootingAreaSpriteRenderer.color = new Color32(140, 140, 140, 143);

                        }

                        holdingShoot = true;
                    }
                    else // Jump here
                    {
                        crosshair.gameObject.SetActive(false);
                        shootingAreaSpriteRenderer.color = new Color32(140, 140, 140, 0);
                        holdingShoot = false;

                        //shooting.SendShoot();
                        var rotationW = crosshair.transform.rotation.w;
                        if (rotationW > 0)
                        {
                            if(rotationW > 0.75f)
                                JumpPlayerMode2();
                        }
                        else
                        {
                            if (rotationW < -0.75f)
                                JumpPlayerMode2();
                        }


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


            if (inputMode == 2 && !holdingShoot)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                tr.rotation = Quaternion.Euler(Vector3.forward * (angle + arrowTouchRotateOffset * -1));


                if (Time.time > jumpRate + lastJump && tr.rotation.w >= 0.75f)
                {
                    tr.gameObject.SetActive(false);
                    crosshair.gameObject.SetActive(false);
                    shootingAreaSpriteRenderer.color = new Color32(140, 140, 140, 0);

                    //JumpPlayer();
                    shooting.SendShoot();
                }
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



        _rb2D.AddForce(vectorUp * ejectForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
        _slowRot = Quaternion.Slerp(transform.rotation, tr.rotation, Time.fixedDeltaTime * playerRotSpeed);
        _rb2D.MoveRotation(_slowRot);

        lastJump = Time.time;

        FindObjectOfType<AudioManager>().Play("Jump");
        //_rb2D.velocity = (vectorUp * ejectForce * Time.deltaTime);

        EjectTweenStart();
    }

    public void JumpPlayerMode2()
    {

        Vector3 vectorUp = crosshair.transform.TransformDirection(Vector2.up);

        _levelManager.playerControl.setPlayerCheckPoint(false);
        arrowiks.SetActive(false);



        _rb2D.AddForce(vectorUp * ejectForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
        _slowRot = Quaternion.Slerp(transform.rotation, crosshair.transform.rotation, Time.fixedDeltaTime * playerRotSpeed);
        _rb2D.MoveRotation(_slowRot);

        lastJump = Time.time;

        FindObjectOfType<AudioManager>().Play("Jump");
        //_rb2D.velocity = (vectorUp * ejectForce * Time.deltaTime);

        EjectTweenStart();
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

            //if (displacement == 0)
            //{
            //    isAtMid = true;
            //}
            //else
            //{
            //    isAtMid = false;
            //}

            //Debug.Log($"At Mid {isAtMid}");
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
            SetGravity.Off(_rb2D);

            Vector3 placedPoint = new Vector3(_rb2D.transform.position.x, collision.transform.position.y + checkPointPlaceOffset, 0);
            _rb2D.transform.SetPositionAndRotation(placedPoint, Quaternion.identity);



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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        FindObjectOfType<AudioManager>().IsPlaying("Bounce", (callback) =>
        {
            if (!callback) FindObjectOfType<AudioManager>().Play("Bounce");
        });





            gameObject.LeanScaleY(initialScale.y - 0.25f, 0.15f).setEase(_type).setOnComplete(() =>
            {
                gameObject.LeanScaleY(initialScale.y, 0.15f).setEase(_type2);
            });


        //if (collision.collider.tag == "Boundary")
        //{
        //    Debug.Log("boundary hit");
        //}
    }


    
    public void Z_ScaleTest(float t)
    {
        // LeanTween.scale(_rb2D.gameObject, new Vector3(initialScale.x, initialScale.y - 0.25f, initialScale.z), t);
        gameObject.LeanScaleY(initialScale.y - 0.20f, t).setEase(_type).setOnComplete(() =>
        {
            gameObject.LeanScaleY(initialScale.y, t).setEase(_type2);
        });
    }

    public void Z_UnScaleTest(float t)
    {
        //LeanTween.scale(_rb2D.gameObject, new Vector3(initialScale.x, initialScale.y + 0.25f, initialScale.z), t);


    }

    public void Z_SetTweenType()
    {
        int val = comboBox.value;
        string txt = comboBox.options[val].text;

        Enum.TryParse(txt, out _type);
        Debug.Log($"Parsed Enum 1 {_type}");


        int val2 = comboBox2.value;
        string txt2 = comboBox2.options[val2].text;

        Enum.TryParse(txt, out _type2);
        Debug.Log($"Parsed Enum 2 {_type2}");
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
       // Debug.Log("Got Ejectforce Value from Level Manager : " + force);
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
