using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    private Transform[] children;

    private bool isClosingGap;
    private bool isMovingRight;

    private float firstMove;

    private Vector3 left;
    private Vector3 right;
    private Vector3 middle;

    private int p_difficulty, p_platformNumber;

    public void GetChildren(int childCount)
    {
        children = new Transform[childCount];
        for (int i = 0; i < this.transform.childCount; i++)
        {
            children[i] = this.transform.GetChild(i);
        }
    }

    public void GetPlatformInfo(int platformNumber)
	{
        p_platformNumber = platformNumber;
	}

    public void MoveChildren(float platformSpeed, float moveDistance)
    {
        if(p_platformNumber != 4) 
        {  
            left = children[0].position;
            right = children[1].position;
            if(p_platformNumber == 3) middle = children[2].position;
		}
		else
		{
            middle = children[0].position;
		}
        
        Vector3 leftScreenPos = Camera.main.WorldToScreenPoint(left);
        Vector3 rightScreenPos = Camera.main.WorldToScreenPoint(right);

        if (p_platformNumber == 1)
		{
            float distance = Vector2.Distance(left, right);

            if (leftScreenPos.x < 100 && Mathf.Abs(rightScreenPos.x - Screen.width) < 100)
            {
                isClosingGap = true;
            }
            else if (distance < moveDistance)
            {
                isClosingGap = false;
            }

            if (isClosingGap)
            {
                left += new Vector3(platformSpeed * Time.fixedDeltaTime, 0, 0);
                right += new Vector3(platformSpeed * Time.fixedDeltaTime * -1, 0, 0);
            }
            else
            {
                left += new Vector3(platformSpeed * Time.fixedDeltaTime * -1, 0, 0);
                right += new Vector3(platformSpeed * Time.fixedDeltaTime, 0, 0);
            }
        }

		if (p_platformNumber== 2)
		{
            if (firstMove != 0)
            {
                Debug.Log(firstMove);
                isMovingRight = true;
            }

            if (Mathf.Abs(rightScreenPos.x - Screen.width) < 100)
            {
                isMovingRight = false;
            }
            else if (leftScreenPos.x < 100)
            {
                isMovingRight = true;
            }

            if (isMovingRight)
            {
                left += new Vector3(platformSpeed * Time.fixedDeltaTime, 0, 0);
                right += new Vector3(platformSpeed * Time.fixedDeltaTime, 0, 0);
            }
            else
            {
                left += new Vector3(platformSpeed * Time.fixedDeltaTime * -1, 0, 0);
                right += new Vector3(platformSpeed * Time.fixedDeltaTime * -1, 0, 0);
            }
        }

        if(p_platformNumber == 3)
		{
            //Debug.Log("ACCESSED ");
            float leftDistance = Vector3.Distance(left, middle);
            float rightDistance = Vector3.Distance(right, middle);

            if(leftDistance < moveDistance)
			{
                isMovingRight = true;
			}
            else if(rightDistance < moveDistance)
			{
                isMovingRight = false;
			}

			if (isMovingRight)
			{
                middle += new Vector3(platformSpeed * Time.fixedDeltaTime, 0, 0);
			}
            else
			{
                middle += new Vector3(platformSpeed * Time.fixedDeltaTime * -1, 0, 0);
			}

		}
        
        children[0].position = left;
        children[1].position = right;
        if (p_platformNumber == 3)
        {
            children[2].position = middle;
        }
    }

    public void ArrangeDistanceAtStart(float moveDistance)
    {
        float manualDistance = Mathf.Abs(children[0].position.x - children[1].position.x);

        if (manualDistance < moveDistance)
        {
            children[0].position = new Vector3((moveDistance / 2) * -1, children[1].position.y, 0);
            children[1].position = new Vector3(moveDistance / 2, children[1].position.y, 0);
        }
        else if (manualDistance > moveDistance)
        {
            children[0].position = new Vector3(moveDistance / 2, children[1].position.y, 0);
            children[1].position = new Vector3((moveDistance / 2) * -1, children[1].position.y, 0);
        }
    }

}
