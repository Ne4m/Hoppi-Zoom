using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCollision : MonoBehaviour
{

    [SerializeField] private string[] objectTags;
    [SerializeField] private GameObject[] objectsToDisableWith;
   // [SerializeField] GameObject[] ballObjects;


    private void Update()
    {
        // boundaryObjects = GameObject.FindGameObjectsWithTag("Boundary");
        //ballObjects = GameObject.FindGameObjectsWithTag("Ball");

        objectsToDisableWith = new GameObject[objectTags.Length];

        for(int i=0; i < objectTags.Length; i++)
        {

            try
            {
                if (GameObject.FindGameObjectsWithTag(objectTags[i]) != null)
                    objectsToDisableWith = GameObject.FindGameObjectsWithTag(objectTags[i]);
            }
            catch(Exception e)
            {
                Debug.Log($"Exception : {e.Message} Null Tag {objectTags[i]}");
            }



            foreach(GameObject obj in objectsToDisableWith)
            {
                if(obj.GetComponent<Collider2D>() != null){
                    var isIgnored = Physics2D.GetIgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());

                    if (!isIgnored) Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
                }

                
            }

        }


        //foreach (GameObject obj in ballObjects)
        //{
        //    Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<CircleCollider2D>(), true);
        //}
    }
}
