using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovements : MonoBehaviour
{
    // Start is called before the first frame update

    public int MovementTypeID;

    private Transform tr;

    [Header ("Speed Values")]
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float rotationSpeed = 1f;

    [Header("Object Props")]
    [SerializeField] private objectProps objectProperties = new objectProps();

    [Header("Child Configuration")]
    [SerializeField] private bool hasChilds;
    [SerializeField] private List<objectProps> childrenProps = new List<objectProps>();


    private Vector3 transformTargetPos = new Vector3();

    void Start()
    {
        this.tr = GetComponent<Transform>();



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
        }
        else
        {
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


                //Debug.Log($"Rotation is [LOCAL] {tr.GetChild(0).localEulerAngles.z}\n");
                //Debug.Log($"Rotation is [GLOBAL] {tr.GetChild(0).eulerAngles.z}\n");

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
        

    }

    [System.Serializable]
    public class objectProps
    {
        public bool isSpecial = false;
        public bool moveable;
        public bool moveableX;
        public bool moveableY;

        public float[] moveX = new float[2];
        public float[] moveY = new float[2];


        public bool setPosition;

        public float startPositionX = -1337;
        public float startPositionY = -1337;

        public float startRotationX;
        public float startRotationY;
        public float startRotationZ;
        public float startRotationW;


    }

    private Vector3 SetPosition(Vector3 pos, float x, float y, float z)
    {
        pos.x = x;
        pos.y = y;
        pos.z = z;
        return pos;
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
