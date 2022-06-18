using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovements : MonoBehaviour
{
    // Start is called before the first frame update

    public int MovementTypeID = 1;

    [SerializeField]
    private bool hasChilds;

    private Transform tr;

    [Header ("Speed Values")]
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float rotationSpeed = 1f;

    [Header("Object Props")]
    [SerializeField] private objectProps objectProperties = new objectProps();

    private bool goingRight = true;

    void Start()
    {
        this.tr = GetComponent<Transform>();

        switch (MovementTypeID)
        {
            case -1:
                break;

            case 0: // middle
                tr.transform.SetPositionAndRotation(new Vector3(0, tr.position.y, tr.position.z), new Quaternion(tr.rotation.x, tr.rotation.y, tr.rotation.z, tr.rotation.w));
                break;
            case 1: // left
                tr.transform.SetPositionAndRotation(new Vector3(0.83f, tr.position.y, tr.position.z), new Quaternion(tr.rotation.x, tr.rotation.y, tr.rotation.z, tr.rotation.w));
                break;
            case 2: // right
                tr.transform.SetPositionAndRotation(new Vector3(-0.83f, tr.position.y, tr.position.z), new Quaternion(tr.rotation.x, tr.rotation.y, tr.rotation.z, tr.rotation.w));
                break;

            case 3:
                tr.GetChild(0).SetPositionAndRotation(new Vector3(-1.48f, tr.GetChild(0).position.y, tr.GetChild(0).position.z), new Quaternion(tr.GetChild(0).rotation.x, tr.GetChild(0).rotation.y, tr.GetChild(0).rotation.z, tr.GetChild(0).rotation.w));
                tr.GetChild(1).SetPositionAndRotation(new Vector3(1.48f, tr.GetChild(1).position.y, tr.GetChild(1).position.z), new Quaternion(tr.GetChild(1).rotation.x, tr.GetChild(1).rotation.y, tr.GetChild(1).rotation.z, tr.GetChild(1).rotation.w));
                break;

            case 4:
                tr.transform.SetPositionAndRotation(new Vector3(0, tr.position.y, tr.position.z), new Quaternion(tr.rotation.x, tr.rotation.y, tr.rotation.z, tr.rotation.w));
                break;

            case 5:
                tr.GetChild(0).SetPositionAndRotation(new Vector3(-2.39f, tr.GetChild(0).position.y, tr.GetChild(0).position.z), new Quaternion(tr.GetChild(0).rotation.x, tr.GetChild(0).rotation.y, tr.GetChild(0).rotation.z, tr.GetChild(0).rotation.w));
                tr.GetChild(1).SetPositionAndRotation(new Vector3(2.39f, tr.GetChild(1).position.y, tr.GetChild(1).position.z), new Quaternion(tr.GetChild(1).rotation.x, tr.GetChild(1).rotation.y, tr.GetChild(1).rotation.z, tr.GetChild(1).rotation.w));
                break;

            case 6:
                tr.GetChild(0).SetPositionAndRotation(new Vector3(-2.385f, tr.GetChild(0).position.y, tr.GetChild(0).position.z), new Quaternion(tr.GetChild(0).rotation.x, tr.GetChild(0).rotation.y, tr.GetChild(0).rotation.z, tr.GetChild(0).rotation.w));
                tr.GetChild(1).SetPositionAndRotation(new Vector3(2.385f, tr.GetChild(1).position.y, tr.GetChild(1).position.z), new Quaternion(tr.GetChild(1).rotation.x, tr.GetChild(1).rotation.y, tr.GetChild(1).rotation.z, tr.GetChild(1).rotation.w));
                break;

            case 7:
                tr.GetChild(0).SetPositionAndRotation(new Vector3(-2.385f, tr.GetChild(0).position.y, tr.GetChild(0).position.z), new Quaternion(tr.GetChild(0).rotation.x, tr.GetChild(0).rotation.y, tr.GetChild(0).rotation.z, tr.GetChild(0).rotation.w));
                tr.GetChild(1).SetPositionAndRotation(new Vector3(2.385f, tr.GetChild(1).position.y, tr.GetChild(1).position.z), new Quaternion(tr.GetChild(1).rotation.x, tr.GetChild(1).rotation.y, tr.GetChild(1).rotation.z, tr.GetChild(1).rotation.w));
                break;

            case 8:
                tr.transform.SetPositionAndRotation(new Vector3(0, tr.position.y, tr.position.z), new Quaternion(tr.rotation.x, tr.rotation.y, tr.rotation.z, tr.rotation.w));
                break;

            case 9:
                tr.transform.SetPositionAndRotation(new Vector3(0, tr.position.y, tr.position.z), new Quaternion(tr.rotation.x, tr.rotation.y, tr.rotation.z, tr.rotation.w));
                break;

            case 10:
                tr.transform.SetPositionAndRotation(new Vector3(0, tr.position.y, tr.position.z), new Quaternion(tr.rotation.x, tr.rotation.y, tr.rotation.z, tr.rotation.w));
                break;

            default:
                tr.transform.SetPositionAndRotation(new Vector3(0, tr.position.y, tr.position.z), new Quaternion(tr.rotation.x, tr.rotation.y, tr.rotation.z, tr.rotation.w));
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void FixedUpdate()
    {
        if (objectProperties.moveable)
        {

            if (objectProperties.moveableX)
            {
                float boundaryX_Left = objectProperties.moveX[0];
                float boundaryX_Right = objectProperties.moveX[1];

                if(tr.position.x > boundaryX_Right)
                {
                    moveSpeed *= -1;
                }
                else if(tr.position.x < boundaryX_Left)
                {
                    moveSpeed *= -1;
                }

                tr.Translate(moveSpeed * Time.deltaTime, 0, 0);
            }

            if (objectProperties.moveableY)
            {
                float boundaryY_Up = objectProperties.moveY[0];
                float boundaryY_Down = objectProperties.moveY[1];

                if (tr.position.y > boundaryY_Up)
                {
                    moveSpeed *= -1;
                }
                else if (tr.position.y < boundaryY_Down)
                {
                    moveSpeed *= -1;
                }

                tr.Translate(0, moveSpeed * Time.deltaTime, 0);
            }
        }
        

        if(MovementTypeID == 0) // Kinetic Middle
        {
            if (goingRight)
            {
             
                if(tr.position.x >= 4.14)
                {
                    goingRight = false;
                    moveSpeed *= -1;
                }
            }
            else
            {
                if(tr.position.x < -4.14)
                {
                    goingRight = true;
                    moveSpeed *= -1;
                }
            }

            tr.Translate(moveSpeed * Time.deltaTime, 0, 0);
        }

        if (MovementTypeID == 1) // Kinetic Left
        {
            if (goingRight)
            {

                if (tr.position.x > 2.43)
                {
                    goingRight = false;
                    moveSpeed *= -1;
                }
            }
            else
            {
                if (tr.position.x <= 0.83)
                {
                    goingRight = true;
                    moveSpeed *= -1;
                }
            }

            tr.Translate(moveSpeed * Time.deltaTime, 0, 0);
        }

        if (MovementTypeID == 2) // Kinetic Right
        {
            if (!goingRight)
            {

                if (tr.position.x > -0.83)
                {
                    goingRight = true;
                    moveSpeed *= -1;
                }
            }
            else
            {
                if (tr.position.x <= -2.43)
                {
                    goingRight = false;
                    moveSpeed *= -1;
                }
            }

            tr.Translate(-1 * moveSpeed * Time.deltaTime, 0, 0);
        }

        if (MovementTypeID == 3) // Platform_Spikey_Kinetic_[3]
        {


            if (tr.GetChild(0).position.x < -3.65)
            {
                moveSpeed *= -1;
            }
            else if (tr.GetChild(0).position.x >= -1.48)
            {
                moveSpeed *= -1;
            }

            tr.GetChild(0).Translate(-1 * moveSpeed * Time.deltaTime, 0, 0);
            tr.GetChild(1).Translate(moveSpeed * Time.deltaTime, 0, 0);
        }

        if (MovementTypeID == 4) // Platform_Spikey_Kinetic_[4]
        {


            if (tr.position.x > 3.11)
            {
                moveSpeed *= -1;
            }
            else if (tr.position.x < -3.11)
            {
                moveSpeed *= -1;
            }

            tr.Translate(moveSpeed * Time.deltaTime, 0, 0);
        }

        if (MovementTypeID == 5) // Platform_Spikey_Kinetic_[5]
        {


            if (tr.GetChild(0).position.x > -1.13)
            {
                moveSpeed *= -1;
            }
            else if (tr.GetChild(0).position.x <= -2.39)
            {
                moveSpeed *= -1;
            }

            tr.GetChild(0).Translate(-1 * moveSpeed * Time.deltaTime, 0, 0);
            tr.GetChild(1).Translate(moveSpeed * Time.deltaTime, 0, 0);
        }

        if (MovementTypeID == 6) // Platform_Spikey_Bridge_Double_1_[6]
        {


            if (tr.GetChild(0).rotation.eulerAngles.z >= 60)
            {
                rotationSpeed *= -1;

            }

            if (tr.GetChild(0).rotation.eulerAngles.z >= 300)
            {
                rotationSpeed *= -1;

            }


            //Debug.Log($"Rotation is [LOCAL] {tr.GetChild(0).localEulerAngles.z}\n");
            //Debug.Log($"Rotation is [GLOBAL] {tr.GetChild(0).eulerAngles.z}\n");

            tr.GetChild(0).Rotate(0, 0, rotationSpeed * Time.fixedDeltaTime);
            tr.GetChild(1).Rotate(0, 0, rotationSpeed * Time.fixedDeltaTime * -1);

        }

        if (MovementTypeID == 7) // Platform_Spikey_Bridge_2_[7]
        {


            if (tr.GetChild(0).rotation.eulerAngles.z >= 60)
            {
                rotationSpeed *= -1;

            }

            if (tr.GetChild(0).rotation.eulerAngles.z < 0)
            {
                rotationSpeed *= -1;

            }


            Debug.Log($"Rotation is [LOCAL] {tr.GetChild(0).localEulerAngles.z}\n");
            Debug.Log($"Rotation is [GLOBAL] {tr.GetChild(0).eulerAngles.z}\n");

            tr.GetChild(0).Rotate(0, 0, rotationSpeed * Time.fixedDeltaTime);
            tr.GetChild(1).Rotate(0, 0, rotationSpeed * Time.fixedDeltaTime * -1);

        }

        if (MovementTypeID == 8) // Platform_Spikey_Bridge_2_[7]
        {


            if (tr.GetChild(0).rotation.eulerAngles.z >= 300)
            {
                rotationSpeed *= -1;

            }

            if (tr.GetChild(0).rotation.eulerAngles.z < 0)
            {
                rotationSpeed *= -1;

            }


            Debug.Log($"Rotation is [LOCAL] {tr.GetChild(0).localEulerAngles.z}\n");
            Debug.Log($"Rotation is [GLOBAL] {tr.GetChild(0).eulerAngles.z}\n");

            tr.GetChild(0).Rotate(0, 0, rotationSpeed * Time.fixedDeltaTime);
            tr.GetChild(1).Rotate(0, 0, rotationSpeed * Time.fixedDeltaTime);

        }

    }

    [System.Serializable]
    public class objectProps
    {
        public bool moveable;
        public bool moveableX;
        public bool moveableY;

        public float[] moveX = new float[2];
        public float[] moveY = new float[2];



    }


    public float getMoveSpeed()
    {
        return this.moveSpeed;
    }

    public void setMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }
}
