using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public struct PlatformInfo
	{
        public int difficulty;
        public int totalPlatformCount;
        /* 0-Easy
         * 1-Normal
         * 2-Hard
        */
	}

    private List<Transform> easyPlatforms = new List<Transform>();
    private List<Transform> normalPlatforms = new List<Transform>();
    private List<Transform> hardPlatforms = new List<Transform>();

    [HideInInspector]
    public float childrenRefreshTime =5f;

    // Start is called before the first frame update
    void Start()
    {
        GetAllChildren();
    }

    // Update is called once per frame
    void Update()
    {
        
        //MoveEasyOne();
    }
    void GetAllChildren()
	{
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child;
            child = transform.GetChild(i);
            if (child.tag.Contains("Easy"))
            {
                easyPlatforms.Add(child);
            }
            else if (child.tag.Contains("Normal"))
            {
                normalPlatforms.Add(child);
            }
            else if (child.tag.Contains("Hard"))
            {
                hardPlatforms.Add(child);
            }
            else
            {
                //Debug.LogError("There are platforms that are not identified");
            }
        }

    }


    void MoveEasyOne()
	{
        
        if (easyPlatforms.Count <= 0)
            return;
        
        Transform childTransform;
        for (int i =0; i< easyPlatforms.Count+1; i++)
		{
            for(int z = 0; z < easyPlatforms[i].childCount; z++)
			{
                childTransform = easyPlatforms[i].GetChild(z);
                print(childTransform);
            }
            
		}
	}

    /*
    IEnumerator GetAllChildrenCoroutine()
	{
        yield return new WaitForSeconds(childrenRefreshTime);
    }
    */
}
