using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puncher : MonoBehaviour
{
    public GameObject anglePoint;
    public AreaEffector2D effector;
    void Start()
    {
        effector = GetComponent<AreaEffector2D>();
        //anglePoint = transform.GetChild(0).GetComponent<GameObject>();

        foreach(Transform child in transform)
        {
            if(child.name == "AnglePointer")
            {
                anglePoint = child.gameObject;
            }
        }
    }


    void Update()
    {
        Vector2 direction = anglePoint.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        effector.forceAngle = angle;
    }
}
