using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovements : MonoBehaviour
{

    public int MovementTypeID;

    private Transform tr;

    [SerializeField] private bool isDestructable = true;

    [Header("Speed Values")]
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private float[] specialRotationSpeed;
    [SerializeField] private float[] specialMoveSpeed;
    [SerializeField] private GameObject[] specialChilds;

    [Header("Object Props")]
    [SerializeField] private objectProps objectProperties = new objectProps();

    [Header("Child Configuration")]
    [SerializeField] private bool hasChilds;
    [SerializeField] private List<objectProps> childrenProps = new List<objectProps>();


    private Vector3 transformTargetPos = new Vector3();

    private Vector3 platformStartPosition = new Vector3();

    GameProgress gameProgress;

    void Start()
    {
        this.tr = GetComponent<Transform>();
        gameProgress = GameProgress.instance;

        platformStartPosition = tr.position;
        //Debug.Log($"Platform Spawn Position Y : {platformStartPosition.y}");

        if (!hasChilds)
        {
            if(objectProperties.setPosition) tr.position = SetPosition(transformTargetPos, !objectProperties.startPositionX.Equals(-1337) ? objectProperties.startPositionX : tr.position.x, !objectProperties.startPositionY.Equals(-1337) ? objectProperties.startPositionY : tr.position.y, tr.position.z);
        }
        else
        {
            tr.position = SetPosition(transformTargetPos, tr.position.x, tr.position.y, tr.position.z);

            for (int i = 0; i < childrenProps.Count; i++)
            {
                if(childrenProps[i].setPosition) tr.GetChild(i).position = SetPosition(transformTargetPos, !childrenProps[i].startPositionX.Equals(-1337) ? childrenProps[i].startPositionX : tr.GetChild(i).position.x, !childrenProps[i].startPositionY.Equals(-1337) ? childrenProps[i].startPositionY : tr.GetChild(i).position.y, tr.GetChild(i).position.z);

            }
        }

        IncreaseMoveSpeed(gameProgress.GetSpeedIncrease());
        IncreaseRotationSpeed(gameProgress.GetSpeedIncrease());
        IncreaseFadeSpeed(gameProgress.GetSpeedIncrease());


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

    private void FixedUpdate()
    {
        if (!objectProperties.isSpecial)
        {
            if (objectProperties.moveable)
            {

                if (objectProperties.moveableX)
                {
                    float boundaryX_Left = objectProperties.moveX[0];
                    float boundaryX_Right = objectProperties.moveX[1];

                    if (tr.position.x > boundaryX_Right)
                    {
                        moveSpeed *= -1;
                    }
                    else if (tr.position.x < boundaryX_Left)
                    {
                        moveSpeed *= -1;
                    }

                    tr.Translate(moveSpeed * Time.deltaTime, 0, 0);
                }

                if (objectProperties.moveableY)
                {
                    float boundaryY_Up = objectProperties.moveY[0] + platformStartPosition.y;
                    float boundaryY_Down = platformStartPosition.y + objectProperties.moveY[1];

                    tr.Translate(0, moveSpeed * Time.deltaTime, 0);

                    if (tr.position.y > boundaryY_Up)
                    {
                        moveSpeed *= -1;
                    }
                    else if (tr.position.y < boundaryY_Down)
                    {
                        moveSpeed *= -1;
                    }


                }
            }

        }
        #region Special Platforms Start
        else
        {
            if (MovementTypeID == 3) // Platform_Spikey_Kinetic_[3]
            {

                if (specialChilds[0] != null)
                {
                    if (specialChilds[0].transform.position.x < -3.65)
                    {
                        specialMoveSpeed[0] *= -1;
                    }
                    else if (specialChilds[0].transform.position.x >= -1.48)
                    {
                        specialMoveSpeed[0] *= -1;
                    }

                    specialChilds[0].transform.Translate(-1 * specialMoveSpeed[0] * Time.deltaTime, 0, 0);
                }

                if (specialChilds[1] != null)
                {
                    if (specialChilds[1].transform.position.x < 1.48)
                    {
                        specialMoveSpeed[1] *= -1;
                    }
                    else if (specialChilds[1].transform.position.x >= 3.65)
                    {
                        specialMoveSpeed[1] *= -1;
                    }

                    specialChilds[1].transform.Translate(specialMoveSpeed[1] * Time.deltaTime, 0, 0);
                }



            }

            if (MovementTypeID == 5) // Platform_Spikey_Kinetic_[5]
            {

                if(specialChilds[0] != null)
                {
                    if (specialChilds[0].transform.position.x < -2.39)
                    {
                        specialMoveSpeed[0] *= -1;
                    }
                    else if (specialChilds[0].transform.position.x > -1.13)
                    {
                        specialMoveSpeed[0] *= -1;
                    }

                    specialChilds[0].transform.Translate(-1 * specialMoveSpeed[0] * Time.deltaTime, 0, 0);
                }

                if (specialChilds[1] != null)
                {
                    if (specialChilds[1].transform.position.x < 1.13)
                    {
                        specialMoveSpeed[1] *= -1;
                    }
                    else if (specialChilds[1].transform.position.x > 2.39)
                    {
                        specialMoveSpeed[1] *= -1;
                    }

                    specialChilds[1].transform.Translate(specialMoveSpeed[1] * Time.deltaTime, 0, 0);
                }



            }

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
                    if (specialChilds[1].transform.position.x < 2.385)
                    {
                        specialMoveSpeed[1] *= -1;
                    }
                    else if (specialChilds[1].transform.position.x > 3.50)
                    {
                        specialMoveSpeed[1] *= -1;
                    }

                    specialChilds[1].transform.Translate(specialMoveSpeed[1] * Time.deltaTime * -1, 0, 0);
                }

            }

            if (MovementTypeID == 11) // Platform_Spikey_Bridge_3_[8]
            {

                

                if (specialChilds[0] != null)
                {
                    if (specialChilds[0].transform.position.x < -3.50)
                    {
                        specialMoveSpeed[0] *= -1;
                    }
                    else if (specialChilds[0].transform.position.x > -2.385)
                    {
                        specialMoveSpeed[0] *= -1;
                    }

                    specialChilds[0].transform.Translate(specialMoveSpeed[0] * Time.deltaTime, 0, 0);
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
        [Header ("Platform Move Settings")]
        public bool isSpecial = false;
        public bool moveable;
        public bool moveableX;
        public bool moveableY;

        public float[] moveX = new float[2];
        public float[] moveY = new float[2];

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
        this.moveSpeed += amount;

        for (int i = 0; i < specialRotationSpeed.Length; i++)
        {
            this.specialMoveSpeed[i] += amount;
        }

    }

    public void IncreaseRotationSpeed(float amount)
    {
        this.rotationSpeed += amount;

        for (int i = 0; i < specialRotationSpeed.Length; i++)
        {
            this.specialRotationSpeed[i] += amount;
        }
    }

    public void IncreaseFadeSpeed(float amount)
    {
        this.objectProperties.fadeSpeed += amount;
    }
}
