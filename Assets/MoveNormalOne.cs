using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNormalOne : MonoBehaviour
{
    private Transform[] children = new Transform[2];
    private bool isClosingGap;
    private Vector3 left;
    private Vector3 right;

    [SerializeField]
    private float moveDistance;
    [SerializeField]
    private float platformSpeed;

    private void Start()
    {
        GetChildren();
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

        float distance = Vector2.Distance(left, right);
        Vector3 leftScreenPos = Camera.main.WorldToScreenPoint(left);
        Vector3 rightScreenPos = Camera.main.WorldToScreenPoint(right);

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

        children[0].position = left;
        children[1].position = right;
    }
}
