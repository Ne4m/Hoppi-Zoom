using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class PlatformMovements : MonoBehaviour
{

    public int MovementTypeID;

    private Transform tr;

    [SerializeField] private bool isTweenable = false;

    [SerializeField] private bool isDestructable = true;

    [Header("Speed Values")]
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private float[] specialRotationSpeed;
    [SerializeField] private float[] specialMoveSpeed;
    [SerializeField] private GameObject[] specialChilds;

    [Header("Object Props")]
    [SerializeField] private objectProps objectProperties = new objectProps();
    [SerializeField] private TweenProps tweenProperties = new TweenProps();

    [Header("Child Configuration")]
    [SerializeField] private bool hasChilds;
    [SerializeField] private List<objectProps> childrenProps = new List<objectProps>();


    private Vector3 transformTargetPos = new Vector3();

    private Vector3 platformStartPosition = new Vector3();

    public TMP_Text debugtxt;
    public float angle;
    public float angle2;
    public Vector3 platformStartEuler;

    GameProgress gameProgress;

    private float scaleRatio;



    void Start()
    {
        this.tr = GetComponent<Transform>();
        gameProgress = GameProgress.instance;

        if (objectProperties.startClockwise) rotationSpeed *= -1;


        //Debug.Log($"Platform Spawn Position Y : {platformStartPosition.y}");

        if (Sprite_AutoRes.instance != null)
        {
            scaleRatio = Sprite_AutoRes.instance.scaleRatio;
            objectProperties.startPositionX *= scaleRatio;
            objectProperties.startPositionY *= scaleRatio;
        }

        if (!hasChilds)
        {
            if (objectProperties.setPosition) tr.position = SetPosition(transformTargetPos, !objectProperties.startPositionX.Equals(-1337) ? objectProperties.startPositionX : tr.position.x, !objectProperties.startPositionY.Equals(-1337) ? objectProperties.startPositionY : tr.position.y, tr.position.z);
        }
        else
        {
            tr.position = SetPosition(transformTargetPos, tr.position.x, tr.position.y, tr.position.z);

            for (int i = 0; i < childrenProps.Count; i++)
            {
                if (childrenProps[i].setPosition) tr.GetChild(i).position = SetPosition(transformTargetPos, !childrenProps[i].startPositionX.Equals(-1337) ? childrenProps[i].startPositionX : tr.GetChild(i).position.x, !childrenProps[i].startPositionY.Equals(-1337) ? childrenProps[i].startPositionY : tr.GetChild(i).position.y, tr.GetChild(i).position.z);

            }
        }

        platformStartPosition = tr.position;

        platformStartEuler = tr.localEulerAngles;




        IncreaseMoveSpeed(gameProgress.GetSpeedIncrease());
        IncreaseRotationSpeed(gameProgress.GetRotateSpeedIncrease());
        IncreaseFadeSpeed(gameProgress.GetSpeedIncrease());


        if (isTweenable)
        {
            LeanTweenActions();
        }

        // Array Initializations
        specialChilds = new GameObject[tr.childCount];
        specialRotationSpeed = new float[tr.childCount];
        specialMoveSpeed = new float[tr.childCount];

        for(int i=0; i<specialRotationSpeed.Length; i++)
        {
            specialChilds[i] = tr.GetChild(i).gameObject;
            specialRotationSpeed[i] = rotationSpeed;
            specialMoveSpeed[i] = moveSpeed;
        }


        if(objectProperties.canFade) StartCoroutine(FadeIn());
        //if (objectProperties.canFade) StartCoroutine(FadeOut());


    }


    [System.Serializable]
    public class TweenProps
    {
        [Header("Move Variables")]
        public float moveSpeed;
        public Vector3 startPoint;
        public Vector3[] movePoints;
        public Vector3 endPoint;
        public bool isPingPongMove = true;
        public LeanTweenType moveEaseType;

        [Header("Spline Move Variables")]
        public GameObject[] splineMovePoints;
        public bool useSplinePoints;
        public int allocationSize;


        [Header("Liner Move Variables")]
        public float linearMoveSpeed;
        public Vector3 destinationCoords;
        public bool isPingPongLinearMove = true;
        public LeanTweenType linearMoveEasyType;

        [Header ("Rotate Variables")]
        public float rotateSpeed;
        public float rotateAngle;
        public bool clockWise = false;
        public LeanTweenType rotateEaseType;
        public bool isPingPongRotation = true;


        [Header ("Fade Variables")]
        public float fadeSpeed;
        public float fadeAlphaMin;
        public bool disableCollision = true;
        [HideInInspector] public bool isFaded = false;
        public LeanTweenType fadeEaseType;
        public bool isPingPongFading = true;

        [Header("Scale Variables")]
        public float scaleSpeed;
        [Range(0f, 1000.0f)] public float scalePercent;
        public LeanTweenType scaleEaseType;
        public bool isPingPongScaling = true;

        [Header("Color Change Variables")]
        public Color color;
        public float colorChangeSpeed;
        public LeanTweenType colorChangeEaseType;
        public bool isPingPongColorChange = true;
        public enum BEHAVIOUR
        {
            NONE,
            MOVE,
            LINEAR_MOVE,
            ROTATE,
            FADE,
            SCALE,
            COLOR,
            SPLINE_MOVE
        }

        public BEHAVIOUR[] BEHAVIOURS;

    
    }

    public void LeanTweenActions()
    {

        foreach(var BEHAVIOUR in tweenProperties.BEHAVIOURS)
        {
            switch (BEHAVIOUR)
            {
                case TweenProps.BEHAVIOUR.NONE:
                    Debug.Log("DEFAULT NONE BEHAVIOUR!");
                    break;
                case TweenProps.BEHAVIOUR.MOVE:
                    Move();
                    break;
                case TweenProps.BEHAVIOUR.LINEAR_MOVE:
                    MoveLinear();
                    break;
                case TweenProps.BEHAVIOUR.SPLINE_MOVE:
                    MoveSpline();
                    break;
                case TweenProps.BEHAVIOUR.ROTATE:
                    Rotate();
                    break;
                case TweenProps.BEHAVIOUR.FADE:
                    Fading();
                    break;
                case TweenProps.BEHAVIOUR.SCALE:
                    Scale();
                    break;
                case TweenProps.BEHAVIOUR.COLOR:
                    Color();
                    break;
                default:
                    Debug.Log("HAS NO BEHAVIOUR!");
                    break;
            }
        }

        void Move()
        {
            // Moving Stuff

            int allocationSize = (4 + tweenProperties.movePoints.Length);
            Vector3[] moveVec = new Vector3[allocationSize];


            moveVec[0] = tweenProperties.startPoint;
            moveVec[1] = tweenProperties.startPoint;
            moveVec[allocationSize - 1] = tweenProperties.endPoint;
            moveVec[allocationSize - 2] = tweenProperties.endPoint;

            for (int i=2; i< allocationSize - 2; i++)
            {
                moveVec[i] = tweenProperties.movePoints[i-2];
            }

            // Adjusting Pivot Positions
            for (int i = 0; i < moveVec.Length; i++)
            {
                //Debug.Log($"Move Vector is ({i}) : {moveVec[i]}");

                moveVec[i] += tr.localPosition;
            }


            int _id = LeanTween.moveSplineLocal(gameObject, moveVec, 0f)
                .setLoopType(tweenProperties.isPingPongMove ? LeanTweenType.pingPong : LeanTweenType.notUsed)
                .setSpeed(tweenProperties.moveSpeed)
                .setEase(tweenProperties.moveEaseType).id;

            // LeanTween.drawBezierPath(moveVec[0], moveVec[1], moveVec[2], moveVec[3], 5f);
        }

        void MoveSpline()
        {
            // Moving Stuff

            if (!tweenProperties.useSplinePoints)
            {
                tweenProperties.allocationSize = (4 + tweenProperties.movePoints.Length);
            }
            else
            {
                tweenProperties.allocationSize = (4 + tweenProperties.splineMovePoints.Length);

            }


            Vector3[] moveVec = new Vector3[tweenProperties.allocationSize];


            if (tweenProperties.startPoint == Vector3.zero)
            {
                moveVec[0] = GetLocalPos();
                moveVec[1] = GetLocalPos();
            }
            else
            {
                moveVec[0] = tweenProperties.startPoint;
                moveVec[1] = tweenProperties.startPoint;
            }


            if(tweenProperties.endPoint == Vector3.zero)
            {
                moveVec[tweenProperties.allocationSize - 1] = GetLocalPos();
                moveVec[tweenProperties.allocationSize - 2] = GetLocalPos();
            }
            else
            {
                moveVec[tweenProperties.allocationSize - 1] = tweenProperties.endPoint;
                moveVec[tweenProperties.allocationSize - 2] = tweenProperties.endPoint;
            }






                for (int i = 2; i < tweenProperties.allocationSize - 2; i++)
                {
                    if(tweenProperties.useSplinePoints)
                    {
                        moveVec[i] = tweenProperties.splineMovePoints[i - 2].transform.localPosition;
                    }
                    else
                    {
                        moveVec[i] = tweenProperties.movePoints[i - 2];
                    }
                }

            // Adjusting Pivot Positions
            //for (int i = 0; i < moveVec.Length; i++)
            //{
            //    //Debug.Log($"Move Vector is ({i}) : {moveVec[i]}");

            //    moveVec[i] += tr.localPosition;
            //}


            int _id = LeanTween.moveSplineLocal(gameObject, moveVec, 0f)
                .setLoopType(tweenProperties.isPingPongMove ? LeanTweenType.pingPong : LeanTweenType.notUsed)
                .setSpeed(tweenProperties.moveSpeed)
                .setEase(tweenProperties.moveEaseType).id;
        }

        Vector3 GetLocalPos()
        {

            return tr.localPosition;
        }

        void MoveLinear()
        {
            tweenProperties.destinationCoords += tr.localPosition;

            LeanTween.moveLocal(gameObject, tweenProperties.destinationCoords, 0f)
                 .setLoopType(tweenProperties.isPingPongLinearMove ? LeanTweenType.pingPong : LeanTweenType.notUsed)
                 .setSpeed(tweenProperties.linearMoveSpeed)
                 .setEase(tweenProperties.linearMoveEasyType);
        }

        void Rotate()
        {
            Debug.Log("ROTATE BEHAVIOUR");

            LeanTween.rotateLocal(gameObject, new Vector3(0, 0, tweenProperties.rotateAngle * (tweenProperties.clockWise ? -1f : 1f)), 0f)
                 .setLoopType(tweenProperties.isPingPongRotation ? LeanTweenType.pingPong : LeanTweenType.notUsed)
                 .setSpeed(tweenProperties.rotateSpeed)
                 .setEase(tweenProperties.rotateEaseType);

           

            //LeanTween.rotateAroundLocal(gameObject, new Vector3(0, 0, 60), -60f, 1f).setLoopPingPong();
        }



        void Scale()
        {
           // Debug.Log("SCALE BEHAVIOUR");

            var initialScale = gameObject.transform.localScale;

            LeanTween.scale(gameObject, initialScale + (initialScale * tweenProperties.scalePercent / 100), 0)
                .setLoopType(tweenProperties.isPingPongScaling ? LeanTweenType.pingPong : LeanTweenType.notUsed)
                .setSpeed(tweenProperties.scaleSpeed)
                .setEase(tweenProperties.scaleEaseType);


        }

        void Fading()
        {
            //Debug.Log("FADE BEHAVIOUR");

            LeanTween.alpha(gameObject, tweenProperties.fadeAlphaMin, 0f)
                .setLoopType(tweenProperties.isPingPongFading ? LeanTweenType.pingPong : LeanTweenType.notUsed)
                .setSpeed(tweenProperties.fadeSpeed)
                .setEase(tweenProperties.fadeEaseType)
                .setOnCompleteOnRepeat(true)
                .setOnComplete(() =>
                {
                    if(!tweenProperties.isFaded)
                    {
                        tweenProperties.isFaded = true;
                        if (tweenProperties.disableCollision)
                        {
                            if (tr.GetComponent<Collider2D>() != null)  tr.GetComponent<Collider2D>().enabled = false;
                        }
                    }
                    else
                    {
                        tweenProperties.isFaded = false;

                        if(tr.GetComponent<Collider2D>() != null)
                        {
                            tr.GetComponent<Collider2D>().enabled = true;
                        }

                    }
                });
        }

        void Color()
        {
            //Debug.Log("COLOR BEHAVIOUR");

            LeanTween.color(gameObject, tweenProperties.color, 0)
                .setLoopType(tweenProperties.isPingPongColorChange ? LeanTweenType.pingPong : LeanTweenType.notUsed)
                .setSpeed(tweenProperties.colorChangeSpeed)
                .setEase(tweenProperties.colorChangeEaseType);
        }

       
    }



    private void FixedUpdate()
    {
        scaleRatio = Sprite_AutoRes.instance.scaleRatio;

        if (!objectProperties.isSpecial)
        {

            if (objectProperties.moveable)
            {

                if (objectProperties.moveableX)
                {
                    float boundaryX_Left = objectProperties.moveX[0] * scaleRatio;
                    float boundaryX_Right = objectProperties.moveX[1] * scaleRatio;

                    if (!objectProperties.moveLocally)
                    {
                        if (tr.localPosition.x > boundaryX_Right)
                        {
                            moveSpeed *= -1;
                        }
                        else if (tr.localPosition.x < boundaryX_Left)
                        {
                            moveSpeed *= -1;
                        }

                        tr.Translate(moveSpeed * Time.deltaTime * scaleRatio, 0, 0, Space.World);
                    }
                    else
                    {
                        if (tr.position.x > boundaryX_Right)
                        {
                            moveSpeed *= -1;
                        }
                        else if (tr.position.x < boundaryX_Left)
                        {
                            moveSpeed *= -1;
                        }

                        tr.Translate(moveSpeed * Time.deltaTime * scaleRatio, 0, 0);
                    }


                }

                if (objectProperties.moveableY)
                {
                    float boundaryY_Up = (objectProperties.moveY[0] + platformStartPosition.y) * scaleRatio;
                    float boundaryY_Down = (platformStartPosition.y + objectProperties.moveY[1]) * scaleRatio;

                    //debugtxt.text = $"Local Pos: {tr.localPosition} \nGlobal Pos: {tr.position}";

                    if (!objectProperties.moveLocally)
                    {
                        if (tr.localPosition.y > boundaryY_Up)
                        {
                            moveSpeed *= -1;
                        }
                        else if (tr.localPosition.y < boundaryY_Down)
                        {
                            moveSpeed *= -1;
                        }

                        tr.Translate(0, moveSpeed * Time.deltaTime * scaleRatio, 0, Space.World);
                    }
                    else
                    {
                        if (tr.position.y > boundaryY_Up)
                        {
                            moveSpeed *= -1;
                        }
                        else if (tr.position.y < boundaryY_Down)
                        {
                            moveSpeed *= -1;
                        }

                        tr.Translate(0, moveSpeed * Time.deltaTime * scaleRatio, 0);
                    }


                }
            }

            if (objectProperties.rotatable)
            {

                if (!objectProperties.rotateFreely)
                {
                    float Rotation;
                    if (tr.localEulerAngles.z <= 180f)
                    {
                        Rotation = tr.localEulerAngles.z;
                    }
                    else
                    {
                        Rotation = tr.localEulerAngles.z - 360f;
                    }
                    Rotation -= platformStartEuler.z;


                    if (Rotation > objectProperties.rotateAngle[0])
                    {
                        rotationSpeed *= -1;
                    }

                    if (Rotation < objectProperties.rotateAngle[1] * -1)
                    {
                        rotationSpeed *= -1;
                    }

                    tr.Rotate(0, 0, rotationSpeed * Time.fixedDeltaTime, Space.Self);
                    //debugtxt.text = $"{Rotation}";
                }
                else
                {
                    tr.Rotate(0, 0, rotationSpeed * Time.fixedDeltaTime, Space.Self);
                }


            }

        }
        #region Special Platforms Start
        else
        {


            if (MovementTypeID == 6) // Platform_Spikey_Bridge_Double_1_[6]
            {


                if(specialChilds[0] != null)
                {
                    if (specialChilds[0].transform.rotation.eulerAngles.z >= 60)
                    {
                        specialRotationSpeed[0] *= -1;

                    }

                    if (specialChilds[0].transform.rotation.eulerAngles.z >= 300)
                    {
                        specialRotationSpeed[0] *= -1;

                    }

                    specialChilds[0].transform.Rotate(0, 0, specialRotationSpeed[0] * Time.fixedDeltaTime);
                }



                if (specialChilds[1] != null)
                {
                    if (specialChilds[1].transform.rotation.eulerAngles.z >= 60)
                    {
                        specialRotationSpeed[1] *= -1;

                    }

                    if (specialChilds[1].transform.rotation.eulerAngles.z >= 300)
                    {
                        specialRotationSpeed[1] *= -1;

                    }

                    specialChilds[1].transform.Rotate(0, 0, specialRotationSpeed[1] * Time.fixedDeltaTime * -1);
                }



                //Debug.Log($"Rotation is [LOCAL] {tr.GetChild(0).localEulerAngles.z}\n");
                //Debug.Log($"Rotation is [GLOBAL] {tr.GetChild(0).eulerAngles.z}\n");




            }

            if (MovementTypeID == 7) // Platform_Spikey_Bridge_2_[7]
            {

                // PRE INITIALIZE CHILDS !!!


                if (specialChilds[0] != null)
                {
                    if (specialChilds[0].transform.rotation.z > 0.5372996)
                    {
                        specialRotationSpeed[0] *= -1;

                    }

                    if (specialChilds[0].transform.rotation.z < 0)
                    {
                        specialRotationSpeed[0] *= -1;

                    }

                    //Debug.Log($"Rotation is [LOCAL] {tr.GetChild(0).localEulerAngles.z}\n");
                    //Debug.Log($"Rotation is [GLOBAL] {tr.GetChild(0).eulerAngles.z}\n");
                    //Debug.Log($"Rotation is  {tr.GetChild(0).rotation.z}\n");

                    specialChilds[0].transform.Rotate(0, 0, specialRotationSpeed[0] * Time.fixedDeltaTime);
                }


                if (specialChilds[1] != null)
                {
                    if (specialChilds[1].transform.rotation.z < -0.5372996)
                    {
                        specialRotationSpeed[1] *= -1;

                    }

                    if (specialChilds[1].transform.rotation.z > 0)
                    {
                        specialRotationSpeed[1] *= -1;

                    }

                    //Debug.Log($"Rotation is [LOCAL] {tr.GetChild(1).localEulerAngles.z}\n");
                    //Debug.Log($"Rotation is [GLOBAL] {tr.GetChild(1).eulerAngles.z}\n");
                    //Debug.Log($"Rotation is  {tr.GetChild(1).rotation.z}\n");


                    specialChilds[1].transform.Rotate(0, 0, specialRotationSpeed[1] * Time.fixedDeltaTime * -1);
                }



            }

            if (MovementTypeID == 8) // Platform_Spikey_Bridge_3_[8]
            {

                if(specialChilds[0] != null)
                {

                    specialChilds[0].transform.Rotate(0, 0, specialRotationSpeed[0] * Time.fixedDeltaTime);

                }

            }

            if (MovementTypeID == 9) // Platform_Spikey_Bridge_4_[9]
            {

                if (specialChilds[1] != null)
                {

                    specialChilds[1].transform.Rotate(0, 0, specialRotationSpeed[1] * Time.fixedDeltaTime * -1);

                }

            }


            if (MovementTypeID == 10) // Platform_Spikey_Bridge_3_[8]
            {

                if (specialChilds[0] != null)
                {

                    specialChilds[0].transform.Rotate(0, 0, specialRotationSpeed[0] * Time.fixedDeltaTime);

                }

                if (specialChilds[1] != null)
                {
                    if (specialChilds[1].transform.position.x < 2.385 * scaleRatio)
                    {
                        specialMoveSpeed[1] *= -1;
                    }
                    else if (specialChilds[1].transform.position.x > 3.50 * scaleRatio)
                    {
                        specialMoveSpeed[1] *= -1;
                    }

                    specialChilds[1].transform.Translate(specialMoveSpeed[1] * Time.deltaTime * -1 * scaleRatio, 0, 0);
                }

            }

            if (MovementTypeID == 11) // Platform_Spikey_Bridge_3_[8]
            {

                if (specialChilds[0] != null)
                {
                    if (specialChilds[0].transform.position.x < -3.50 * scaleRatio)
                    {
                        specialMoveSpeed[0] *= -1;
                    }
                    else if (specialChilds[0].transform.position.x > -2.385 * scaleRatio)
                    {
                        specialMoveSpeed[0] *= -1;
                    }

                    specialChilds[0].transform.Translate(specialMoveSpeed[0] * Time.deltaTime * scaleRatio, 0, 0);
                }

                if (specialChilds[1] != null)
                {

                    specialChilds[1].transform.Rotate(0, 0, specialRotationSpeed[1] * Time.fixedDeltaTime * -1);

                }

            }

        }
        #endregion

    }

    [System.Serializable]
    public class objectProps
    {
        public bool isSpecial = false;

        [Header("Platform Move Settings")]
        public bool moveLocally = true;
        public bool moveable;
        public bool moveableX;
        public bool moveableY;
        public float[] moveX = new float[2];
        public float[] moveY = new float[2];

        [Header("Platform Rotation Settings")]
        public bool rotatable;
        public bool startClockwise;
        public bool rotateFreely;
        public float[] rotateAngle = new float[2];


        [Header("Fade In/Out Settings")]
        public bool canFade;
        public float fadeDuration;
        public float fadeSpeed;

        [Header ("Platform Starting Position")]
        public bool setPosition;

        public float startPositionX = -1337;
        public float startPositionY = -1337;

        public float startRotationX;
        public float startRotationY;
        public float startRotationZ;
        public float startRotationW;


    }



    public IEnumerator FadeIn()
    {
        float duration = objectProperties.fadeDuration;
        float speed = objectProperties.fadeSpeed;

        float fadeAmount;



        Renderer renderer = tr.GetComponent<Renderer>();
        Color platformColor = renderer.material.color;

        while (platformColor.a > 0)
        {
            fadeAmount = platformColor.a - (speed * Time.deltaTime);
            platformColor = new Color(platformColor.r, platformColor.g, platformColor.b, fadeAmount);

            renderer.material.color = platformColor;

            if(tr.childCount > 0)
            {
                for (int i = 0; i < tr.childCount; i++)
                {
                    tr.GetChild(i).GetComponent<Renderer>().material.color = platformColor;

                    if (platformColor.a < 0)
                    {
                        tr.GetChild(i).GetComponent<Collider2D>().enabled = false;
                    }
                }
            }

            if(platformColor.a < 0)
            {
                platformColor.a = 0;
                
                tr.GetComponent<Collider2D>().enabled = false;

                yield return new WaitForSeconds(duration);
                StartCoroutine(FadeOut());
            }

           // Debug.Log($"Platform Alpha : {platformColor.a}");
            yield return new WaitForSeconds(0f);
        }

       
    }

    public IEnumerator FadeOut()
    {

        float duration = objectProperties.fadeDuration;
        float speed = objectProperties.fadeSpeed;
        float fadeAmount;

        Renderer renderer = tr.GetComponent<Renderer>();
        Color platformColor = renderer.material.color;

        while (platformColor.a < 1)
        {
            fadeAmount = platformColor.a + (speed * Time.deltaTime);
            platformColor = new Color(platformColor.r, platformColor.g, platformColor.b, fadeAmount);

            renderer.material.color = platformColor;

            if (tr.childCount > 0)
            {
                for (int i = 0; i < tr.childCount; i++)
                {
                    tr.GetChild(i).GetComponent<Renderer>().material.color = platformColor;

                    if (platformColor.a > 1)
                    {
                        tr.GetChild(i).GetComponent<Collider2D>().enabled = true;
                    }
                }


            }

            if (platformColor.a > 1)
            {
                platformColor.a = 1;

                tr.GetComponent<Collider2D>().enabled = enabled;

                yield return new WaitForSeconds(duration);
                StartCoroutine(FadeIn());
            }

           // Debug.Log($"Platform Alpha : {platformColor.a}");
            yield return new WaitForSeconds(0f);
        }


    }

    private Vector3 SetPosition(Vector3 pos, float x, float y, float z)
    {
        pos.x = x;
        pos.y = y;
        pos.z = z;
        return pos;
    }

    public float GetMoveSpeed()
    {
        return this.moveSpeed;
    }

    public void SetMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    public void IncreaseMoveSpeed(float amount)
    {
        if (isTweenable)
        {
            tweenProperties.moveSpeed += amount;

        }

        this.moveSpeed += amount;

        for (int i = 0; i < specialRotationSpeed.Length; i++)
        {
            this.specialMoveSpeed[i] += amount;
        }

    }

    public void IncreaseRotationSpeed(float amount)
    {
        if (isTweenable)
        {
            tweenProperties.rotateSpeed += amount;
            tweenProperties.linearMoveSpeed += amount;
        }

        this.rotationSpeed += amount;

        for (int i = 0; i < specialRotationSpeed.Length; i++)
        {
            this.specialRotationSpeed[i] += amount;
        }
    }

    public void IncreaseFadeSpeed(float amount)
    {
        if (isTweenable)
        {
            tweenProperties.fadeSpeed += amount;
        }

        this.objectProperties.fadeSpeed += amount;
    }

    public bool GetIsDestructable()
    {
        return this.isDestructable ;
    }
}
