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


    [SerializeField]
    private float moveSpeed = 1f;

    private bool goingRight = true;

    void Start()
    {
        this.tr = GetComponent<Transform>();

        switch (MovementTypeID)
        {
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
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

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
