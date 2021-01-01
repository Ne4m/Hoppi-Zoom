using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNormalTwo : MonoBehaviour
{
    private Transform[] children = new Transform[2];
    private bool isMovingRight;
    private float firstMove;
    private Vector3 left;
    private Vector3 right;

    [SerializeField]
    private float moveDistance;
    [SerializeField]
    private float platformSpeed;

    private void Start()
    {
        GetChildren();

        firstMove = Random.Range(0, 1);

        ArrangeDistanceAtStart();
    }

    private void FixedUpdate()
    {

        MoveChildren();
    }

    void GetChildren()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            children[i] = this.transform.GetChild(i);
        }
    }

    void MoveChildren()
    {
        left = children[0].position;
        right = children[1].position;

        Vector3 leftScreenPos = Camera.main.WorldToScreenPoint(left);
        Vector3 rightScreenPos = Camera.main.WorldToScreenPoint(right);

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

        children[0].position = left;
        children[1].position = right;
    }

    void ArrangeDistanceAtStart()
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
